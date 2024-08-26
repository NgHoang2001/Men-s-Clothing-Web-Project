using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibary.Context.Migrations
{
    /// <inheritdoc />
    public partial class HinhAnhBanner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isBanner",
                table: "HinhAnhs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBanner",
                table: "HinhAnhs");
        }
    }
}
