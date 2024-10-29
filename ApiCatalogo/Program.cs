using System.Text.Json.Serialization;
using ApiCatalogo;
using ApiCatalogo.Context;
using ApiCatalogo.Extensions;
using ApiCatalogo.Filters;
using ApiCatalogo.Logging;
using ApiCatalogo.Repositories;
using ApiCatalogo.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
}).AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();



string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");



builder.Services.AddDbContext<AppDbContext>(options =>
options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddTransient<IMeuServico, MeuServico>();


builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfig
{
    LogLevel = LogLevel.Information
}));

var app = builder.Build();


if (!app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Error");
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionsHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Use(async (context, next) => { await next(context); });

app.MapControllers();

// app.Run(async (context) => { await context.Response.WriteAsync("Middlewere final"); });

app.Run();
