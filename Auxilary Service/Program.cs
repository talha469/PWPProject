var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger to read the YAML file
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});

// Add CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Serve the YAML file manually
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Configure Swagger UI to use JSON documentation
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the root of the application
    });

    // Map endpoint to serve the YAML file
    app.MapGet("/swagger/v1/swagger.yaml", async context =>
    {
        var yamlFilePath = Path.Combine(AppContext.BaseDirectory, "Swagger", "openapi.yaml");
        if (File.Exists(yamlFilePath))
        {
            var yamlContent = await File.ReadAllTextAsync(yamlFilePath);
            context.Response.ContentType = "application/x-yaml";
            await context.Response.WriteAsync(yamlContent);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync("YAML file not found");
        }
    });
}

// Use CORS middleware
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.UseAuthorization(); // Uncomment if you have authorization configured

app.MapControllers();

await app.RunAsync();