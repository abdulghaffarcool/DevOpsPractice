namespace DevOpsPractice.Web.Data;

using DevOpsPractice.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

// Console.WriteLine("Connection String:");
// Console.WriteLine(context.Database.GetConnectionString());

//         await context.Database.MigrateAsync();

        // Create Role
        const string roleName = "Administrator";

        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        // Create Admin User
        const string email = "admin@devopspractice.com";
        const string password = "Admin@123";

        var admin = await userManager.FindByEmailAsync(email);

        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(admin, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, roleName);
            }
        }

        // Seed Products
        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product { Name = "Laptop", Description = "Dell Laptop", Price = 1200, CreatedDate = DateTime.UtcNow },
                new Product { Name = "Keyboard", Description = "Mechanical Keyboard", Price = 90, CreatedDate = DateTime.UtcNow },
                new Product { Name = "Mouse", Description = "Wireless Mouse", Price = 35, CreatedDate = DateTime.UtcNow },
                new Product { Name = "Monitor", Description = "27 inch Monitor", Price = 300, CreatedDate = DateTime.UtcNow },
                new Product { Name = "Headphones", Description = "Noise Cancelling", Price = 150, CreatedDate = DateTime.UtcNow },
                new Product { Name = "Webcam", Description = "1080p Webcam", Price = 75, CreatedDate = DateTime.UtcNow },
                new Product { Name = "USB Hub", Description = "USB-C Hub", Price = 45, CreatedDate = DateTime.UtcNow },
                new Product { Name = "SSD", Description = "1TB NVMe SSD", Price = 110, CreatedDate = DateTime.UtcNow },
                new Product { Name = "Microphone", Description = "USB Microphone", Price = 80, CreatedDate = DateTime.UtcNow },
                new Product { Name = "Docking Station", Description = "Laptop Dock", Price = 180, CreatedDate = DateTime.UtcNow }
            );

            await context.SaveChangesAsync();
        }
    }
}