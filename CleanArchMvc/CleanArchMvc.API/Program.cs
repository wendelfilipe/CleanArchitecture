using CleanArchMvc.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DependencyInjectionAPI.AddInfrastructureAPI(builder.Services, builder.Configuration);

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
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();

