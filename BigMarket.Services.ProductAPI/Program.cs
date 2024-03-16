using AutoMapper;
using BigMarket.Services.ProductAPI;
using BigMarket.Services.ProductAPI.Data;
using BigMarket.Services.ProductAPI.Extensions;
using BigMarket.Services.ProductAPI.Services;
using BigMarket.Services.ProductAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(optiion =>
{
    optiion.UseSqlServer(builder.Configuration.GetConnectionString("ProductConnection"));
});
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();

//Enable versioning in Web API controllers
builder.Services.AddApiVersioning(config =>
{
    config.ApiVersionReader = new UrlSegmentApiVersionReader();
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.AddSwaggerConfiguration();
builder.AddAppAuthentication();

builder.Services.AddAuthentication();

//CORS: localhost:7000, localhost:5278
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
        .WithOrigins(builder.Configuration.GetSection("AllowedOrigins7000").Get<string[]>())
        .WithMethods("GET", "POST", "PUT", "DELETE");
    });

    options.AddPolicy("5278Client", policyBuilder =>
    {
        policyBuilder
        .WithOrigins(builder.Configuration.GetSection("AllowedOrigins5278").Get<string[]>())
        .WithMethods("GET");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");
    }); //creates swagger UI for testing all Web API endpoints / action methods);
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API");
        c.RoutePrefix = string.Empty;
    });
}



app.UseHttpsRedirection();
app.UseAuthentication();

app.UseCors();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
ApplayMigration();
app.Run();

void ApplayMigration()
{
    if (!builder.Environment.IsEnvironment("Test"))
    {
        using var scope = app.Services.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (_db.Database.GetPendingMigrations().Any())
        {
            _db.Database.Migrate();
        }
    }
}
public partial class Program { } //make the auto-generated Program accessible programmatically
