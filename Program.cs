using Microsoft.EntityFrameworkCore;
using SunNxtBackend;
using SunNxtBackend.Repositories;
using SunNxtBackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

// Add DB Connection 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
builder.Services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
builder.Services.AddScoped<IAgeRangeRepository, AgeRangeRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IAppUserService, AppUserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


using var scope = app.Services.CreateScope();
var dbInit = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
dbInit.Initialize().GetAwaiter().GetResult();

app.Run();
