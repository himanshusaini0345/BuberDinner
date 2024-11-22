using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();

    // Add Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "BuberDinner API",
            Version = "v1",
            Description = "API documentation for the BuberDinner application"
        });
    });

    builder.Services.AddApplication()
                    .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    // Enable Swagger
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
