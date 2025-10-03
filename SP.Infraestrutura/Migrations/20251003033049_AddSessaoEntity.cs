using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SP.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class AddSessaoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sessoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    DataHoraAgendada = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataHoraRealizada = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DuracaoMinutos = table.Column<int>(type: "integer", nullable: false, defaultValue: 50),
                    DuracaoRealMinutos = table.Column<int>(type: "integer", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Periodicidade = table.Column<int>(type: "integer", nullable: false, defaultValue: 3),
                    Observacoes = table.Column<string>(type: "character varying(1000)", unicode: false, maxLength: 1000, nullable: true),
                    AnotacoesClinicas = table.Column<string>(type: "character varying(2000)", unicode: false, maxLength: 2000, nullable: true),
                    MotivoCancelamento = table.Column<string>(type: "character varying(500)", unicode: false, maxLength: 500, nullable: true),
                    Pago = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DataPagamento = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FormaPagamento = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    ConsiderarFaturamento = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    SessaoOriginalId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessoes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessoes_Sessoes_SessaoOriginalId",
                        column: x => x.SessaoOriginalId,
                        principalTable: "Sessoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_sessoes_ativo",
                table: "Sessoes",
                column: "Ativo");

            migrationBuilder.CreateIndex(
                name: "ix_sessoes_cliente_data",
                table: "Sessoes",
                columns: new[] { "ClienteId", "DataHoraAgendada" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_sessoes_cliente_id",
                table: "Sessoes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "ix_sessoes_data_hora_agendada",
                table: "Sessoes",
                column: "DataHoraAgendada");

            migrationBuilder.CreateIndex(
                name: "ix_sessoes_pago",
                table: "Sessoes",
                column: "Pago");

            migrationBuilder.CreateIndex(
                name: "IX_Sessoes_SessaoOriginalId",
                table: "Sessoes",
                column: "SessaoOriginalId");

            migrationBuilder.CreateIndex(
                name: "ix_sessoes_status",
                table: "Sessoes",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessoes");
        }
    }
}
