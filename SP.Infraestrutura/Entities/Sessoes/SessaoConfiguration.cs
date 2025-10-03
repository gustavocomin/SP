using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP.Dominio.Enums;
using SP.Dominio.Sessoes;
using SP.Infraestrutura.Common.Base;

namespace SP.Infraestrutura.Entities.Sessoes;

/// <summary>
/// Configuração do Entity Framework para a entidade Sessao
/// </summary>
public class SessaoConfiguration : ConfigurationBase<Sessao>
{
    public override void ConfigurarEntidade(EntityTypeBuilder<Sessao> builder)
    {
        // Configuração da tabela
        builder.ToTable("Sessoes");

        // Chave primária
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd();

        // Relacionamento com Cliente
        builder.HasOne(s => s.Cliente)
            .WithMany()
            .HasForeignKey(s => s.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Propriedades obrigatórias
        builder.Property(s => s.ClienteId)
            .IsRequired();

        builder.Property(s => s.DataHoraAgendada)
            .IsRequired()
            .HasColumnType("timestamp without time zone");

        builder.Property(s => s.DataHoraRealizada)
            .HasColumnType("timestamp without time zone");

        builder.Property(s => s.DuracaoMinutos)
            .IsRequired()
            .HasDefaultValue(50);

        builder.Property(s => s.Valor)
            .IsRequired()
            .HasPrecision(18, 2);

        // Enums
        builder.Property(s => s.Status)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(StatusSessao.Agendada);

        builder.Property(s => s.Periodicidade)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(PeriodicidadeSessao.Semanal);

        // Propriedades de texto
        builder.Property(s => s.Observacoes)
            .HasMaxLength(1000);

        builder.Property(s => s.AnotacoesClinicas)
            .HasMaxLength(2000);

        builder.Property(s => s.MotivoCancelamento)
            .HasMaxLength(500);

        builder.Property(s => s.FormaPagamento)
            .HasMaxLength(50);

        // Propriedades booleanas
        builder.Property(s => s.Pago)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(s => s.ConsiderarFaturamento)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(s => s.Ativo)
            .IsRequired()
            .HasDefaultValue(true);

        // Google Calendar Integration
        builder.Property(s => s.GoogleCalendarEventId)
            .HasMaxLength(255);

        // Propriedades de auditoria
        builder.Property(s => s.DataCriacao)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone");

        builder.Property(s => s.DataUltimaAtualizacao)
            .HasColumnType("timestamp without time zone");

        builder.Property(s => s.DataPagamento)
            .HasColumnType("timestamp without time zone");

        // Auto-relacionamento para reagendamentos
        builder.HasOne(s => s.SessaoOriginal)
            .WithMany(s => s.SessoesReagendadas)
            .HasForeignKey(s => s.SessaoOriginalId)
            .OnDelete(DeleteBehavior.Restrict);

        // Evitar AutoInclude em navegações circulares
        builder.Navigation(s => s.SessoesReagendadas)
            .EnableLazyLoading(false);

        builder.Navigation(s => s.SessaoOriginal)
            .EnableLazyLoading(false);

        // Índices
        builder.HasIndex(s => s.ClienteId)
            .HasDatabaseName("ix_sessoes_cliente_id");

        builder.HasIndex(s => s.DataHoraAgendada)
            .HasDatabaseName("ix_sessoes_data_hora_agendada");

        builder.HasIndex(s => s.Status)
            .HasDatabaseName("ix_sessoes_status");

        builder.HasIndex(s => s.Pago)
            .HasDatabaseName("ix_sessoes_pago");

        builder.HasIndex(s => new { s.ClienteId, s.DataHoraAgendada })
            .HasDatabaseName("ix_sessoes_cliente_data")
            .IsUnique();

        builder.HasIndex(s => s.Ativo)
            .HasDatabaseName("ix_sessoes_ativo");
    }
}
