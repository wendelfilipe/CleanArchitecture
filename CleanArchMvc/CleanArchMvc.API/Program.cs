using CleanArchMvc.Infra.Ioc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

DependencyInjectionAPI.AddInfrastructureAPI(builder.Services, builder.Configuration);

DependencyInjectionJWT.AddInfrastructureJWT(builder.Services, builder.Configuration);

DependencyInjectionSwagger.AddInfrastructureSwagger(builder.Services);

builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();

        });
});
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStatusCodePages();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();

