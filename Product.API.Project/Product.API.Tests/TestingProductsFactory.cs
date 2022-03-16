using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Product.API.Project.Data;
using System.Linq;

namespace Product.API.Tests
{
    public class TestingProductsFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ProductContext>));

                services.Remove(descriptor);

                services.AddDbContext<ProductContext>((options, context) =>
                {
                    context.UseSqlServer(Configuration.GetConnectionString("ProductContextDb"));
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ProductContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<TestingProductsFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                }
            });
        }
    }
}
