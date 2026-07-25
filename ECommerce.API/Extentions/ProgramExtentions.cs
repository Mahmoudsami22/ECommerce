using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Extentions
{
    public static class ProgramExtentions
    {
        public static async Task MigrationAndSeedAsnyc(this WebApplication app)
        {
            var scope = app.Services.CreateScope();

            var DbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var CatalogLogger = scope.ServiceProvider.GetRequiredService<ILogger<CatalogDataSeeder>>();

            var pending = await DbContext.Database.GetPendingMigrationsAsync();

            if (pending.Count() > 0)
                await DbContext.Database.MigrateAsync();

            CatalogDataSeeder catalogDataSeeder = new CatalogDataSeeder(DbContext, CatalogLogger);

            await catalogDataSeeder.SeedAsync();
        }
    }
}
