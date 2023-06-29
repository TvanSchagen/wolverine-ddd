using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfferteTool.Migrations
{
    /// <inheritdoc />
    public partial class AddRelatieEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RelatieEmail",
                table: "Offertes",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatieEmail",
                table: "Offertes");
        }
    }
}
