using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ABB.NTier.WebApi.Utils
{
    public static class Extensions
    {
        public static void EnsureDatabaseMigrate<T>(this IApplicationBuilder app) where T : DbContext
        {
            Thread.Sleep(1000);

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<T>();
                context.Database.Migrate();
            }
        }
    }
}
