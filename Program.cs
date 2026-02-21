using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PlotsAndPlates.Backend.Data;
using Npgsql;
using PlotsAndPlates.Backend.Models;

try 
{

    Console.WriteLine("--> 1. Iniciando o Builder...");
    var builder = WebApplication.CreateBuilder(args);

    Console.WriteLine("--> 2. Configurando Banco de Dados...");
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    // A linha abaixo foi comentada para esconder a sua senha do banco!
    // Console.WriteLine($"--> String usada: {connectionString}");

    var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
    
    var dataSource = dataSourceBuilder.Build();

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(dataSource));

    Console.WriteLine("--> 3. Adicionando Services, CORS e Swagger...");
    
    // Configuração do CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    
    // Swagger simples (sem o cadeado por enquanto para garantir que funcione)
    builder.Services.AddSwaggerGen();

    // Configuração da Autenticação (JWT)
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

    Console.WriteLine("--> 4. Construindo o App (Build)...");
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        Console.WriteLine("--> 5. Ativando Swagger UI...");
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // ==========================================
    // PIPELINE (A ORDEM AQUI É SUPER IMPORTANTE)
    // ==========================================
    app.UseCors("AllowAll"); // 1º Libera o acesso do Frontend
    app.UseAuthentication(); // 2º Verifica QUEM é o usuário (Lê o Token)
    app.UseAuthorization();  // 3º Verifica O QUE ele pode fazer
    app.MapControllers();    // 4º Roteia para os Controllers

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