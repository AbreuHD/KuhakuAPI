using Auth.Infraestructure.Identity;
using Core.Application;
using Core.Application.Enums;
using Infrastructure.Persistence;
using KuhakuCentral.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication();
builder.Services.AddKhakuLayer(builder.Configuration);
builder.Services.AddPersistenceInfraestructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.AddIdentityInfrastructure(builder.Configuration);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

//new
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressInferBindingSourcesForParameters = true;
    options.SuppressMapClientErrors = false;
});
builder.Services.AddHealthChecks();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddApiVersioningExtension();
builder.Services.AddSwaggerExtension();
builder.Services.AddHttpClient();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await services.AddIdentityRolesAsync(Enum.GetNames<Roles>());
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseHttpsRedirection();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Kuhaku API");
        options.DefaultModelRendering(ModelRendering.Model);
    });

}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

//app.UseHttpsRedirection();
app.UseSwagger(options =>
{
    options.RouteTemplate = "/openapi/{documentName}.json";
});

app.MapScalarApiReference();
app.UseAuthorization();

app.MapControllers();

app.Run();
