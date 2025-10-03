using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP.Dominio.Clientes;
using SP.Dominio.Enums;
using SP.Infraestrutura.Common.Base;

namespace SP.Infraestrutura.Entities.Clientes
{
    public class ClienteConfiguration : ConfigurationBase<Cliente>
    {
        public override void ConfigurarEntidade(EntityTypeBuilder<Cliente> builder)
        {
            // Tabela
            builder.ToTable("Clientes");

            // Chave primária
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd();

            // Dados Pessoais
            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Telefone)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(c => c.CPF)
                   .IsRequired()
                   .HasMaxLength(14)
                   .IsFixedLength();

            builder.Property(c => c.DataNascimento)
                   .IsRequired()
                   .HasColumnType("date");

            // Endereço
            builder.Property(c => c.CidadeId);

            // Campos legados (manter por compatibilidade)
            builder.Property(c => c.Estado)
                   .HasMaxLength(2)
                   .IsFixedLength();

            builder.Property(c => c.CidadeNome)
                   .HasMaxLength(100);

            builder.Property(c => c.CEP)
                   .HasMaxLength(10);

            builder.Property(c => c.Endereco)
                   .HasMaxLength(200);

            builder.Property(c => c.Bairro)
                   .HasMaxLength(100);

            builder.Property(c => c.Complemento)
                   .HasMaxLength(100);

            builder.Property(c => c.Numero)
                   .HasMaxLength(10);

            // Campos Financeiros
            builder.Property(c => c.ValorSessao)
                   .IsRequired()
                   .HasPrecision(10, 2);

            builder.Property(c => c.FormaPagamentoPreferida)
                   .HasMaxLength(50);

            builder.Property(c => c.DiaVencimento)
                   .HasDefaultValue(null);

            builder.Property(c => c.StatusFinanceiro)
                   .IsRequired()
                   .HasConversion<int>()
                   .HasDefaultValue(StatusFinanceiro.EmDia);

            // Controle e Auditoria
            builder.Property(c => c.DataCadastro)
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(c => c.DataUltimaAtualizacao)
                   .HasDefaultValue(null);

            builder.Property(c => c.Ativo)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.Property(c => c.Observacoes)
                   .HasMaxLength(1000);

            // Campos Específicos para Psicologia
            builder.Property(c => c.ContatoEmergencia)
                   .HasMaxLength(200);

            builder.Property(c => c.TelefoneEmergencia)
                   .HasMaxLength(20);

            builder.Property(c => c.Profissao)
                   .HasMaxLength(100);

            // LGPD
            builder.Property(c => c.AceiteLgpd)
                   .IsRequired()
                   .HasDefaultValue(false);

            // Índices (PostgreSQL usa nomes em minúsculas)
            builder.HasIndex(c => c.CPF)
                   .IsUnique()
                   .HasDatabaseName("ix_clientes_cpf");

            builder.HasIndex(c => c.Email)
                   .IsUnique()
                   .HasDatabaseName("ix_clientes_email");

            builder.HasIndex(c => c.StatusFinanceiro)
                   .HasDatabaseName("ix_clientes_status_financeiro");

            builder.HasIndex(c => c.Ativo)
                   .HasDatabaseName("ix_clientes_ativo");

            // Configurações adicionais para performance
            builder.HasIndex(c => new { c.Nome, c.Ativo })
                   .HasDatabaseName("ix_clientes_nome_ativo");

            builder.HasIndex(c => c.CidadeId)
                   .HasDatabaseName("ix_clientes_cidade_id");

            // Relacionamentos
            builder.HasOne(c => c.Cidade)
                   .WithMany()
                   .HasForeignKey(c => c.CidadeId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
