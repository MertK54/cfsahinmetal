using Microsoft.EntityFrameworkCore;
using SahinMetal.Api.Entities;

namespace SahinMetal.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ContactSubmission> ContactSubmissions { get; set; } = null!;
    public DbSet<SiteSetting> SiteSettings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Category-Product relationship
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure JSON column for specifications in MySQL
        modelBuilder.Entity<Product>()
            .Property(p => p.Specifications)
            .HasColumnType("json");

        // Seed default Admin User (Password is 'admin123' hashed using SHA256)
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Username = "admin",
            Email = "admin@sahinmetal.com",
            PasswordHash = "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9",
            Role = "Admin",
            CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });

        // Seed clean categories for references / completed works
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Döküm & Ekipman İmalatı", Slug = "dokum-ekipman-imalati", Description = "Döküm taşıma arabaları, taşıma sepetleri, tartı platformları ve özel döküm ekipmanları." },
            new Category { Id = 2, Name = "Endüstriyel & Fason İmalat", Slug = "endustriyel-fason-imalat", Description = "Çelik çatı, fason kaynak, sulu kesim, kapı, pencere, korkuluk ve yangın merdiveni imalatı." },
            new Category { Id = 3, Name = "Montaj & Saha Hizmetleri", Slug = "montaj-saha-hizmetleri", Description = "Gemi kaynak montaj, fabrikalara endüstriyel çalışmalar, söküm, takım ve elektrot dolgu işleri." }
        );

        // Seed initial real reference projects
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "Döküm Taşıma Arabası İmalatı",
                Slug = "dokum-tasima-arabasi-imalati",
                Description = "Dökümhaneler ve ağır sanayi tesisleri için özel imal edilen yüksek taşıma kapasiteli döküm taşıma arabası.",
                MaterialType = "Ağır Çelik / Döküm Şase",
                Weight = 250.00m,
                Dimensions = "Proje Ölçülerine Göre Özel Üretim",
                Specifications = "{\"Taşıma Kapasitesi\":\"5 - 20 Ton\",\"Kullanım Alanı\":\"Dökümhaneler & Ağır Sanayi\"}",
                ImageUrl = "/images/services/hero_foundry.jpg",
                IsActive = true
            },
            new Product
            {
                Id = 2,
                CategoryId = 2,
                Name = "Fabrika Çelik Çatı Konstrüksiyonu",
                Slug = "fabrika-celik-cati-konstruksiyonu",
                Description = "Endüstriyel tesisler için yüksek dayanımlı çelik çatı konstrüksiyon imalatı ve montajı.",
                MaterialType = "NPU / NPI Çelik Konstrüksiyon",
                Weight = 1200.00m,
                Dimensions = "Proje Çizimine Göre İmalat",
                Specifications = "{\"Statik Dayanım\":\"Rüzgar ve Kar Yükü Hesaplı\"}",
                ImageUrl = "/uploads/service_3.jpg",
                IsActive = true
            },
            new Product
            {
                Id = 3,
                CategoryId = 3,
                Name = "Tersane Gemi Kaynak & Saha Montajı",
                Slug = "tersane-gemi-kaynak-saha-montaji",
                Description = "Tersaneler ve gemi inşa sektörüne özel sertifikalı kaynak ve boru montaj çalışmaları.",
                MaterialType = "Gemi Sacı / Deniz Şartlarına Dayanıklı",
                Weight = 500.00m,
                Dimensions = "Saha Montaj Projesine Göre",
                Specifications = "{\"Sertifika\":\"Loyd Onaylı Kaynak Prosedürü\"}",
                ImageUrl = "/uploads/service_6.jpg",
                IsActive = true
            }
        );

        // Seed site settings
        modelBuilder.Entity<SiteSetting>().HasData(
            new SiteSetting { Key = "companyName", Value = "Şahin Metal" },
            new SiteSetting { Key = "siteTitle", Value = "Şahin Metal - Döküm Taşıma Arabaları, Çelik Çatı & Endüstriyel Kaynak Hizmetleri" },
            new SiteSetting { Key = "siteDescription", Value = "Döküm taşıma arabaları, sepetler, tartı platformları, fason kaynak, sulu kesim, çelik çatı ve gemi kaynak montaj hizmetleri." },
            new SiteSetting { Key = "contactEmail", Value = "info@sahinmetal.com" },
            new SiteSetting { Key = "contactPhone", Value = "0532 628 37 89" },
            new SiteSetting { Key = "contactAddress", Value = "Çayırova Özgürlük Mahallesi, İstiklal Caddesi No: 64, Kocaeli" }
        );
    }
}
