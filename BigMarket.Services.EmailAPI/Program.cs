using BigMarket.Services.EmailAPI.Data;
using BigMarket.Services.EmailAPI.Extentions;
using BigMarket.Services.EmailAPI.Messaging;
using BigMarket.Services.EmailAPI.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(optiion =>
{
    optiion.UseSqlServer(builder.Configuration.GetConnectionString("EmailConnection"));
});

var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("EmailConnection"));
builder.Services.AddSingleton(new EmailService(optionBuilder.Options)); // Becouse EmailService is singleton

builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();
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
ApplayMigration();
app.UseAzureServiceBusConsumer();
app.Run();

void ApplayMigration()
{
    using var scope = app.Services.CreateScope();
    var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (_db.Database.GetPendingMigrations().Any())
    {
        _db.Database.Migrate();
    }
}
