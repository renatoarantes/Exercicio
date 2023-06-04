using MediatR;
using Microsoft.OpenApi.Models;
using Questao5.Application.Queries;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;
using Questao5.Infrastructure.Database.CommandStore;
using Questao5.Infrastructure.Database.QueryStore;
using Questao5.Infrastructure.Sqlite;
using Questao5.Infrastructure.Swagger;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(null, false));
}); ;

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

// queries
builder.Services.AddTransient<IQuery<string, SaldoResponse>, SaldoQuery>();

// commandStore
builder.Services.AddTransient<ICommandStore<Movimento>, MovimentoCommandStore>();
builder.Services.AddTransient<ICommandStore<IdEmPotencia>, IdEmPotenciaCommandStore>();

// queryStore
builder.Services.AddTransient<IQueryStore<ContaCorrente, string>, ContaCorrenteQueryStore>();
builder.Services.AddTransient<IQueryStore<IdEmPotencia, string>, IdEmPotenciaQueryStore>();
builder.Services.AddTransient<IQueryStore<double, string>, SaldoQueryStore>();

// data
builder.Services.AddScoped<DbSession>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Conta Bancária", 
                                         Version = "v1",
                                         Description = "Esta API é a Questão 5 do Teste",
                                         Contact = new OpenApiContact
                                         {
                                             Name = "Renato Arantes",
                                             Email = "renato.m.arantes@gmail.com",
                                         }
                                        });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.SchemaFilter<EnumTypesSchemaFilter>(xmlPath);
});

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

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html


