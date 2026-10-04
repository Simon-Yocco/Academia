using Application.Services;
using Data;
using WebAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =========================================================================
// 1. INYECCIÓN DE DEPENDENCIAS (Servicios Base)
// =========================================================================
// Le decimos a la API qué clase concreta instanciar cuando alguien pide una interfaz
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<ICursoService, CursoService>();

builder.Services.AddScoped<IEspecialidadRepository, EspecialidadRepository>();
builder.Services.AddScoped<IEspecialidadService, EspecialidadService>();

builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();
builder.Services.AddScoped<IMateriaService, MateriaService>();

builder.Services.AddScoped<IComisionRepository, ComisionRepository>();
builder.Services.AddScoped<IComisionService, ComisionService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<Application.Services.AuthService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// =========================================================================
// 2. CONFIGURACIÓN DE SWAGGER (Documentación y pruebas de la API)
// =========================================================================
// Configuramos Swagger para que incluya soporte para tokens JWT (Bearer)
builder.Services.AddSwaggerGen(c =>
{
    // Creamos el botón "Authorize" en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Escribí la palabra 'Bearer', dejá un espacio, y luego pegá tu token. Ejemplo: \"Bearer eyJhbGci...\"",
        Name = "Authorization", // Nombre del encabezado HTTP
        In = ParameterLocation.Header, // El token viaja en el encabezado
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    // Obligamos a que Swagger envíe el token en cada petición a la API
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

// =========================================================================
// 3. CONFIGURACIÓN DE SEGURIDAD (Autenticación y Autorización)
// =========================================================================
// Valores del "contrato" del Token. (En producción esto va en appsettings.json o User Secrets)
var secretKey = "TPI-JWT-Secret-Key-256-bits-long-for-development-only-never-use-in-production"; // Firma digital
var issuer = "TPI-API"; // Quién lo emite
var audience = "TPI-Clients"; // Quién lo consume

// Enseñamos al servidor cómo leer, desencriptar y validar el token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            
            ValidateAudience = true,
            ValidAudience = audience,
            
            ValidateLifetime = true, // Verifica que el token no haya expirado
            
            ValidateIssuerSigningKey = true, // Verifica la firma con la clave secreta
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            
            ClockSkew = TimeSpan.Zero // Tiempo de tolerancia cero al vencer
        };
    });

// Habilitamos el motor de permisos (para decidir quién entra a dónde)
builder.Services.AddAuthorization();


// =========================================================================
// 4. CREACIÓN DE LA APP Y MIDDLEWARES (Tubería de peticiones HTTP)
// =========================================================================
var app = builder.Build();

// Solo en modo desarrollo mostramos Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Forzamos el uso de HTTPS por seguridad
app.UseHttpsRedirection();
app.UseCors("AllowBlazor");

// EL ORDEN AQUÍ ES CRÍTICO:
app.UseAuthentication(); // 1ro: "Quién sos" (Lee el token)
app.UseAuthorization();  // 2do: "Qué podés hacer" (Verifica permisos)


// =========================================================================
// 5. MAPEO DE ENDPOINTS (Rutas de la API)
// =========================================================================
// Aquí conectamos cada ruta (ej. /api/cursos) con su lógica
app.MapCursoEndpoints();
app.MapEspecialidadEndpoints();
app.MapMateriaEndpoints();
app.MapComisionEndpoints();
app.MapAuthEndpoints();
// app.MapUsuarioEndpoints();

// Arrancamos el servidor
app.Run();


