using Inteia.Api.Models;
using Inteia.Api.Services;
using Inteia.Api.Core;
using Inteia.Api.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ========== Configuraciones ==========
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

// ========== Inyección de dependencias ==========
builder.Services.AddSingleton(typeof(MongoRepository<>));
builder.Services.AddSingleton(typeof(IRepository<>), typeof(MongoRepository<>));
builder.Services.AddScoped<IGenericService<Evento>, EventoService>();
builder.Services.AddScoped<IGenericService<Vinculador>, VinculadorService>();
builder.Services.AddScoped<IGenericService<GrupoInvestigacion>, GrupoInvestigacionService>();
builder.Services.AddScoped<IGenericService<ClasificacionGrupo>, ClasificacionGrupoService>();
builder.Services.AddScoped<IGenericService<ProgramasColciencias>, ProgramasColcienciasService>();
builder.Services.AddScoped<IGenericService<Convocatoria>, ConvocatoriaService>();

// Servicios de autenticación
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// ========== Configuración JWT ==========
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// ========== MVC y Swagger ==========
builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ========== Middleware ==========
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inteia API V1");
    c.RoutePrefix = string.Empty; 
});

app.UseHttpsRedirection();

// Autenticación y autorización JWT
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
