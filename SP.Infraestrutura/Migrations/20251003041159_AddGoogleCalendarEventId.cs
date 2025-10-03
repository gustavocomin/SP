using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SP.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class AddGoogleCalendarEventId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleCalendarEventId",
                table: "Sessoes",
                type: "character varying(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleCalendarEventId",
                table: "Sessoes");
        }
    }
}
