using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP.Dominio.Localizacao;
using SP.Infraestrutura.Common.Base;

namespace SP.Infraestrutura.Entities.Localizacao;

/// <summary>
/// Configuração do Entity Framework para a entidade País
/// </summary>
public class PaisConfiguration : ConfigurationBase<Pais>
{
    public override void ConfigurarEntidade(EntityTypeBuilder<Pais> builder)
    {
        // Configuração da tabela
        builder.ToTable("Paises");

        // Chave primária
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        // Propriedades obrigatórias
        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("VARCHAR(100)");

        builder.Property(p => p.CodigoISO)
            .IsRequired()
            .HasMaxLength(2)
            .HasColumnType("CHAR(2)");

        builder.Property(p => p.CodigoISO3)
            .IsRequired()
            .HasMaxLength(3)
            .HasColumnType("CHAR(3)");

        builder.Property(p => p.CodigoTelefone)
            .IsRequired()
            .HasMaxLength(5)
            .HasColumnType("VARCHAR(5)");

        // Propriedades de controle
        builder.Property(p => p.Ativo)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.DataCriacao)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone");

        // Índices
        builder.HasIndex(p => p.CodigoISO)
            .IsUnique()
            .HasDatabaseName("ix_paises_codigo_iso");

        builder.HasIndex(p => p.CodigoISO3)
            .IsUnique()
            .HasDatabaseName("ix_paises_codigo_iso3");

        builder.HasIndex(p => p.Nome)
            .HasDatabaseName("ix_paises_nome");

        builder.HasIndex(p => p.Ativo)
            .HasDatabaseName("ix_paises_ativo");

        // Relacionamentos
        builder.HasMany(p => p.Estados)
            .WithOne(e => e.Pais)
            .HasForeignKey(e => e.PaisId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
