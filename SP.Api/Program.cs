using Microsoft.EntityFrameworkCore;
using SP.Infraestrutura;
using SP.Infraestrutura.Data.Context;
using SP.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar camadas
builder.Services.AddInfraestrutura(builder.Configuration);
builder.Services.ConfigurarDependencias();

var app = builder.Build();

// Execute migrations automatically
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SPContext>();
    try
    {
        Console.WriteLine("🔄 Executando migrações do banco de dados...");
        context.Database.Migrate();
        Console.WriteLine("✅ Migrações executadas com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro ao executar migrações: {ex.Message}");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new {
    status = "healthy",
    timestamp = DateTime.UtcNow,
    version = "1.0.0"
}));

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
