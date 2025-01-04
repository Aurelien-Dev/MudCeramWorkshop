using MudCeramWorkshop.Data.Domain.InterfacesRepository;
using MudCeramWorkshop.Data.Domain.InterfacesWorker;
using Domain.InterfacesWorker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MudCeramWorkshop.Data.Repository.Repositories;
using MudCeramWorkshop.Data.Repository.Workers;

namespace MudCeramWorkshop.Data.Repository;

public static class DependencyInjection
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        //Repositories
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IMaterialRepository, MaterialRepository>();
        services.AddTransient<IFiringRepository, FiringRepository>();
        services.AddTransient<IWorkshopRepository, WorkshopRepository>();
        services.AddTransient<IImageInstructionRepository, ImageInstructionRepository>();
        services.AddTransient<IProductFiringRepository, ProductFiringRepository>();
        services.AddTransient<IProductMaterialRepository, ProductMaterialRepository>();


        //Workers
        services.AddTransient<IProductWorker, ProductWorker>();
        services.AddTransient<IWorkshopWorker, WorkshopWorker>();
        services.AddTransient<IApiWorker, ApiWorker>();

        //DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var postgresCs = "Host=172.26.0.3;Port=5432;Database=CeramWorkshopDb;Username=pguser;Password=PGUserPwd";
            options.UseNpgsql(postgresCs);
        }, ServiceLifetime.Transient);

        return services;
    }
}
