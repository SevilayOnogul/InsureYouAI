using InsureYouAI.Context;
using InsureYouAI.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Text;
using System.Text.Json;

namespace InsureYouAI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly InsureContext _context;
        private readonly IConfiguration _configuration; // Yapılandırma dosyasına erişim için eklendi

        public DefaultController(InsureContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult SendMessage()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(Message message)
        {
            // 1. Kullanıcıdan gelen mesajı kaydediyoruz
            message.SendDate = DateTime.Now;
            message.IsRead = false;
            _context.Messages.Add(message);
            _context.SaveChanges();

            #region Claude_AI_Analiz
            // API Key'i güvenli şekilde appsettings.json'dan çekiyoruz
            string apiKey = _configuration["ClaudeSettings:ApiKey"] ?? "";
            string prompt = $"Sen bir sigorta firmasının müşteri iletişim asistanısın.\r\n\r\nKurumsal ama samimi, net ve anlaşılır bir dille yaz.\r\n\r\nYanıtlarını 2–3 paragrafla sınırla.\r\n\r\nEksik bilgi (poliçe numarası, kimlik vb.) varsa kibarca talep et.\r\n\r\nFiyat, ödeme, teminat gibi kritik konularda kesin rakam verme, müşteri temsilcisine yönlendir.\r\n\r\nHasar ve sağlık gibi hassas durumlarda empati kur.\r\n\r\nCevaplarını teşekkür ve iyi dilekle bitir.\r\n\r\n Kullanıcının sana gönderdiği mesaj şu şekilde:' {message.MessageDetail}.'";

            string? textContent = "Mesajınız sistemimize ulaştı, en kısa sürede size dönüş sağlayacağız.";

            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://api.anthropic.com/");
                client.DefaultRequestHeaders.Add("x-api-key", apiKey);
                client.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestBody = new { model = "claude-3-opus-20240229", max_tokens = 1000, temperature = 0.5, messages = new[] { new { role = "user", content = prompt } } };
                var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("v1/messages", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var json = JsonNode.Parse(responseString);
                    textContent = json?["content"]?[0]?["text"]?.ToString() ?? textContent;
                }
            }
            catch { /* Hata durumunda varsayılan textContent kullanılır */ }
            #endregion

            #region Email_Gönderme
            var senderEmail = _configuration["EmailSettings:Email"];
            var appPassword = _configuration["EmailSettings:Password"];

            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("InsureYouAI Admin", senderEmail));
            mimeMessage.To.Add(new MailboxAddress("Sayın Müşterimiz", message.Email));
            mimeMessage.Subject = "InsureYouAI Bilgilendirme Yanıtı";

            var bodyBuilder = new BodyBuilder();
            // Mail içeriğine sadece yapay zeka yanıtını koyuyoruz 
            bodyBuilder.TextBody = textContent;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using (SmtpClient client2 = new SmtpClient())
            {
                client2.Connect("smtp.gmail.com", 587, false);
                client2.Authenticate(senderEmail, appPassword);
                client2.Send(mimeMessage);
                client2.Disconnect(true);
            }
            #endregion

            #region ClaudeAIMessage_DbKayıt
            // Yapay zekanın verdiği cevabı veritabanına kaydediyoruz
            ClaudeAIMessage claudeAIMessage = new ClaudeAIMessage()
            {
                MessageDetail = textContent,
                ReceiveEmail = message.Email,
                ReceiveNameSurname = message.NameSurname,
                SendDate = DateTime.Now
            };
            _context.ClaudeAIMessages.Add(claudeAIMessage);
            _context.SaveChanges();
            #endregion

            TempData["SuccessMessage"] = "Mesajınız başarıyla iletildi!";
            return RedirectToAction("Index");
        }
        public PartialViewResult SubscribeEmail()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult SubscribeEmail(string email)
        {
            return RedirectToAction("Index");
        }
    }
}