using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SP.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalizacaoEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cidade",
                table: "Clientes",
                newName: "CidadeNome");

            migrationBuilder.AddColumn<int>(
                name: "CidadeId",
                table: "Clientes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "VARCHAR(100)", unicode: false, maxLength: 100, nullable: false),
                    CodigoISO = table.Column<string>(type: "CHAR(2)", unicode: false, maxLength: 2, nullable: false),
                    CodigoISO3 = table.Column<string>(type: "CHAR(3)", unicode: false, maxLength: 3, nullable: false),
                    CodigoTelefone = table.Column<string>(type: "VARCHAR(5)", unicode: false, maxLength: 5, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "VARCHAR(100)", unicode: false, maxLength: 100, nullable: false),
                    Sigla = table.Column<string>(type: "CHAR(2)", unicode: false, maxLength: 2, nullable: false),
                    CodigoIBGE = table.Column<string>(type: "CHAR(2)", unicode: false, maxLength: 2, nullable: true),
                    Regiao = table.Column<string>(type: "VARCHAR(50)", unicode: false, maxLength: 50, nullable: true),
                    PaisId = table.Column<int>(type: "integer", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estados_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "VARCHAR(100)", unicode: false, maxLength: 100, nullable: false),
                    CodigoIBGE = table.Column<string>(type: "CHAR(7)", unicode: false, maxLength: 7, nullable: true),
                    CEP = table.Column<string>(type: "VARCHAR(10)", unicode: false, maxLength: 10, nullable: true),
                    Latitude = table.Column<decimal>(type: "numeric(10,8)", precision: 18, scale: 2, nullable: true),
                    Longitude = table.Column<decimal>(type: "numeric(11,8)", precision: 18, scale: 2, nullable: true),
                    Populacao = table.Column<int>(type: "INTEGER", nullable: true),
                    Area = table.Column<decimal>(type: "numeric(10,2)", precision: 18, scale: 2, nullable: true),
                    EstadoId = table.Column<int>(type: "integer", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cidades_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_clientes_cidade_id",
                table: "Clientes",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "ix_cidades_ativo",
                table: "Cidades",
                column: "Ativo");

            migrationBuilder.CreateIndex(
                name: "ix_cidades_cep",
                table: "Cidades",
                column: "CEP");

            migrationBuilder.CreateIndex(
                name: "ix_cidades_codigo_ibge",
                table: "Cidades",
                column: "CodigoIBGE",
                unique: true,
                filter: "\"CodigoIBGE\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_cidades_coordenadas",
                table: "Cidades",
                columns: new[] { "Latitude", "Longitude" },
                filter: "\"Latitude\" IS NOT NULL AND \"Longitude\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_cidades_estado_nome",
                table: "Cidades",
                columns: new[] { "EstadoId", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cidades_nome",
                table: "Cidades",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "ix_estados_ativo",
                table: "Estados",
                column: "Ativo");

            migrationBuilder.CreateIndex(
                name: "ix_estados_codigo_ibge",
                table: "Estados",
                column: "CodigoIBGE",
                unique: true,
                filter: "\"CodigoIBGE\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_estados_nome",
                table: "Estados",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "ix_estados_pais_sigla",
                table: "Estados",
                columns: new[] { "PaisId", "Sigla" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_estados_regiao",
                table: "Estados",
                column: "Regiao");

            migrationBuilder.CreateIndex(
                name: "ix_paises_ativo",
                table: "Paises",
                column: "Ativo");

            migrationBuilder.CreateIndex(
                name: "ix_paises_codigo_iso",
                table: "Paises",
                column: "CodigoISO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_paises_codigo_iso3",
                table: "Paises",
                column: "CodigoISO3",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_paises_nome",
                table: "Paises",
                column: "Nome");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Cidades_CidadeId",
                table: "Clientes",
                column: "CidadeId",
                principalTable: "Cidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Cidades_CidadeId",
                table: "Clientes");

            migrationBuilder.DropTable(
                name: "Cidades");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropIndex(
                name: "ix_clientes_cidade_id",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CidadeId",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "CidadeNome",
                table: "Clientes",
                newName: "Cidade");
        }
    }
}
