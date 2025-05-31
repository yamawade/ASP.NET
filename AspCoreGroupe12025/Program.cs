using AspCoreGroupe12025.Entities;
using AspCoreGroupe12025.Helpers;
using AspCoreGroupe12025.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
var env = builder.Environment;

builder.Services.AddDbContext<DataContext>();

builder.Services.AddControllers().AddJsonOptions(x =>
{
// serialize enums as strings in api responses (e.g. Role)
x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
// ignore omitted parameters on models to enable optional params (e.g. User update)
 x.JsonSerializerOptions.DefaultIgnoreCondition =
JsonIgnoreCondition.WhenWritingNull;
 });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// configure DI for application services
services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClientService, ClientService>();


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

app.Run();
