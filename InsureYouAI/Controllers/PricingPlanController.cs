using InsureYouAI.Context;
using InsureYouAI.Entities;
using InsureYouAI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Net.Http.Headers;
using System.Text;

namespace InsureYouAI.Controllers
{
    public class PricingPlanController : Controller
    {
        private readonly InsureContext _context;

        public PricingPlanController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult PricingPlanList()
        {
            ViewBag.ControllerName = "AI Destekli Sigorta Planı";
            ViewBag.PageName = "Mevcut Sigorta Plan Listeleri";
            var values = _context.PricingPlans.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreatePricingPlan()
        {
            ViewBag.ControllerName = "AI Destekli Sigorta Planı";
            ViewBag.PageName = "Yeni Sigorta Planı Oluşturma";
            return View();
        }

        [HttpPost]
public IActionResult CreatePricingPlan(PricingPlan pricingPlan)
{
    try
    {
        var userName = User.Identity?.Name ?? "Anonymous";

        _context.PricingPlans.Add(pricingPlan);
        _context.SaveChanges();

        Log.Information(
            "PricingPlan oluşturuldu. Id: {Id}, PlanAdi: {PlanAdi}, IslemYapan: {User}",
            pricingPlan.PricingPlanId,
            pricingPlan.Title,
            userName
        );

        return RedirectToAction("PricingPlanList");
    }
    catch (Exception ex)
    {
        Log.Error(ex, "PricingPlan oluşturulurken hata oluştu");
        throw;
    }
}


        [HttpGet]
        public IActionResult UpdatePricingPlan(int id)
        {
            ViewBag.ControllerName = "AI Destekli Sigorta Planı";
            ViewBag.PageName = "Sigorta Plan Revizyonu";
            var value = _context.PricingPlans.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdatePricingPlan(PricingPlan pricingPlan)
        {
            try
            {
                var userName = User.Identity?.Name ?? "Anonymous";

                _context.PricingPlans.Update(pricingPlan);
                _context.SaveChanges();

                Log.Information(
                    "PricingPlan güncellendi. Id: {Id}, PlanAdi: {PlanAdi}, IslemYapan: {User}",
                    pricingPlan.PricingPlanId,
                    pricingPlan.Title,
                    userName
                );

                return RedirectToAction("PricingPlanList");
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "PricingPlan güncellenirken hata oluştu. Id: {Id}",
                    pricingPlan.PricingPlanId
                );
                throw;
            }
        }


        public IActionResult DeletePricingPlan(int id)
        {
            try
            {
                var pricingPlan = _context.PricingPlans.Find(id);

                if (pricingPlan == null)
                {
                    Log.Warning("Silinmek istenen PricingPlan bulunamadı. Id: {Id}", id);
                    return RedirectToAction("PricingPlanList");
                }

                var userName = User.Identity?.Name ?? "Anonymous";

                _context.PricingPlans.Remove(pricingPlan);
                _context.SaveChanges();

                Log.Information(
                    "PricingPlan silindi. Id: {Id}, PlanAdi: {PlanAdi}, IslemYapan: {User}",
                    pricingPlan.PricingPlanId,
                    pricingPlan.Title,
                    userName
                );

                return RedirectToAction("PricingPlanList");
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "PricingPlan silinirken hata oluştu. Id: {Id}",
                    id
                );

                throw;
            }
        }


        public IActionResult ChangeStatus(int id)
        {
            var userName = User.Identity?.Name ?? "Anonymous";

            try
            {
                var value = _context.PricingPlans.Find(id);

                if (value == null)
                {
                    Log.Warning(
                        "PricingPlan bulunamadı. Id: {Id}, IslemYapan: {User}",
                        id,
                        userName
                    );
                    return RedirectToAction("PricingPlanList");
                }

                bool oldStatus = value.IsFeature;
                value.IsFeature = !value.IsFeature;

                _context.SaveChanges();

                Log.Information(
                    "PricingPlan durumu değiştirildi. Id: {Id}, EskiDurum: {OldStatus}, YeniDurum: {NewStatus}, IslemYapan: {User}",
                    id,
                    oldStatus ? "Aktif" : "Pasif",
                    value.IsFeature ? "Aktif" : "Pasif",
                    userName
                );

                return RedirectToAction("PricingPlanList");
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "PricingPlan durum değişikliği sırasında hata oluştu. Id: {Id}, IslemYapan: {User}",
                    id,
                    userName
                );

                throw; 
            }
        }


        


        [HttpGet]
        public IActionResult CreateUserCustomizePlan()
        {
            ViewBag.ControllerName = "AI Destekli Sigorta Planı";
            ViewBag.PageName = "Kullanıcıya Özel AI Destekli Sigorta Planı Belirleme";
            var model = new AIInsuranceRecommendationViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserCustomizePlan(AIInsuranceRecommendationViewModel model)
        {
            ViewBag.ControllerName = "AI Destekli Sigorta Planı";
            ViewBag.PageName = "Kullanıcıya Özel AI Destekli Sigorta Planı Belirleme";

            string apiKey = "";
            // Kullanıcı girdilerini JSON'a çeviriyoruz
            var userJson = JsonConvert.SerializeObject(model);

            // OpenAI'ye göndereceğimiz prompt:
            var prompt = $@"
                Sen profesyonel bir sigorta uzmanı AI asistanısın. 
                Aşağıdaki kullanıcının bilgilerini analiz ederek en uygun sigorta paketini öner.

                Paketler ve özellikleri:
                1) Premium Paket (599 TL/ay): Yatarak tedavi, check-up, geniş yol yardım, yurtiçi seyahat güvencesi.
                2) Standart Paket (449 TL/ay): Acil sağlık, müşteri hizmetleri, kaza sonrası tıbbi destek.
                3) Ekonomik Paket (339 TL/ay): Temel sağlık, temel yol yardım.

                Kullanıcı bilgileri:
                {userJson}

                Sadece şu formatta JSON döndür:

                {{
                  ""onerilenPaket"": ""Premium | Standart | Ekonomik"",
                  ""ikinciSecenek"": ""Premium | Standart | Ekonomik"",
                  ""neden"": ""Kısa analiz metni""
                }}
                ";

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            var body = new
            {
                model = "gpt-4.1-mini",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var jsonBody = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            dynamic ai = JsonConvert.DeserializeObject(jsonResponse);
            string aiResult = ai.choices[0].message.content;

            // AI cevabı JSON formatında gelmiş olacak
            var result = JsonConvert.DeserializeObject<AIInsuranceRecommendationViewModel>(aiResult);

            // Sonuçları modele geri yazıyoruz
            model.RecommendedPackage = result.onerilenPaket;
            model.SecondBestPackage = result.ikinciSecenek;
            model.AnalysisText = result.neden;
            TempData["RawAI"] = aiResult;
            return View(model);
        }
    }
}