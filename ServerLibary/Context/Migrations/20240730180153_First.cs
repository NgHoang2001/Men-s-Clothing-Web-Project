using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibary.Context.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMucs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_DanhMuc_Cha = table.Column<int>(type: "int", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoiTuongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoiTuongs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KichThuocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KichThuocs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MauSacs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MaMau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MauSacs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokenInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_KhachHang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokenInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuKiens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kieu = table.Column<int>(type: "int", nullable: false),
                    GiaTri = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuKiens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrangThaiDonHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThaiDonHangs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrangThaiGioHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThaiGioHangs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VaiTros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_DanhMuc = table.Column<int>(type: "int", nullable: true),
                    Id_DoiTuong = table.Column<int>(type: "int", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatLieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HuongDanSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonGia = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanPhams_DanhMucs_Id_DanhMuc",
                        column: x => x.Id_DanhMuc,
                        principalTable: "DanhMucs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SanPhams_DoiTuongs_Id_DoiTuong",
                        column: x => x.Id_DoiTuong,
                        principalTable: "DoiTuongs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_VaiTro = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_VaiTros_Id_VaiTro",
                        column: x => x.Id_VaiTro,
                        principalTable: "VaiTros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HinhAnhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_SanPham = table.Column<int>(type: "int", nullable: true),
                    Id_DanhMuc = table.Column<int>(type: "int", nullable: true),
                    Id_SanPhamChiTiet = table.Column<int>(type: "int", nullable: true),
                    Id_MauSac = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
                    Kieu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhAnhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HinhAnhs_DanhMucs_Id_DanhMuc",
                        column: x => x.Id_DanhMuc,
                        principalTable: "DanhMucs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HinhAnhs_SanPhams_Id_SanPham",
                        column: x => x.Id_SanPham,
                        principalTable: "SanPhams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SanPham_SuKiens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_SanPham = table.Column<int>(type: "int", nullable: false),
                    Id_SuKien = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham_SuKiens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanPham_SuKiens_SanPhams_Id_SanPham",
                        column: x => x.Id_SanPham,
                        principalTable: "SanPhams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPham_SuKiens_SuKiens_Id_SuKien",
                        column: x => x.Id_SuKien,
                        principalTable: "SuKiens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPhamChiTiets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_SanPham = table.Column<int>(type: "int", nullable: false),
                    Id_MauSac = table.Column<int>(type: "int", nullable: true),
                    Id_KichThuoc = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamChiTiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiets_KichThuocs_Id_KichThuoc",
                        column: x => x.Id_KichThuoc,
                        principalTable: "KichThuocs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiets_MauSacs_Id_MauSac",
                        column: x => x.Id_MauSac,
                        principalTable: "MauSacs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiets_SanPhams_Id_SanPham",
                        column: x => x.Id_SanPham,
                        principalTable: "SanPhams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_TaiKhoan = table.Column<int>(type: "int", nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Sdt = table.Column<string>(type: "VARCHAR(15)", maxLength: 15, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "Date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhachHangs_TaiKhoans_Id_TaiKhoan",
                        column: x => x.Id_TaiKhoan,
                        principalTable: "TaiKhoans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiaChis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_KhachHang = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChiChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefalut = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaChis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaChis_KhachHangs_Id_KhachHang",
                        column: x => x.Id_KhachHang,
                        principalTable: "KhachHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHangChiTiets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_KhachHang = table.Column<int>(type: "int", nullable: true),
                    Id_SanPhamChiTiet = table.Column<int>(type: "int", nullable: true),
                    Id_TrangThaiGioHang = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    IsChon = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangChiTiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiets_KhachHangs_Id_KhachHang",
                        column: x => x.Id_KhachHang,
                        principalTable: "KhachHangs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GioHangChiTiets_SanPhamChiTiets_Id_SanPhamChiTiet",
                        column: x => x.Id_SanPhamChiTiet,
                        principalTable: "SanPhamChiTiets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GioHangChiTiets_TrangThaiGioHangs_Id_TrangThaiGioHang",
                        column: x => x.Id_TrangThaiGioHang,
                        principalTable: "TrangThaiGioHangs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_KhachHang = table.Column<int>(type: "int", nullable: true),
                    Id_DiaChi = table.Column<int>(type: "int", nullable: true),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonHangs_DiaChis_Id_DiaChi",
                        column: x => x.Id_DiaChi,
                        principalTable: "DiaChis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DonHangs_KhachHangs_Id_KhachHang",
                        column: x => x.Id_KhachHang,
                        principalTable: "KhachHangs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DonHang_TrangThaiDonHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_DonHang = table.Column<int>(type: "int", nullable: false),
                    Id_TrangThaiDonHang = table.Column<int>(type: "int", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang_TrangThaiDonHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonHang_TrangThaiDonHangs_DonHangs_Id_DonHang",
                        column: x => x.Id_DonHang,
                        principalTable: "DonHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonHang_TrangThaiDonHangs_TrangThaiDonHangs_Id_TrangThaiDonHang",
                        column: x => x.Id_TrangThaiDonHang,
                        principalTable: "TrangThaiDonHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonHangChiTiets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_DonHang = table.Column<int>(type: "int", nullable: false),
                    Id_SanPhamChiTiet = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangChiTiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonHangChiTiets_DonHangs_Id_DonHang",
                        column: x => x.Id_DonHang,
                        principalTable: "DonHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonHangChiTiets_SanPhamChiTiets_Id_SanPhamChiTiet",
                        column: x => x.Id_SanPhamChiTiet,
                        principalTable: "SanPhamChiTiets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThanhToans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_DonHang = table.Column<int>(type: "int", nullable: false),
                    Kieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TongTienChuaThanhToan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongTienDaThanhToan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhToans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThanhToans_DonHangs_Id_DonHang",
                        column: x => x.Id_DonHang,
                        principalTable: "DonHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiaChis_Id_KhachHang",
                table: "DiaChis",
                column: "Id_KhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_TrangThaiDonHangs_Id_DonHang",
                table: "DonHang_TrangThaiDonHangs",
                column: "Id_DonHang");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_TrangThaiDonHangs_Id_TrangThaiDonHang",
                table: "DonHang_TrangThaiDonHangs",
                column: "Id_TrangThaiDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangChiTiets_Id_DonHang",
                table: "DonHangChiTiets",
                column: "Id_DonHang");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangChiTiets_Id_SanPhamChiTiet",
                table: "DonHangChiTiets",
                column: "Id_SanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_Id_DiaChi",
                table: "DonHangs",
                column: "Id_DiaChi");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_Id_KhachHang",
                table: "DonHangs",
                column: "Id_KhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiets_Id_KhachHang",
                table: "GioHangChiTiets",
                column: "Id_KhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiets_Id_SanPhamChiTiet",
                table: "GioHangChiTiets",
                column: "Id_SanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiets_Id_TrangThaiGioHang",
                table: "GioHangChiTiets",
                column: "Id_TrangThaiGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_HinhAnhs_Id_DanhMuc",
                table: "HinhAnhs",
                column: "Id_DanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_HinhAnhs_Id_SanPham",
                table: "HinhAnhs",
                column: "Id_SanPham");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangs_Id_TaiKhoan",
                table: "KhachHangs",
                column: "Id_TaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_SuKiens_Id_SanPham",
                table: "SanPham_SuKiens",
                column: "Id_SanPham");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_SuKiens_Id_SuKien",
                table: "SanPham_SuKiens",
                column: "Id_SuKien");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiets_Id_KichThuoc",
                table: "SanPhamChiTiets",
                column: "Id_KichThuoc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiets_Id_MauSac",
                table: "SanPhamChiTiets",
                column: "Id_MauSac");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiets_Id_SanPham",
                table: "SanPhamChiTiets",
                column: "Id_SanPham");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_Id_DanhMuc",
                table: "SanPhams",
                column: "Id_DanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_Id_DoiTuong",
                table: "SanPhams",
                column: "Id_DoiTuong");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_Id_VaiTro",
                table: "TaiKhoans",
                column: "Id_VaiTro");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToans_Id_DonHang",
                table: "ThanhToans",
                column: "Id_DonHang");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonHang_TrangThaiDonHangs");

            migrationBuilder.DropTable(
                name: "DonHangChiTiets");

            migrationBuilder.DropTable(
                name: "GioHangChiTiets");

            migrationBuilder.DropTable(
                name: "HinhAnhs");

            migrationBuilder.DropTable(
                name: "RefreshTokenInfos");

            migrationBuilder.DropTable(
                name: "SanPham_SuKiens");

            migrationBuilder.DropTable(
                name: "ThanhToans");

            migrationBuilder.DropTable(
                name: "TrangThaiDonHangs");

            migrationBuilder.DropTable(
                name: "SanPhamChiTiets");

            migrationBuilder.DropTable(
                name: "TrangThaiGioHangs");

            migrationBuilder.DropTable(
                name: "SuKiens");

            migrationBuilder.DropTable(
                name: "DonHangs");

            migrationBuilder.DropTable(
                name: "KichThuocs");

            migrationBuilder.DropTable(
                name: "MauSacs");

            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "DiaChis");

            migrationBuilder.DropTable(
                name: "DanhMucs");

            migrationBuilder.DropTable(
                name: "DoiTuongs");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.DropTable(
                name: "VaiTros");
        }
    }
}
