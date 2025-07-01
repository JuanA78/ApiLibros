using Uttt.Micro.Libro.Extenciones;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar la política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policyBuilder =>
    {
        policyBuilder
             .WithOrigins(
                "http://localhost:3000",           // Desarrollo local
                "https://miapp.vercel.app"         // Producción en Vercel
            ) 
            .AllowAnyMethod()                      // GET, POST, PUT, DELETE...
            .AllowAnyHeader()                      // Headers permitidos
            .AllowCredentials();                   // Permite cookies o auth si usas
    });
});

// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomServices(builder.Configuration);

var app = builder.Build();

// Middleware para desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2. Aplicar CORS **ANTES** de usar Authorization y MapControllers
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
