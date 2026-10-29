# Şahin Metal — Kurumsal Web Sitesi & Yönetim Paneli

Şahin Metal (Metal Sanayi & İmalat) için geliştirilmiş modern, %100 mobil uyumlu, yüksek performanslı ve güvenli kurumsal web platformu ve içerik yönetim sistemi (CMS).

---

## 🛠️ Teknoloji Yığını (Tech Stack)

### Backend (.NET Web API)
- **Framework:** .NET 9.0 (C#)
- **Veritabanı:** MySQL / MariaDB (Pomelo.EntityFrameworkCore.MySql)
- **Kimlik Doğrulama:** JWT (JSON Web Tokens)
- **Güvenlik:** EF Core LINQ parametreli sorguları (SQL Injection koruması), XSS ve girdi temizleme yardımcısı (`SecurityHelper`), HTTP güvenlik başlıkları (`nosniff`, `SAMEORIGIN`, `XSS Protection`).

### Frontend (SPA)
- **Framework:** Vue 3 (Composition API / `<script setup>`)
- **Derleyici & Build Tool:** Vite
- **Stil & Arayüz:** Modern CSS3, Bootstrap 5, Bootstrap Icons, Plus Jakarta Sans & Inter tipografisi
- **Yönlendirme & Durum:** Vue Router 4, Composables (`useTheme` - Açık/Koyu Tema desteği)
- **SEO & Head Yönetimi:** `@unhead/vue`

---

## 📁 Proje Dizin Yapısı

```
SahinMetal/
├── backend/
│   ├── src/
│   │   └── SahinMetal.Api/       # .NET Core Web API projesi
│   │       ├── Controllers/      # API uç noktaları (Ürünler, Mesajlar, Auth)
│   │       ├── Services/         # İş mantığı ve veri erişim katmanı
│   │       ├── Entities/         # Veritabanı modelleri
│   │       ├── DTOs/             # Veri transfer nesneleri ve doğrulamalar
│   │       ├── Common/Helpers/   # Güvenlik ve slug yardımcıları
│   │       └── appsettings.json  # Yapılandırma ayarları
│   └── schema.sql                # MySQL veritabanı şeması ve başlangıç verisi
├── frontend/
│   ├── src/
│   │   ├── views/                # Sayfalar (Public & Admin Dashboard)
│   │   ├── services/             # Axios API istemcisi
│   │   ├── composables/          # Tema ve paylaşılan durum mantığı
│   │   ├── utils/                # Girdi maskeleme ve güvenlik araçları
│   │   └── assets/styles.css     # Kurumsal renk paleti ve responsive stiller
│   └── package.json              # Frontend bağımlılıkları ve çalıştırma komutları
└── README.md
```

---

## 🚀 Kurulum ve Çalıştırma

### 1. Ön Gereksinimler
- [.NET 9 SDK](https://dotnet.microsoft.com/)
- [Node.js](https://nodejs.org/) (v18 veya üzeri)
- [MySQL Server](https://dev.mysql.com/downloads/) (XAMPP, WampServer veya yerel MySQL)

---

### 2. Veritabanı Kurulumu
1. MySQL servisinizi başlatın.
2. `sahin_metal_db` adında yeni bir veritabanı oluşturun:
   ```sql
   CREATE DATABASE sahin_metal_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
   ```
3. `backend/schema.sql` dosyasını bu veritabanında çalıştırın veya projeyi ilk kez başlattığınızda Entity Framework Core otomatik olarak tabloları ve varsayılan yönetici kullanıcısını oluşturacaktır.

---

### 3. Backend'i Başlatma
```bash
cd backend/src/SahinMetal.Api

# Gerekirse appsettings.json dosyasındaki MySQL bağlantı dizesini düzenleyin:
# "DefaultConnection": "server=127.0.0.1;port=3306;database=sahin_metal_db;user=root;password=sifreniz"

dotnet run
```
*API Varsayılan Olarak:* `http://localhost:5164` adresinde çalışacaktır.

---

### 4. Frontend'i Başlatma
Yeni bir terminal sekmesinde:
```bash
cd frontend

# Paketleri yükleyin
npm install

# Geliştirme sunucusunu başlatın
npm run dev
# (veya alternatif olarak: npm run serve)
```
*Web Arayüzü:* `http://localhost:5173` adresinde açılacaktır.

---

## 🔐 Yönetici Paneli Giriş Bilgileri

- **Giriş URL:** `http://localhost:5173/login`
- **Kullanıcı Adı:** `admin`
- **Şifre:** `admin123`

---

## ✨ Öne Çıkan Özellikler

1. **%100 Responsive & Mobil Odaklı Arayüz:** Akıllı telefonlardan büyük monitörlere kadar sorunsuz, dokunmatik uyumlu deneyim.
2. **Karanlık / Aydınlık Mod (Dark Mode):** Göz yormayan, sistem ve kullanıcı tercihini hafızada tutan çift tema desteği.
3. **Akıllı Hero Slider:** CSS Grid tabanlı sıfır sıçramalı (zero layout shift) ve dokunmatik swipe özellikli slayt geçişleri.
4. **Güvenli İletişim Formu:**
   - Harf girişini engelleyen, Türkiye cep telefonu standardına göre otomatik formatlanan numara alanı.
   - İstemci ve sunucu tarafında çift katmanlı SQL Injection & XSS filtreleme.
5. **Yönetici Paneli (CMS):**
   - Yapılan iş / referans ekleme, düzenleme ve silme.
   - 5MB limitli görsel yükleme ve otomatik görsel önizleme.
   - Müşterilerden gelen teklif mesajlarını anında okuma, tek tıkla arama veya WhatsApp'tan yanıt verme.
