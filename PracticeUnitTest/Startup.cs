using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PracticeUnitTest.Provider;
using PracticeUnitTest.Provider.Implementations;
using System;
using Microsoft.EntityFrameworkCore;
using PracticeUnitTest.Models;
using PracticeUnitTest.Repository;

namespace PracticeUnitTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BlogDBContext>(item => item.UseSqlServer(Configuration.GetConnectionString("BlogDBConnection")));
            services.AddScoped<IPostRepository, PostRepository>();

            services.AddSingleton<ISystemApiProvider, SystemApiProvider>();

            SetupHttpClient(services);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void SetupHttpClient(IServiceCollection services)
        {
            var mailIdApiClientBuilder = services.AddHttpClient<SystemApiProvider>(typeof(SystemApiProvider).FullName, c =>
            {
                c.BaseAddress = new Uri("https://github.com");
                c.Timeout = TimeSpan.FromSeconds(15);
            });
            mailIdApiClientBuilder.SetHandlerLifetime(TimeSpan.FromSeconds(60));
        }
    }
}