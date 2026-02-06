# 🛡️ InsureYouAI – ASP.NET Core 8.0 Yapay Zeka Destekli Sigorta Portalı

**InsureYouAI**, sigortacılık sektörü için geliştirilen,  
**yapay zeka destekli içerik üretimini ve yönetimini** merkeze alan bir  
**ASP.NET Core 8 MVC** tabanlı web uygulamasıdır.

Proje; başta **OpenAI**, **Google Gemini**, **Anthropic Claude**, **Hugging Face**,  
**Tavily AI** ve **ElevenLabs** olmak üzere farklı **LLM ve AI servislerinin**  
entegrasyonuna uygun şekilde tasarlanmıştır.

> 📌 Proje geliştirme süreci tamamlanmış olup, mevcut haliyle **uçtan uca çalışan bir AI destekli sigorta portalıdır**.  
> Mimari yapı, **çoklu yapay zeka sağlayıcılarını** destekleyecek şekilde genişletilebilir yapıdadır.

---

## 🛠 Kullanılan Teknolojiler

- **Backend:** ASP.NET Core 8 MVC  
- **Identity:** ASP.NET Core Identity  
- **ORM:** Entity Framework Core  
- **Database:** MS SQL Server  
- **AI & ML Entegrasyonları:**  
  - OpenAI API  
  - Google Gemini API  
  - Anthropic Claude API  
  - Hugging Face API  
  - Tavily AI  
  - ElevenLabs  
  - ML.NET & Microsoft.ML.TimeSeries  
- **Frontend:** Razor Views, Bootstrap 5, Bootstrap Icons  

---

## ✨ Mevcut Özellikler 

- ✅ **Dinamik Dashboard & Grafik Yönetimi:**
  - Finansal verilerin **LINQ `GroupBy`** ile aylık bazda analizi
  - **ViewComponent** tabanlı modüler dashboard yapısı
  - **Chart.js / ApexCharts** ile dinamik grafikler

- ✅ **ML.NET Time Series Forecasting (SSA):**
  - Sigorta poliçe satış verilerinin zaman serisi analizi
  - **ML.NET** ve **Microsoft.ML.TimeSeries** kullanılarak satış tahmini
  - SSA (Singular Spectrum Analysis) algoritması ile:
    - Tahmin edilen satış değerleri
    - Alt / üst güven aralıkları
  - Dashboard ve raporlama sistemleri için öngörü altyapısı

- ✅ **İletişim Paneli & AI Otomatik Yanıt Sistemi:**
  - Kullanıcı mesajlarının **SQL veritabanına** kaydedilmesi
  - **Anthropic Claude API** entegrasyonu ile akıllı yanıt üretimi
  - **MailKit / SMTP** üzerinden kullanıcıya anında e-posta gönderimi

- ✅ **Kullanıcı Kayıt Sistemi (Identity):**
  - `AspNetUsers` tablosu ile entegre kullanıcı kayıt akışı
  - JavaScript ile şifre göster / gizle
  - Dinamik form doğrulamaları

- ✅ **Çoklu Yapay Zeka Entegrasyonu:**
  - **OpenAI:** Makale (Article) içerik üretimi
  - **Google Gemini:** Kurumsal ve statik içerik üretimi
  - **Anthropic Claude:** Hizmet (Services) içerikleri
  - **Hugging Face:** Duygu analizi ve moderasyon altyapısı

- ✅ **AI Destekli Görsel Oluşturma:**
  - **OpenAI DALL·E** ile prompt tabanlı görsel üretimi
  - Üretilen görsellerin içeriklerde kullanılmasına uygun altyapı

- ✅ **Admin Paneli:**
  - Bootstrap tabanlı responsive tasarım
  - CRUD operasyonları
  - Modüler ve genişletilebilir yapı

- ✅ **Blog Sistemi:**
  - Partial View (Kısmi Görünüm) ile modüler blog listeleme
  - Arama (Search) altyapısı
  - Blog detay sayfası ve sosyal paylaşım linkleri

---

## 🤖 Yapay Zeka Entegrasyon Detayları

### 💬 Real-Time AI Chat (SignalR & Streaming)
- SignalR tabanlı gerçek zamanlı AI sohbet altyapısı
- OpenAI token-by-token streaming
- Sohbet geçmişi (context) yönetimi
- Asenkron ve bellek dostu veri akışı

---

### 🔹 Anthropic Claude API

#### 📄 PDF Analizleri
- Kullanıcı tarafından yüklenen PDF dosyalarının:
  - Metin içeriklerinin çıkarılması
  - Yapay zeka ile analiz edilmesi
- Sigorta dokümanları üzerinden:
  - Özetleme
  - İçerik yorumlama
  - Bilgiye dayalı yanıt üretimi

---

### 🔍 Tavily AI Entegrasyonu
- Yapay zekaya **gerçek zamanlı web arama** yeteneği kazandırılması
- AI yanıtlarının:
  - Güncel
  - Kaynağa dayalı
  - Daha doğru bağlamda üretilmesi
- Sigorta sektörüne özel güncel bilgi çekme altyapısı

---

### 🔊 ElevenLabs – Metin Seslendirme & Sesli Asistan
- AI tarafından üretilen metinlerin **doğal insan sesiyle** seslendirilmesi
- Kullanıcıya:
  - Sesli bilgilendirme
  - Sesli asistan deneyimi
- Erişilebilirlik ve kullanıcı deneyimini artıran yapı

---

### 🔹 Hugging Face API

- **Toxic-BERT** ile kullanıcı yorumlarının moderasyonu
- Yorumların:
  - Toksik
  - Onaylandı
  olarak sınıflandırılması
- **Helsinki-NLP** ile otomatik çeviri (TR → EN)

---

### 🔍 AI Destekli Profil & Davranış Analizi
- Kullanıcının yazdığı makaleler üzerinden:
  - İlgi alanı
  - Yazım tarzı
  - Davranış profili çıkarımı
- AI analiz sonuçlarının admin panelinde görüntülenmesi
- Manuel değerlendirme ve düzenlemeye açık yapı

---

⚠️ **Güvenlik Notu:**  
Production ortamında API anahtarlarının  
`appsettings.json`, **Environment Variables** veya **Azure Key Vault** üzerinden  
yönetilmesi önerilmektedir.

---

## ⚙️ Kurulum ve Çalıştırma

1. Projeyi klonlayın:
   ```bash
   git clone https://github.com/SevilayOnogul/InsureYouAI.git
2. `appsettings.json` dosyasındaki **Connection String** bilgisini güncelleyin.
3. Package Manager Console üzerinden `Update-Database` komutunu çalıştırın.
4. Projeyi çalıştırın: `Ctrl + F5`
5. **API Yapılandırması:**  
   `appsettings.json` dosyası içerisindeki **OpenAI**, **Google Gemini** ve  
   **Hugging Face** API anahtar alanlarını kendi lisans anahtarlarınızla doldurun.

> ℹ️ Güvenlik nedeniyle API anahtarları projede varsayılan olarak boş bırakılmıştır.  
> 🧠 Bu proje, modern web geliştirme ve yapay zeka entegrasyonlarının birlikte nasıl tasarlanabileceğini göstermek amacıyla geliştirilmiştir.
