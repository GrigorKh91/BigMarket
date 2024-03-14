using AutoMapper;
using BigMarket.Services.CouponAPI;
using BigMarket.Services.CouponAPI.Data;
using BigMarket.Services.CouponAPI.Extensions;
using BigMarket.Services.CouponAPI.Middleware;
using BigMarket.Services.CouponAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration
    .ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
    .ReadFrom.Services(services); //read out current app's services and make them available to serilog
});

builder.Services.AddDbContext<AppDbContext>(optiion =>
{
    optiion.UseSqlServer(builder.Configuration.GetConnectionString("CouponConnection"));
});
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICouponService, BigMarket.Services.CouponAPI.Services.CouponService>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
    options.Filters.Add(new ConsumesAttribute("application/json"));
})
 .AddXmlSerializerFormatters();
//var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<ResponseHeaderActionFilter>>();
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add(new ResponseHeaderActionFilter(logger, "My-Key-From-Global", "My-ValueTask-from-Global"));
//});// Add Global Filter

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.AddSwaggerConfiguration();

builder.AddAppAuthentication();

builder.Services.AddAuthentication();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coupon API");
        c.RoutePrefix = string.Empty;
    });
    app.UseExceptionHandlingMiddleware();
}


StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
ApplayMigration();
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
