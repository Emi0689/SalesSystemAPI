using Microsoft.AspNetCore.Localization;
using SalesSystem.IOC;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.DependencyInjections(builder.Configuration);

///////CULTURE INFO////////
var defaultCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

// (Opcional for request HTTP)
var supportedCultures = new[] { defaultCulture };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(defaultCulture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
//////////////////


builder.Services.AddCors(op => { op.AddPolicy("Policy", app => app.AllowAnyHeader().AllowAnyOrigin().AllowAnyOrigin().AllowAnyMethod()); });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("Policy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
