using Application.Data.Context;
using Application.Data.Repositories;
using Application.Domain.Interfaces.Repositories;
using Application.Domain.Interfaces.Services;
using Application.Domain.Services;
using Application.Web.UI.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Web.UI.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<CommerceDbContext>();
            services.AddScoped<IdentityContextDb>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<INotifierService, NotifierService>();

            services.AddDbContext<CommerceDbContext>(option => option
                                    .UseSqlServer(configuration.GetConnectionString("CommerceDbContext")));
            
            services.AddDbContext<IdentityContextDb>(option => option
                                    .UseSqlServer(configuration.GetConnectionString("IdentityContext")));

            return services;
        }
    }
}
