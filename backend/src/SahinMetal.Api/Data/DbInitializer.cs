using Microsoft.EntityFrameworkCore;
using SahinMetal.Api.Entities;

namespace SahinMetal.Api.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();
        
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync();

            // Ensure default admin password hash is correctly set
            var adminUser = await context.Users.FirstOrDefaultAsync(u => u.Username == "admin");
            if (adminUser != null)
            {
                adminUser.PasswordHash = "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9";
                await context.SaveChangesAsync();
            }
            else
            {
                context.Users.Add(new User
                {
                    Username = "admin",
                    Email = "admin@sahinmetal.com",
                    PasswordHash = "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9",
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow
                });
                await context.SaveChangesAsync();
            }

            // Replace legacy service image URLs with standard path
            var legacyProducts = await context.Products
                .Where(p => p.ImageUrl == "/uploads/service_1.jpg" || p.ImageUrl == "/images/services/service_1.jpg")
                .ToListAsync();

            if (legacyProducts.Any())
            {
                foreach (var p in legacyProducts)
                {
                    p.ImageUrl = "/images/services/hero_foundry.jpg";
                }
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Veritabanı ilklendirilirken ve seed verisi oluşturulurken bir hata meydana geldi.");
        }
    }
}
