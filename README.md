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
- **Frontend:** Bootstrap 5, jQuery, Razor Views, Bootstrap Icons  

---

## ✨ Mevcut Özellikler (Şu Ana Kadar)

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
- **Müşteri Yorumları (Testimonials)** üzerinden yapay zeka destekli analiz
- Kullanıcı yorumlarının:
  - Anlam bütünlüğü korunarak işlenmesi
  - AI tarafından örnek/temsilî yorum metinlerine dönüştürülmesi
- Sigorta sektörüne uygun, doğal ve gerçekçi müşteri geri bildirimleri üretimi
- Üretilen yorumların:
  - Admin panelinde listelenmesi
  - Manuel düzenlemeye açık şekilde saklanması

> ⚠️ API anahtarları güvenlik nedeniyle projede **hardcoded tutulmamaktadır**.  
> Environment Variable veya `appsettings.json` üzerinden yönetilmesi önerilir.

---

## 🏗 Proje Yapısı

### 📁 Controllers
Uygulamanın iş akışı ve endpoint yönetimi:
- `ArticleController` → Makale CRUD & OpenAI entegrasyonu
- `ServiceController` → Anthropic Claude entegrasyonu
- `CategoryController` → Kategori yönetimi
- `AboutController` → Kurumsal içerik yönetimi
- `AdminLayoutController` → Admin panel layout yapısı

---

### 📁 Entities
Veritabanı tablolarını temsil eden sınıflar:
- `Article`
- `Category`
- `Service`
- `About`
- `Testimonial`
- Diğer içerik varlıkları

---

### 📁 Context
- `InsureContext`  
  Entity Framework Core DbContext yapılandırmaları ve DbSet tanımları

---

### 📁 Views
Razor tabanlı kullanıcı arayüzleri:
- `AdminLayout` → Yönetim paneli ana layout
- `Article`, `Service`, `Category`, `About`, `Contact` vb. modüller
- Identity kullanıcı ekranları

---

### 📁 ViewComponents
Admin panel için modüler bileşenler:
- Navbar  
- Sidebar  
- Breadcrumb  
- Script & Head bileşenleri  

---

### 📁 Migrations
- EF Core migration dosyaları
- Veritabanı şema ve versiyon yönetimi

---

## 🗺️ Roadmap (Planlanan Geliştirmeler)

- 🔹 Çoklu AI sağlayıcıları için ortak servis katmanı
- 🔹 Prompt yönetimi ve versiyonlama
- 🔹 AI çıktılarının veritabanına kaydedilmesi
- 🔹 Rol bazlı yetkilendirme (Admin / Editor)
- 🔹 AI içerik kalite kontrol ve düzenleme ekranları
- 🔹 Görsel + metin aynı prompttan üretme altyapısı

---

## ⚙️ Kurulum ve Çalıştırma

1. Projeyi klonlayın:
   ```bash
   git clone https://github.com/SevilayOnogul/InsureYouAI.git
2. `appsettings.json` dosyasındaki **Connection String** bilgisini güncelleyin.
3. Package Manager Console üzerinden `Update-Database` komutunu çalıştırın.
4. Projeyi çalıştırın: `Ctrl + F5`
