using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP.Dominio.Localizacao;
using SP.Infraestrutura.Common.Base;

namespace SP.Infraestrutura.Entities.Localizacao;

/// <summary>
/// Configuração do Entity Framework para a entidade Cidade
/// </summary>
public class CidadeConfiguration : ConfigurationBase<Cidade>
{
    public override void ConfigurarEntidade(EntityTypeBuilder<Cidade> builder)
    {
        // Configuração da tabela
        builder.ToTable("Cidades");

        // Chave primária
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        // Propriedades obrigatórias
        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("VARCHAR(100)");

        // Propriedades opcionais
        builder.Property(c => c.CodigoIBGE)
            .HasMaxLength(7)
            .HasColumnType("CHAR(7)");

        builder.Property(c => c.CEP)
            .HasMaxLength(10)
            .HasColumnType("VARCHAR(10)");

        builder.Property(c => c.Latitude)
            .HasColumnType("DECIMAL(10,8)");

        builder.Property(c => c.Longitude)
            .HasColumnType("DECIMAL(11,8)");

        builder.Property(c => c.Populacao)
            .HasColumnType("INTEGER");

        builder.Property(c => c.Area)
            .HasColumnType("DECIMAL(10,2)");

        // Chave estrangeira
        builder.Property(c => c.EstadoId)
            .IsRequired();

        // Propriedades de controle
        builder.Property(c => c.Ativo)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(c => c.DataCriacao)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone");

        // Índices
        builder.HasIndex(c => new { c.EstadoId, c.Nome })
            .IsUnique()
            .HasDatabaseName("ix_cidades_estado_nome");

        builder.HasIndex(c => c.CodigoIBGE)
            .IsUnique()
            .HasDatabaseName("ix_cidades_codigo_ibge")
            .HasFilter("\"CodigoIBGE\" IS NOT NULL");

        builder.HasIndex(c => c.CEP)
            .HasDatabaseName("ix_cidades_cep");

        builder.HasIndex(c => c.Nome)
            .HasDatabaseName("ix_cidades_nome");

        builder.HasIndex(c => c.Ativo)
            .HasDatabaseName("ix_cidades_ativo");

        builder.HasIndex(c => new { c.Latitude, c.Longitude })
            .HasDatabaseName("ix_cidades_coordenadas")
            .HasFilter("\"Latitude\" IS NOT NULL AND \"Longitude\" IS NOT NULL");

        // Relacionamentos
        builder.HasOne(c => c.Estado)
            .WithMany(e => e.Cidades)
            .HasForeignKey(c => c.EstadoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
