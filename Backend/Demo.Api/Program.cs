using Demo.Api.Extensions;
using Demo.Core;
using Demo.Domain.Utilities;
using Demo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureCorsPolicy(builder.Configuration); // form Api/Extensions

builder.Services.InjectDependence(builder.Configuration);  // from Infrastructure 
builder.Services.RegisterServices(); // form Core
builder.Services.MongoDbIdentityConfig(builder.Configuration); // form Core

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors(Constants.AppSettings.CorsPolicy);
app.UseCors("AllowOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
