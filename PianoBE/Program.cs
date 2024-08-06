using DataLayer.DbContext;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using ServiceLayer.CustomException;
using ServiceLayer.Filter;
using ServiceLayer.Seed;
using ServiceLayer.Services.Implementation;
using ServiceLayer.Services.Interface;
using System.Text;
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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero,
    };
});
#region service and repo
builder.Services.AddScoped<IServiceWrapper, ServiceWrapper>();
#endregion

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelAttribute>();
    options.Filters.Add<CustomExceptionFilter>();
})
        .ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                throw new InvalidModelStateException(context.ModelState);
            };
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    #region jwt ui
    string SecurityId = "Jwt Bearer";

    options.AddSecurityDefinition(SecurityId, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,  //ko xài .ApiKey vì .http sẽ ko cần gõ Bearer
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization using the Bearer scheme. \"Bearer\" is not needed.Just paste the jwt"
    }
                   );
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = SecurityId
                            }
                        },
                        new string[]{}
                    }
                }
    );
    #region google auth ui

    //var securityScheme = new OpenApiSecurityScheme
    //{
    //    Name = "Authorization",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.OAuth2,
    //    Flows = new OpenApiOAuthFlows()
    //    {
    //        Implicit = new OpenApiOAuthFlow()
    //        {
    //            AuthorizationUrl = new Uri(configuration["Authentication:Google:Web:auth_uri"]/*"https://accounts.google.com/o/oauth2/v2/auth"*/),
    //            Scopes = new Dictionary<string, string> {
    //                { "openid", "Allow this app to get some basic account info" },
    //                { "email", "email" },
    //                { "profile", "profile" }
    //            },

    //            TokenUrl = new Uri(configuration["Authentication:Google:Web:token_uri"])
    //        }
    //    },
    //    Extensions = new Dictionary<string, IOpenApiExtension>
    //    {
    //        {"x-tokenName", new OpenApiString("id_token")}
    //    },
    //};

    //options.AddSecurityDefinition("abc", securityScheme);

    //var securityRequirements = new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Type = ReferenceType.SecurityScheme,
    //                Id = "abc"
    //            }
    //        },
    //        new List<string> {"openid", "email", "profile"}
    //    }
    //};

    //options.AddSecurityRequirement(securityRequirements);
    #endregion
    #endregion
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("allcors", builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed(hostName => true));

});
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

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("allcors");

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
