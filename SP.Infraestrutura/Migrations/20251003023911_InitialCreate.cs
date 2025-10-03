using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SP.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    CPF = table.Column<string>(type: "character(14)", unicode: false, fixedLength: true, maxLength: 14, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    Estado = table.Column<string>(type: "character(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    Cidade = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    CEP = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: true),
                    Endereco = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: true),
                    Bairro = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    Complemento = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    Numero = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: true),
                    ValorSessao = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    FormaPagamentoPreferida = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    DiaVencimento = table.Column<int>(type: "integer", nullable: true),
                    StatusFinanceiro = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Observacoes = table.Column<string>(type: "character varying(1000)", unicode: false, maxLength: 1000, nullable: true),
                    ContatoEmergencia = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: true),
                    TelefoneEmergencia = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true),
                    Profissao = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    AceiteLgpd = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_clientes_ativo",
                table: "Clientes",
                column: "Ativo");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_cpf",
                table: "Clientes",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_clientes_email",
                table: "Clientes",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_clientes_nome_ativo",
                table: "Clientes",
                columns: new[] { "Nome", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "ix_clientes_status_financeiro",
                table: "Clientes",
                column: "StatusFinanceiro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
