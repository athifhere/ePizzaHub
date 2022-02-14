using ePizzaHub.DAL;
using ePizzaHub.DAL.Implementaions;
using ePizzaHub.DAL.Interfaces;
using ePizzaHub.Entities;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ePizzaHub.Services.Configuration
{
    public static class ConfigureDependencies
    {
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();

            services.AddScoped<DbContext, DatabaseContext>();


            //repositories
            services.AddScoped<IRepository<Item>, Repository<Item>>();
            services.AddScoped<IRepository<ItemType>, Repository<ItemType>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            //services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICatalogService, CatalogService>();
        }
    }
}
