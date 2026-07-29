using ECommerce.Domain.Contracts;
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
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            await seeder.SeedAsync();

            var IdentitySeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Identity");
            await IdentitySeeder.SeedAsync();
        }
    }
}
