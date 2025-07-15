using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using System.Diagnostics;

namespace MovieApi.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<MovieApiContext>();

                //Can i connect to the database? If not, delete and recreate it
                if (!await context.Database.CanConnectAsync())
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }

                try
                {
                    await SeedData.InitAsync(context);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"An error occurred while seeding the database: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
