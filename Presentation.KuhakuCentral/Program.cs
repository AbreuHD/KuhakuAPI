using Infraestructure.Shared;
using Core.Application;
using Infrastructure.Identity;
using Infraestructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using KuhakuCentral.Extensions;
using Swashbuckle.AspNetCore.SwaggerUI;
using Infrastructure.Identity.Entities;
using Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication();
builder.Services.AddSharedInfraestructure(builder.Configuration);
builder.Services.AddKhakuLayer(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddPersistenceInfraestructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.Seed(userManager, roleManager);
        await DefaultOwner.Seed(userManager, roleManager);
        await DefaultUser.Seed(userManager, roleManager);
    }
    catch (Exception ex)
    {

    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Kuhaku API");
        options.DefaultModelRendering(ModelRendering.Model);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
