using DataLayer.DbContext;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Seed;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
bool IsInMemory = configuration["ConnectionStrings:InMemory"].ToLower() == "true";
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

var app = builder.Build();
if (IsInMemory)
{
    Console.WriteLine("++++++++++================+++++++++++++++InMemory++++++++++++=============++++++++++");
    //app.SeedInMemoryDb();
}
app.SeedInMemoryDb(IsInMemory);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
