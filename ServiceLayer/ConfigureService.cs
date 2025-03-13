using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.IRepository;
using ServiceLayer.Services;
using ServiceLayer.Services.Implementation;
using ServiceLayer.Services.Implementation.Db;
using ServiceLayer.Services.Interface;
using ServiceLayer.Services.Interface.Db;

namespace ServiceLayer;

public static class ConfigureService
{
    public static IServiceCollection ConfigureServiceService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<ISongService, SongService>();
        services.AddScoped<IArtistService, ArtistService>();
        services.AddScoped<IInstrumentService, InstrumentService>();
        services.AddScoped<ISheetService, SheetService>();
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISystemService, SystemService>();
        services.AddScoped<IServiceWrapper, ServiceWrapper>();
        services.AddScoped<IPlayTrackingService, PlayTrackingService>();
        return services;
    }
}