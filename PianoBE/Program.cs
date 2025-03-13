using System.Reflection;
using DataLayer.DbContext;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using ServiceLayer.Seed;
using ServiceLayer.Services.Implementation;
using ServiceLayer.Services.Interface;
using System.Text;
using System.Text.Json;
using API.Middleware;
using RepositoryLayer;
using RepositoryLayer.IRepository;
using RepositoryLayer.Repository;
using ServiceLayer;
using ServiceLayer.Filter;
using ServiceLayer.CustomException;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

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
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.SameSite = SameSiteMode.Lax; // Use Lax or Strict for local development
    options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Allow insecure cookies for local development
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    options.Scope.Add("profile");
    options.Scope.Add("email");
    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
    options.CorrelationCookie.SameSite = SameSiteMode.Lax; // Use Lax or Strict for local development
    options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.None; // Allow insecure cookies for local development
    options.CallbackPath = "/login/oauth2/code/google";
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
    options.Secure = CookieSecurePolicy.None; // Allow insecure cookies for local development
});

builder.Services.ConfigureServiceService(builder.Configuration);
builder.Services.ConfigureRepositoryService(builder.Configuration);

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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    #region jwt ui
    string SecurityId = "Jwt Bearer";
    
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Piano API", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
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
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#region cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("allcors", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});
#endregion
var app = builder.Build();
if (IsInMemory)
{
    Console.WriteLine("++++++++++================+++++++++++++++InMemory++++++++++++=============++++++++++");
    //app.SeedInMemoryDb();
}
// Configure the HTTP request pipeline.
app.UseCookiePolicy();
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
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
app.UseCors("allcors");
app.Run();
