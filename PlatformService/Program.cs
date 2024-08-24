using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ICommandDataClient, HttpCommandDataClient>();


// if (builder.Environment.IsDevelopment())
// {
//     Console.WriteLine("--> Using InMem Db");
//     builder.Services.AddDbContext<AppDbContext>(opt =>
//          opt.UseInMemoryDatabase("InMem"));
// }
// else
// {
//     Console.WriteLine("--> Using SqlServer Db");
//     builder.Services
//            .AddDbContext<AppDbContext>(opt =>
//            opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
// }
builder.Services
           .AddDbContext<AppDbContext>(opt =>
           opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//PrepDb.PrepPopulation(app, app.Environment.IsProduction());
Console.WriteLine($"--> Listining Command service at endpoint :" + builder.Configuration["CommandService"]);
app.Run();
