# 🛡️ InsureYouAI – Yapay Zeka Destekli Sigorta Portalı

**InsureYouAI**, sigortacılık sektörü için geliştirilen,  
**yapay zeka destekli içerik üretimini ve yönetimini** merkeze alan bir **ASP.NET Core MVC** tabanlı web uygulamasıdır.

Proje; başta **OpenAI** olmak üzere farklı **LLM (Large Language Model)** servislerinin entegrasyonuna uygun şekilde tasarlanmış,  
**AI destekli makale üretimi, içerik yönetimi ve admin paneli** odaklı bir altyapı sunmaktadır.

> 📌 Proje şu anda **aktif geliştirme / başlangıç aşamasındadır**.  
> Mimari yapı, ilerleyen aşamalarda **çoklu yapay zeka sağlayıcılarını** destekleyecek şekilde planlanmıştır.

---

## 🛠 Kullanılan Teknolojiler

- **Backend:** ASP.NET Core MVC  
- **ORM:** Entity Framework Core  
- **Database:** MS SQL Server  
- **AI Entegrasyonu:** OpenAI API, Google Gemini API  
- **Frontend:** Bootstrap 5, Razor Views, Bootstrap Icons  
- **Mimari Yaklaşım:**  
  Tek katmanlı yapı, **SOLID prensiplerine ve Clean Code** yaklaşımına uygun geliştirme

---

## ✨ Mevcut Özellikler (Şu Ana Kadar)

- ✅ **Makale Yönetimi (CRUD):**  
  Makale ekleme, listeleme, güncelleme ve silme işlemleri
- 🤖 **AI Destekli Makale Üretimi:**  
  Prompt tabanlı otomatik içerik oluşturma (Admin Panel üzerinden)
- 📂 **Kategori Sistemi:**  
  Makalelerin kategoriler ile bire-çok ilişkili şekilde yönetilmesi
- 📊 **Admin Panel:**  
  Bootstrap tabanlı, responsive ve kullanıcı dostu yönetim paneli
- 🧭 **Sidebar Navigasyon:**  
  Düzenli admin menü yapısı ve doğru URL yönlendirmeleri
- 🧩 **ViewComponent Kullanımı:**  
  Admin layout bileşenlerinin modüler hale getirilmesi

---

## 🤖 Yapay Zeka Entegrasyonları

- **OpenAI API**
  - Makale üretimi için prompt tabanlı içerik oluşturma
  - Admin panel üzerinden AI destekli yazı üretimi

- **Google Gemini API**
  - “Hakkımızda” gibi statik içeriklerin AI ile oluşturulması
  - Çoklu LLM entegrasyonuna uygun yapı denemeleri

> ⚠️ API anahtarları güvenlik nedeniyle projede **hardcoded** tutulmamaktadır.

---

## 🏗 Proje Yapısı

### 📁 Controllers
Uygulamanın iş akışı ve endpoint yönetimi:
- `ArticleController` → Makale CRUD & AI entegrasyonu
- `CategoryController` → Kategori yönetimi
- `AdminLayoutController` → Admin panel layout yapısı
- `About`, `Service`, `Slider`, `Testimonial`, `PricingPlan` vb. içerik controller’ları

---

### 📁 Entities
Veritabanı tablolarını temsil eden sınıflar:
- `Article`
- `Category`
- Diğer içerik varlıkları

---

### 📁 Context
- `InsureContext`  
  EF Core DbContext yapılandırmaları ve DbSet tanımları

---

### 📁 Views
Razor tabanlı kullanıcı arayüzleri:
- `AdminLayout` → Yönetim paneli ana layout
- `Article` → Makale listeleme, ekleme ve güncelleme sayfaları
- `Category`, `About`, `Service`, `Contact` vb. modüller

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
- Veritabanı şema yönetimi

---

## 🗺️ Roadmap (Planlanan Geliştirmeler)

- Çoklu AI sağlayıcıları için ortak servis altyapısı
- Prompt yönetimi ve versiyonlama
- AI çıktılarını veritabanına kaydetme
- Yetkilendirme & rol bazlı admin erişimi
- AI içerik kalite kontrol ve düzenleme ekranları

---

## ⚙️ Kurulum ve Çalıştırma

1. Projeyi klonlayın:
   ```bash
   git clone https://github.com/SevilayOnogul/InsureYouAI.git
2. `appsettings.json` dosyasındaki **Connection String** bilgisini güncelleyin.
3. Package Manager Console üzerinden `Update-Database` komutunu çalıştırın.
4. Projeyi çalıştırın: `Ctrl + F5`
