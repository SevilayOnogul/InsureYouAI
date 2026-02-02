# 🛡️ InsureYouAI – ASP.NET Core 8.0 Yapay Zeka Destekli Sigorta Portalı

**InsureYouAI**, sigortacılık sektörü için geliştirilen,  
**yapay zeka destekli içerik üretimini ve yönetimini** merkeze alan bir  
**ASP.NET Core 8 MVC** tabanlı web uygulamasıdır.

Proje; başta **OpenAI**, **Google Gemini**, **Anthropic Claude** ve **Hugging Face** olmak üzere  
farklı **LLM (Large Language Model)** servislerinin entegrasyonuna uygun şekilde tasarlanmıştır.

> 📌 Proje şu anda **aktif geliştirme aşamasındadır**.  
> Mimari yapı, **çoklu yapay zeka sağlayıcılarını** destekleyecek şekilde genişletilebilir yapıdadır.

---

## 🛠 Kullanılan Teknolojiler

- **Backend:** ASP.NET Core 8 MVC  
- **Identity:** ASP.NET Core Identity (Kullanıcı Yönetimi & Güvenlik)
- **ORM:** Entity Framework Core  
- **Database:** MS SQL Server  
- **AI Entegrasyonu:**  
  - OpenAI API  
  - Google Gemini API  
  - Anthropic Claude API  
  - Hugging Face API  
- **Frontend:** Razor Views, Bootstrap 5, Bootstrap Icons (minimal jQuery usage)

---

## ✨ Mevcut Özellikler 

- ✅ **İletişim Paneli & AI Otomatik Yanıt Sistemi:**
  - Kullanıcı mesajlarının **SQL veritabanına** kaydedilmesi
  - **Anthropic Claude API** entegrasyonu ile akıllı yanıt üretimi
  - **MailKit / SMTP** üzerinden kullanıcıya anında e-posta gönderimi

- ✅ **Kullanıcı Kayıt Sistemi (Identity):**
  - `AspNetUsers` tablosu ile entegre kullanıcı kayıt akışı
  - JavaScript ile şifre göster/gizle
  - Dinamik form kontrolleri

- ✅ **Çoklu Yapay Zeka Entegrasyonu:**
  - **OpenAI:** Makale (Article) içerik üretimi
  - **Google Gemini:** Kurumsal ve statik içerik üretimi (Hakkımızda vb.)
  - **Anthropic Claude:** Hizmetler (Services) bölümü için yapılandırılmış veri üretimi
  - **Hugging Face:** Müşteri yorumları üzerinden duygu ve metin analizi altyapısı

- ✅ **AI Destekli Görsel Oluşturma:**
  - **OpenAI DALL·E** ile prompt tabanlı görsel üretimi
  - Üretilen görsellerin içeriklerde kullanılmasına uygun altyapı

- ✅ **Admin Paneli:**
  - Bootstrap tabanlı responsive tasarım
  - CRUD operasyonları
  - Modüler ve genişletilebilir yapı

- ✅ **Blog Sistemi:**
  - Partial View (Kısmi Görünüm) ile modüler blog listeleme
  - Arama (Search) mekanizması için altyapı hazırlanması

---

## 🤖 Yapay Zeka Entegrasyon Detayları

### 💬 Real-Time AI Chat (SignalR & Streaming)
- **Anlık Sohbet:** Kullanıcı ve yapay zeka arasında SignalR tabanlı, gecikmesiz iletişim altyapısı.
- **Token Streaming:** OpenAI’dan gelen yanıtların (GPT-4o-mini) tamamının beklenmesi yerine, kelime kelime (token-by-token) eş zamanlı olarak arayüze yansıtılması.
- **Sohbet Geçmişi (Context):** Kullanıcı oturumu boyunca konuşma geçmişinin saklanması ve yapay zekanın önceki mesajları hatırlayarak cevap vermesi.
- **Asenkron Akış Yönetimi:** `IHttpClientFactory` ve `StreamReader` kullanılarak bellek dostu ve yüksek performanslı veri akışı sağlanması.

### 🔹 OpenAI API
- Prompt tabanlı makale üretimi
- Admin panel üzerinden AI destekli içerik oluşturma


### 🎨 OpenAI DALL·E
- Yapay zeka destekli görsel üretimi
- Prompt tabanlı resim oluşturma
- Üretilen görsellerin:
  - Admin panel üzerinden önizlenmesi
  - İçeriklerde (makale, hizmet, slider vb.) kullanılabilmesi
- Çoklu AI sağlayıcı mimarisine uyumlu servis yapısı

### 🔹 Google Gemini API
- Kurumsal ve bilgilendirici metin üretimi
- Çoklu LLM mimarisine geçiş için deneme altyapısı

### 🔹 Anthropic Claude API
- **ServiceController** üzerinden hizmet içeriklerinin otomatik oluşturulması
- AI çıktılarının:
  - `Split`
  - `Trim`
  yöntemleri ile işlenerek **liste/tablo yapısına** dönüştürülmesi
- Prompt çıktıları admin panelinde **manuel düzenlemeye uygun** yapıdadır

### 🔹 Hugging Face API

#### 🗣️ Müşteri Yorumları (Testimonials) & Moderasyon
- Kullanıcı yorumlarının **anlam bütünlüğü korunarak** yapay zeka tarafından işlenmesi  

- **Toxic-BERT Modeli Entegrasyonu:**
  - Yorumların toksiklik (küfür / hakaret / olumsuz dil) oranının analiz edilmesi  
  - Uygunsuz içeriklerin otomatik olarak tespit edilmesi ve filtrelenmesi  

- **Dinamik Onay Sistemi:**
  - AI analiz sonucuna göre yorumların:
    - **Toksik**
    - **Onaylandı**
    olarak sınıflandırılması  
  - Sonucun veritabanına kaydedilmesi ve admin panelinde yönetilmesi  

#### 🌍 Helsinki-NLP Entegrasyonu
- Kullanıcı yorumlarının (Türkçe) yapay zeka tarafından **otomatik olarak İngilizceye çevrilmesi**  
- Çeviri sonuçlarının:
  - Asenkron (async) yöntemlerle işlenmesi  
  - AI moderasyon katmanına (**Toxic-BERT**) girdi olarak beslenmesi  
- Çok dilli destek ve **global içerik yönetimi** için altyapı oluşturulması  

---

### 🔍 AI Destekli Profil & Davranış Analizi
- Kullanıcıların **kendi yazdığı makaleler** üzerinden yapay zeka destekli analiz yapılması  
- Yazı içeriklerine göre kullanıcının:
  - İlgi alanlarının
  - Yazım tarzının
  - Genel profil ve davranış eğilimlerinin
  AI tarafından çıkarımlanması  
- AI tarafından üretilen analiz sonuçlarının:
  - Admin panelinde görüntülenmesi
  - Manuel değerlendirme ve düzenlemeye açık olması  
- Kişiselleştirilmiş içerik üretimi ve kullanıcı segmentasyonu için altyapı oluşturulması

⚠️ Güvenlik Notu: Proje şu an geliştirme aşamasındadır. API anahtarları kolay test edilebilmesi amacıyla kod içerisinde yer almaktadır. Projenin yayına alınması (Production) durumunda, bu anahtarların appsettings.json, Environment Variables veya Azure Key Vault gibi güvenli yöntemlerle yönetilmesi kritik önem taşımaktadır.

---

## 🏗 Proje Yapısı

### 📁 Controllers
- `ArticleController` → Makale CRUD & OpenAI entegrasyonu
- `ServiceController` → Anthropic Claude entegrasyonu
- `CategoryController` → Kategori yönetimi
- `AboutController` → Kurumsal içerik yönetimi
- `AdminLayoutController` → Admin panel layout yapısı

### 📁 Entities
- `Article`
- `Category`
- `Service`
- `About`
- `Testimonial`
- Diğer içerik varlıkları

### 📁 Context
- `InsureContext`  
  Entity Framework Core DbContext yapılandırmaları

### 📁 Views
- `AdminLayout`
- `Article`, `Service`, `Category`, `About`, `Contact`
- Identity kullanıcı ekranları

### 📁 ViewComponents
- Navbar  
- Sidebar  
- Breadcrumb  
- Script & Head  

### 📁 Migrations
- EF Core migration dosyaları
- Veritabanı şema ve versiyon yönetimi

---

## 🗺️ Roadmap (Planlanan Geliştirmeler)

- 🔹 Çoklu AI sağlayıcıları için ortak servis katmanı
- 🔹 Prompt yönetimi ve versiyonlama
- 🔹 Rol bazlı yetkilendirme (Admin / Editor)
- 🔹 AI içerik kalite kontrol ve düzenleme ekranları
- 🔹 Görsel + metin aynı prompttan üretme altyapısı
- 🔹 Serilog ile AI isteklerinin ve hata süreçlerinin izlenebilir hale getirilmesi

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
