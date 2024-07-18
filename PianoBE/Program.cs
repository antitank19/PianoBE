using DataLayer.DbContext;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Seed;
using ServiceLayer.Services.Implementation;
using ServiceLayer.Services.Interface;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
bool IsInMemory = configuration["ConnectionStrings:InMemory"].ToLower() == "true";
bool SeedOnStartUp = configuration["ConnectionStrings:SeedOnStartUp"].ToLower() == "true";
// Add services to the container.
#region dbContext
builder.Services.AddDbContext<PianoContext>(options =>
{
    options.EnableSensitiveDataLogging();
    //options.EnableRetryOnFailure();
    //options.EnableRetryOnFailure
    if (IsInMemory)
    {
        options.UseInMemoryDatabase("PianoDb");
    }
    else
    {
        Console.WriteLine(configuration.GetConnectionString("Default"));
        options.UseSqlServer(configuration.GetConnectionString("Default"), o =>
        {
            o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        });
    }
});
#endregion

builder.Services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = false)
//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    //.AddRoles<Role>()
    .AddEntityFrameworkStores<PianoContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

#region service and repo
builder.Services.AddScoped<IServiceWrapper, ServiceWrapper>();
#endregion

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#region cors
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("allcors", builder => builder
//        .AllowAnyMethod()
//        .AllowAnyHeader()
//        .AllowCredentials()
//        .SetIsOriginAllowed(hostName => true));

//});
#endregion
var app = builder.Build();
if (IsInMemory)
{
    Console.WriteLine("++++++++++================+++++++++++++++InMemory++++++++++++=============++++++++++");
    //app.SeedInMemoryDb();
}
// Configure the HTTP request pipeline.
app.UseSwagger();       
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        await ApplicationDbInitializer.SeedRolesAndUsers(services);
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred seeding the DB.");
//    }
//}

app.SeedInMemoryDb(IsInMemory, SeedOnStartUp);

app.Run();
