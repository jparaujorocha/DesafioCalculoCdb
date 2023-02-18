using DesafioCalculoCdb.Application.Interfaces;
using DesafioCalculoCdb.Application.Mappings;
using DesafioCalculoCdb.Application.Services;
using DesafioCalculoCdb.Domain.Interfaces;
using DesafioCalculoCdb.Infra.Data.Context;
using DesafioCalculoCdb.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioCalculoCdb.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
            ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IImpostoInvestimentoRepository, ImpostoInvestimentoRepository>();
            services.AddScoped<IInvestimentoRepository, InvestimentoRepository>();
            services.AddScoped<IImpostoRepository, ImpostoRepository>();
            services.AddScoped<IImpostoInvestimentoService, ImpostoInvestimentoService>();
            services.AddScoped<IInvestimentoService, InvestimentoService>();
            services.AddScoped<IImpostoService, ImpostoService>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}
