using JonasHendrickx.Shop.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using JonasHendrickx.Shop.DataContext.Context;
using JonasHendrickx.Shop.Infrastructure.Contracts;
using JonasHendrickx.Shop.Infrastructure.Repositories;
using JonasHendrickx.Shop.Services;
using Microsoft.EntityFrameworkCore;

namespace JonasHendrickx.Shop.Api
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
            // Can be replaced with actual database here. For demo purposes, it was easier to use an in-memory database. :-)
            services.AddDbContext<ShopDbContext>(opt => opt.UseInMemoryDatabase("shop"));
            services.AddTransient<ShopDbContext>();
            services.AddTransient<IBasketRepository, BasketRepository>();
            services.AddTransient<IDiscountRepository, DiscountRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductListingRepository, ProductListingRepository>();
            services.AddTransient<IBasketService, BasketService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductListingService, ProductListingService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JonasHendrickx.Shop.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JonasHendrickx.Shop.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
