using System.Text.Json.Serialization;
using application;
using application.Features.Transactions.Queries.GetTransactionsById;
using application.Middlewares;
using infrastructure;
using infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructureServicesTransaction(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddDbContext<TransactionDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("UrlSqlite"),
        b => b.MigrationsAssembly(typeof(TransactionDbContext).Assembly.FullName));
});

builder.Services.AddMediatR(typeof(GetTransactionsByIdQueryHandler).Assembly);

builder.Services.AddHttpClient("ProductService", client =>
{
    client.BaseAddress = new Uri("http://localhost:5002/api/v1/product/");
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

app.Run();