using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleWebAppMVC.Data;

namespace SimpleWebAppMVC.Test.Setup
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var sp = services.BuildServiceProvider();

                var env = sp.GetService<IWebHostEnvironment>();

                //TODO change here!

                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(IsLocal(env)
                    ? "Server=<YOURSERVER>;Database=simplewebappdb;Integrated Security=true;MultipleActiveResultSets=True;"
                    : "Server=tcp:<YOURSERVER>;Database=simplewebappdb;Persist Security Info=False;User ID=<YOURUSER>;Password=<YOURPASSWORD>;MultipleActiveResultSets=True;");
                services.AddSingleton(_ => new AppDbContext(optionsBuilder.Options));
            });
        }

        private static bool IsLocal(IWebHostEnvironment env) => env.ContentRootPath.Contains("C:");
    }
}