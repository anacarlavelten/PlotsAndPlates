using Microsoft.EntityFrameworkCore;
using PlotsAndPlates.Backend.Data;
using PlotsAndPlates.Backend.Controllers; // Garante que acha os controllers

try 
{
    Console.WriteLine("--> 1. Iniciando o Builder...");
    var builder = WebApplication.CreateBuilder(args);

    // Configuração do Banco
    Console.WriteLine("--> 2. Configurando Banco de Dados...");
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    // Imprime a string de conexão (SÓ PARA TESTE - apague depois se for colocar em produção)
    Console.WriteLine($"--> String usada: {connectionString}");

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));

    // Configuração dos Controllers e Swagger
    Console.WriteLine("--> 3. Adicionando Services e Swagger...");
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Construção do App
    Console.WriteLine("--> 4. Construindo o App (Build)...");
    var app = builder.Build();

    // Pipeline
    if (app.Environment.IsDevelopment())
    {
        Console.WriteLine("--> 5. Ativando Swagger UI...");
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();

    Console.WriteLine("--> 6. TUDO PRONTO! Iniciando o servidor agora...");
    app.Run();
}
catch (Exception ex)
{
    // AQUI VAI APARECER O ERRO REAL
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\nCRASH TOTAL! Ocorreu um erro fatal:");
    Console.WriteLine(ex.ToString());
    Console.ResetColor();
}