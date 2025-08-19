using System.Text.Json.Serialization;
using application;
using application.Contracts.Infrastructure;
using application.Features.Products.Queries.GetProductList;
using application.Middlewares;
using infrastructure;
using infrastructure.ImageCloudinary;
using infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServicesProduct(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddDbContext<ProductDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("UrlSqlite"),
        b => b.MigrationsAssembly(typeof(ProductDbContext).Assembly.FullName));
});

builder.Services.AddMediatR(typeof(GetProductListQueryHandler).Assembly);

//Registro de imagenes
builder.Services.AddScoped<IManageImageService, ManageImageService>();

builder.Services.AddHttpClient("TransactionsService", client =>
{
    client.BaseAddress = new Uri("http://localhost:5001/api/v1/transaction/");
    client.DefaultRequestHeaders.Accept.Add(
        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin();
        corsPolicyBuilder.AllowAnyMethod();
        corsPolicyBuilder.AllowAnyHeader();
    });
});


builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = service.GetRequiredService<ProductDbContext>();
        await context.Database.MigrateAsync();

        //Insertar Data de prueba
        await EcommerceDbContextData.LoadDataAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Error en la migración");
    }
}

app.Run();