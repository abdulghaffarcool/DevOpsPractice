using Microsoft.EntityFrameworkCore;

namespace DevOpsPractice.Web.Data;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        const int maxRetries = 10;

        using var scope = services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                Console.WriteLine($"Database initialization attempt {attempt}...");
                // =====
                var pending = await context.Database.GetPendingMigrationsAsync();

                Console.WriteLine("Pending migrations:");

                foreach (var migration in pending)
                {
                    Console.WriteLine(migration);
                }

                var applied = await context.Database.GetAppliedMigrationsAsync();

                Console.WriteLine("Applied migrations:");

                foreach (var migration in applied)
                {
                    Console.WriteLine(migration);
                }
                // ====
                await context.Database.MigrateAsync();

                Console.WriteLine("Database is ready.");

                await SeedData.InitializeAsync(services);

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Attempt {attempt} failed.");
                Console.WriteLine(ex.Message);

                if (attempt == maxRetries)
                    throw;

                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }
    }
}