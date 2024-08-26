using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibary.Context.Migrations
{
    /// <inheritdoc />
    public partial class DiaChiUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "DiaChis",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "District_code",
                table: "DiaChis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province_code",
                table: "DiaChis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ward_code",
                table: "DiaChis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Administrative_Regions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    code_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    code_name_en = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrative_Regions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Administrative_Units",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    full_name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    short_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    short_name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    code_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    code_name_en = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrative_Units", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    full_name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    code_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    administrative_unit_id = table.Column<int>(type: "int", nullable: true),
                    administrative_region_id = table.Column<int>(type: "int", nullable: true),
                    Administrative_Unitsid = table.Column<int>(type: "int", nullable: true),
                    Administrative_Regionsid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.code);
                    table.ForeignKey(
                        name: "FK_Provinces_Administrative_Regions_Administrative_Regionsid",
                        column: x => x.Administrative_Regionsid,
                        principalTable: "Administrative_Regions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Provinces_Administrative_Units_Administrative_Unitsid",
                        column: x => x.Administrative_Unitsid,
                        principalTable: "Administrative_Units",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    full_name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    code_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    province_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    administrative_unit_id = table.Column<int>(type: "int", nullable: true),
                    Administrative_Unitsid = table.Column<int>(type: "int", nullable: true),
                    Provincescode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.code);
                    table.ForeignKey(
                        name: "FK_Districts_Administrative_Units_Administrative_Unitsid",
                        column: x => x.Administrative_Unitsid,
                        principalTable: "Administrative_Units",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_Provincescode",
                        column: x => x.Provincescode,
                        principalTable: "Provinces",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                columns: table => new
                {
                    code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    full_name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    code_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    district_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    administrative_unit_id = table.Column<int>(type: "int", nullable: true),
                    Administrative_Unitsid = table.Column<int>(type: "int", nullable: true),
                    Districtscode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.code);
                    table.ForeignKey(
                        name: "FK_Wards_Administrative_Units_Administrative_Unitsid",
                        column: x => x.Administrative_Unitsid,
                        principalTable: "Administrative_Units",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Wards_Districts_Districtscode",
                        column: x => x.Districtscode,
                        principalTable: "Districts",
                        principalColumn: "code");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Districts_Administrative_Unitsid",
                table: "Districts",
                column: "Administrative_Unitsid");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_Provincescode",
                table: "Districts",
                column: "Provincescode");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_Administrative_Regionsid",
                table: "Provinces",
                column: "Administrative_Regionsid");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_Administrative_Unitsid",
                table: "Provinces",
                column: "Administrative_Unitsid");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_Administrative_Unitsid",
                table: "Wards",
                column: "Administrative_Unitsid");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_Districtscode",
                table: "Wards",
                column: "Districtscode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Administrative_Regions");

            migrationBuilder.DropTable(
                name: "Administrative_Units");

            migrationBuilder.DropColumn(
                name: "District_code",
                table: "DiaChis");

            migrationBuilder.DropColumn(
                name: "Province_code",
                table: "DiaChis");

            migrationBuilder.DropColumn(
                name: "Ward_code",
                table: "DiaChis");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "DiaChis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
