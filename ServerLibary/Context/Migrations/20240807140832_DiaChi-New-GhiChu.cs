using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibary.Context.Migrations
{
    /// <inheritdoc />
    public partial class DiaChiNewGhiChu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "DiaChis",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "DiaChis");
        }
    }
}
