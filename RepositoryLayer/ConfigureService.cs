using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.IRepository;
using RepositoryLayer.Repository;

namespace RepositoryLayer;

public static class ConfigureService 
{
    public static IServiceCollection ConfigureRepositoryService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<ISheetRepository, SheetRepository>();
        services.AddScoped<ISongRepository, SongRepository>();
        services.AddScoped<IInstrumentRepository, InstrumentRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPlayTrackingRepository, PlayTrackingRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}