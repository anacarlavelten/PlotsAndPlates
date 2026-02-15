using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PlotsAndPlates.Backend.Data;
using PlotsAndPlates.Backend.Controllers;
try 
{
    Console.WriteLine("--> 1. Iniciando o Builder...");
    var builder = WebApplication.CreateBuilder(args);

    Console.WriteLine("--> 2. Configurando Banco de Dados...");
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    Console.WriteLine($"--> String usada: {connectionString}");

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));

    Console.WriteLine("--> 3. Adicionando Services e Swagger...");
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Cabeçalho de autorização padrão usando o esquema Bearer (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value!)),
            ValidateIssuer = false, // Em produção, mude para true
            ValidateAudience = false // Em produção, mude para true
        };
    });

    builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
    Console.WriteLine("--> 4. Construindo o App (Build)...");
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        Console.WriteLine("--> 5. Ativando Swagger UI...");
        app.UseSwagger();
        app.UseSwaggerUI();
    }
app.UseCors("AllowAll");
    app.MapControllers();

    Console.WriteLine("--> 6. TUDO PRONTO! Iniciando o servidor agora...");
    app.Run();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\nCRASH TOTAL! Ocorreu um erro fatal:");
    Console.WriteLine(ex.ToString());
    Console.ResetColor();
}
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();