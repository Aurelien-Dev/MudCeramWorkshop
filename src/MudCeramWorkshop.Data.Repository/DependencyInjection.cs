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

        //Workers
        services.AddTransient<IProductWorker, ProductWorker>();
        services.AddTransient<IWorkshopWorker, WorkshopWorker>();
        services.AddTransient<IApiWorker, ApiWorker>();

        //DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var sqlServerCs = $"Server=tcp:ceram.database.windows.net,1433;Initial Catalog=sql-db-Ceram;Persist Security Info=False;User ID=ceramadmin;Password=Rxroyr21;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var postgresCs = $"Server=127.0.0.1;Port=5432;Userid=postgres;Password=dd;Protocol=3;SSL=false;Pooling=false;MinPoolSize=1;MaxPoolSize=20;Timeout=15;SslMode=Disable;";

            options.UseSqlServer(sqlServerCs);
            //options.UseNpgsql(postgresCs);
        });

        return services;
    }
}