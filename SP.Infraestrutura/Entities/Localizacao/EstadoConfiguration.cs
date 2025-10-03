using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP.Dominio.Localizacao;
using SP.Infraestrutura.Common.Base;

namespace SP.Infraestrutura.Entities.Localizacao;

/// <summary>
/// Configuração do Entity Framework para a entidade Estado
/// </summary>
public class EstadoConfiguration : ConfigurationBase<Estado>
{
    public override void ConfigurarEntidade(EntityTypeBuilder<Estado> builder)
    {
        // Configuração da tabela
        builder.ToTable("Estados");

        // Chave primária
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        // Propriedades obrigatórias
        builder.Property(e => e.Nome)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("VARCHAR(100)");

        builder.Property(e => e.Sigla)
            .IsRequired()
            .HasMaxLength(2)
            .HasColumnType("CHAR(2)");

        // Propriedades opcionais
        builder.Property(e => e.CodigoIBGE)
            .HasMaxLength(2)
            .HasColumnType("CHAR(2)");

        builder.Property(e => e.Regiao)
            .HasMaxLength(50)
            .HasColumnType("VARCHAR(50)");

        // Chave estrangeira
        builder.Property(e => e.PaisId)
            .IsRequired();

        // Propriedades de controle
        builder.Property(e => e.Ativo)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.DataCriacao)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone");

        // Índices
        builder.HasIndex(e => new { e.PaisId, e.Sigla })
            .IsUnique()
            .HasDatabaseName("ix_estados_pais_sigla");

        builder.HasIndex(e => e.CodigoIBGE)
            .IsUnique()
            .HasDatabaseName("ix_estados_codigo_ibge")
            .HasFilter("\"CodigoIBGE\" IS NOT NULL");

        builder.HasIndex(e => e.Nome)
            .HasDatabaseName("ix_estados_nome");

        builder.HasIndex(e => e.Ativo)
            .HasDatabaseName("ix_estados_ativo");

        builder.HasIndex(e => e.Regiao)
            .HasDatabaseName("ix_estados_regiao");

        // Relacionamentos
        builder.HasOne(e => e.Pais)
            .WithMany(p => p.Estados)
            .HasForeignKey(e => e.PaisId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Cidades)
            .WithOne(c => c.Estado)
            .HasForeignKey(c => c.EstadoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
