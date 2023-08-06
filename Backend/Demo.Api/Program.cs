using Demo.Core;
using Demo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InjectDependence(builder.Configuration);  // from Infrastructure //
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
