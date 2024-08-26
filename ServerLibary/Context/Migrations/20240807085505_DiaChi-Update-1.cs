using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibary.Context.Migrations
{
    /// <inheritdoc />
    public partial class DiaChiUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "DiaChis",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "DiaChis");
        }
    }
}
