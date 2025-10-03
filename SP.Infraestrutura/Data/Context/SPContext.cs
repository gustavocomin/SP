using Microsoft.EntityFrameworkCore;
using SP.Dominio.Clientes;
using SP.Dominio.Sessoes;
using SP.Dominio.Localizacao;

namespace SP.Infraestrutura.Data.Context
{
    public class SPContext : DbContext
    {
        public SPContext(DbContextOptions<SPContext> options) : base(options) { }

        // DbSets
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }

        // Localização
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Cidade> Cidades { get; set; }

        // public DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicar todas as configurações
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SPContext).Assembly);
            
            // Configurações globais
            ConfigurarConvencoes(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigurarConvencoes(ModelBuilder modelBuilder)
        {
            // Configurar todas as strings como VARCHAR ao invés de NVARCHAR
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            {
                property.SetIsUnicode(false);
            }

            // Configurar precisão decimal padrão
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?))))
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }

            // Configurar DateTime para timestamp sem timezone (PostgreSQL)
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?))))
            {
                property.SetColumnType("timestamp without time zone");
            }
        }
    }
}
