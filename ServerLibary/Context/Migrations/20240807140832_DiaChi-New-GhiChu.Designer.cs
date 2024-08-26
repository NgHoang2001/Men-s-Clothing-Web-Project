﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServerLibary.Context;

#nullable disable

namespace ServerLibary.Context.Migrations
{
    [DbContext(typeof(DuAnWebCanifaDbContext))]
    [Migration("20240807140832_DiaChi-New-GhiChu")]
    partial class DiaChiNewGhiChu
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BaseLibary.Entities.DanhMuc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Id_DanhMuc_Cha")
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("DanhMucs");
                });

            modelBuilder.Entity("BaseLibary.Entities.DiaChi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DiaChiChiTiet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_KhachHang")
                        .HasColumnType("int");

                    b.Property<bool>("IsDefalut")
                        .HasColumnType("bit");

                    b.Property<string>("Province_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenNguoiNhan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ward_code")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id_KhachHang");

                    b.ToTable("DiaChis");
                });

            modelBuilder.Entity("BaseLibary.Entities.DoiTuong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ten")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("DoiTuongs");
                });

            modelBuilder.Entity("BaseLibary.Entities.DonHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Id_DiaChi")
                        .HasColumnType("int");

                    b.Property<int?>("Id_KhachHang")
                        .HasColumnType("int");

                    b.Property<DateTime>("ThoiGianTao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Id_DiaChi");

                    b.HasIndex("Id_KhachHang");

                    b.ToTable("DonHangs");
                });

            modelBuilder.Entity("BaseLibary.Entities.DonHangChiTiet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("DonGia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Id_DonHang")
                        .HasColumnType("int");

                    b.Property<int>("Id_SanPhamChiTiet")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<DateTime>("ThoiGianTao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Id_DonHang");

                    b.HasIndex("Id_SanPhamChiTiet");

                    b.ToTable("DonHangChiTiets");
                });

            modelBuilder.Entity("BaseLibary.Entities.DonHang_TrangThaiDonHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Id_DonHang")
                        .HasColumnType("int");

                    b.Property<int>("Id_TrangThaiDonHang")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ThoiGianTao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Id_DonHang");

                    b.HasIndex("Id_TrangThaiDonHang");

                    b.ToTable("DonHang_TrangThaiDonHangs");
                });

            modelBuilder.Entity("BaseLibary.Entities.GioHangChiTiet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Id_KhachHang")
                        .HasColumnType("int");

                    b.Property<int?>("Id_SanPhamChiTiet")
                        .HasColumnType("int");

                    b.Property<int?>("Id_TrangThaiGioHang")
                        .HasColumnType("int");

                    b.Property<bool>("IsChon")
                        .HasColumnType("bit");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id_KhachHang");

                    b.HasIndex("Id_SanPhamChiTiet");

                    b.HasIndex("Id_TrangThaiGioHang");

                    b.ToTable("GioHangChiTiets");
                });

            modelBuilder.Entity("BaseLibary.Entities.HinhAnh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Id_DanhMuc")
                        .HasColumnType("int");

                    b.Property<int?>("Id_MauSac")
                        .HasColumnType("int");

                    b.Property<int?>("Id_SanPham")
                        .HasColumnType("int");

                    b.Property<int?>("Id_SanPhamChiTiet")
                        .HasColumnType("int");

                    b.Property<int>("Kieu")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR");

                    b.Property<bool>("isBanner")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Id_DanhMuc");

                    b.HasIndex("Id_SanPham");

                    b.ToTable("HinhAnhs");
                });

            modelBuilder.Entity("BaseLibary.Entities.KhachHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("GioiTinh")
                        .HasColumnType("bit");

                    b.Property<int?>("Id_TaiKhoan")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("Date");

                    b.Property<string>("Sdt")
                        .HasMaxLength(15)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Ten")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Id_TaiKhoan");

                    b.ToTable("KhachHangs");
                });

            modelBuilder.Entity("BaseLibary.Entities.KichThuoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ten")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("KichThuocs");
                });

            modelBuilder.Entity("BaseLibary.Entities.MauSac", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MaMau")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MauSacs");
                });

            modelBuilder.Entity("BaseLibary.Entities.RefreshTokenInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Id_KhachHang")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokenInfos");
                });

            modelBuilder.Entity("BaseLibary.Entities.SanPham", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ChatLieu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DonGia")
                        .HasColumnType("int");

                    b.Property<string>("HuongDanSuDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Id_DanhMuc")
                        .HasColumnType("int");

                    b.Property<int?>("Id_DoiTuong")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Id_DanhMuc");

                    b.HasIndex("Id_DoiTuong");

                    b.ToTable("SanPhams");
                });

            modelBuilder.Entity("BaseLibary.Entities.SanPhamChiTiet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Id_KichThuoc")
                        .HasColumnType("int");

                    b.Property<int?>("Id_MauSac")
                        .HasColumnType("int");

                    b.Property<int>("Id_SanPham")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Id_KichThuoc");

                    b.HasIndex("Id_MauSac");

                    b.HasIndex("Id_SanPham");

                    b.ToTable("SanPhamChiTiets");
                });

            modelBuilder.Entity("BaseLibary.Entities.SanPham_SuKien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DonGia")
                        .HasColumnType("int");

                    b.Property<int>("Id_SanPham")
                        .HasColumnType("int");

                    b.Property<int>("Id_SuKien")
                        .HasColumnType("int");

                    b.Property<DateTime>("ThoiGianBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ThoiGianKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Id_SanPham");

                    b.HasIndex("Id_SuKien");

                    b.ToTable("SanPham_SuKiens");
                });

            modelBuilder.Entity("BaseLibary.Entities.SuKien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GiaTri")
                        .HasColumnType("int");

                    b.Property<int>("Kieu")
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ThoiGianTao")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("SuKiens");
                });

            modelBuilder.Entity("BaseLibary.Entities.TaiKhoan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Id_VaiTro")
                        .HasColumnType("int");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Id_VaiTro");

                    b.ToTable("TaiKhoans");
                });

            modelBuilder.Entity("BaseLibary.Entities.ThanhToan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Id_DonHang")
                        .HasColumnType("int");

                    b.Property<string>("Kieu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ThoiGianTao")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TongTienChuaThanhToan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TongTienDaThanhToan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Id_DonHang");

                    b.ToTable("ThanhToans");
                });

            modelBuilder.Entity("BaseLibary.Entities.TrangThaiDonHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ten")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("TrangThaiDonHangs");
                });

            modelBuilder.Entity("BaseLibary.Entities.TrangThaiGioHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ten")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("TrangThaiGioHangs");
                });

            modelBuilder.Entity("BaseLibary.Entities.VaiTro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("VaiTros");
                });

            modelBuilder.Entity("BaseLibary.Entities.administrative_regions", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("code_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code_name_en")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name_en")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Administrative_Regions");
                });

            modelBuilder.Entity("BaseLibary.Entities.administrative_units", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("code_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code_name_en")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("full_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("full_name_en")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("short_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("short_name_en")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Administrative_Units");
                });

            modelBuilder.Entity("BaseLibary.Entities.districts", b =>
                {
                    b.Property<string>("code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Administrative_Unitsid")
                        .HasColumnType("int");

                    b.Property<string>("Provincescode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("administrative_unit_id")
                        .HasColumnType("int");

                    b.Property<string>("code_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("full_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("full_name_en")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name_en")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("province_code")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("code");

                    b.HasIndex("Administrative_Unitsid");

                    b.HasIndex("Provincescode");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("BaseLibary.Entities.provinces", b =>
                {
                    b.Property<string>("code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Administrative_Regionsid")
                        .HasColumnType("int");

                    b.Property<int?>("Administrative_Unitsid")
                        .HasColumnType("int");

                    b.Property<int?>("administrative_region_id")
                        .HasColumnType("int");

                    b.Property<int?>("administrative_unit_id")
                        .HasColumnType("int");

                    b.Property<string>("code_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("full_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("full_name_en")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name_en")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("code");

                    b.HasIndex("Administrative_Regionsid");

                    b.HasIndex("Administrative_Unitsid");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("BaseLibary.Entities.wards", b =>
                {
                    b.Property<string>("code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Administrative_Unitsid")
                        .HasColumnType("int");

                    b.Property<string>("Districtscode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("administrative_unit_id")
                        .HasColumnType("int");

                    b.Property<string>("code_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("district_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("full_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("full_name_en")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name_en")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("code");

                    b.HasIndex("Administrative_Unitsid");

                    b.HasIndex("Districtscode");

                    b.ToTable("Wards");
                });

            modelBuilder.Entity("BaseLibary.Entities.DiaChi", b =>
                {
                    b.HasOne("BaseLibary.Entities.KhachHang", "KhachHang")
                        .WithMany("DiaChis")
                        .HasForeignKey("Id_KhachHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhachHang");
                });

            modelBuilder.Entity("BaseLibary.Entities.DonHang", b =>
                {
                    b.HasOne("BaseLibary.Entities.DiaChi", "DiaChi")
                        .WithMany()
                        .HasForeignKey("Id_DiaChi");

                    b.HasOne("BaseLibary.Entities.KhachHang", "KhachHang")
                        .WithMany("DonHangs")
                        .HasForeignKey("Id_KhachHang");

                    b.Navigation("DiaChi");

                    b.Navigation("KhachHang");
                });

            modelBuilder.Entity("BaseLibary.Entities.DonHangChiTiet", b =>
                {
                    b.HasOne("BaseLibary.Entities.DonHang", "DonHang")
                        .WithMany("DonHangChiTiets")
                        .HasForeignKey("Id_DonHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseLibary.Entities.SanPhamChiTiet", "SanPhamChiTiet")
                        .WithMany()
                        .HasForeignKey("Id_SanPhamChiTiet")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonHang");

                    b.Navigation("SanPhamChiTiet");
                });

            modelBuilder.Entity("BaseLibary.Entities.DonHang_TrangThaiDonHang", b =>
                {
                    b.HasOne("BaseLibary.Entities.DonHang", "DonHang")
                        .WithMany("DonHang_TrangThaiDonHangs")
                        .HasForeignKey("Id_DonHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseLibary.Entities.TrangThaiDonHang", "TrangThaiDonHang")
                        .WithMany("DonHang_TrangThaiDonHangs")
                        .HasForeignKey("Id_TrangThaiDonHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonHang");

                    b.Navigation("TrangThaiDonHang");
                });

            modelBuilder.Entity("BaseLibary.Entities.GioHangChiTiet", b =>
                {
                    b.HasOne("BaseLibary.Entities.KhachHang", "KhachHang")
                        .WithMany("GioHangChiTiets")
                        .HasForeignKey("Id_KhachHang");

                    b.HasOne("BaseLibary.Entities.SanPhamChiTiet", "SanPhamChiTiet")
                        .WithMany()
                        .HasForeignKey("Id_SanPhamChiTiet");

                    b.HasOne("BaseLibary.Entities.TrangThaiGioHang", "TrangThaiGioHang")
                        .WithMany("GioHangChiTiets")
                        .HasForeignKey("Id_TrangThaiGioHang");

                    b.Navigation("KhachHang");

                    b.Navigation("SanPhamChiTiet");

                    b.Navigation("TrangThaiGioHang");
                });

            modelBuilder.Entity("BaseLibary.Entities.HinhAnh", b =>
                {
                    b.HasOne("BaseLibary.Entities.DanhMuc", "DanhMuc")
                        .WithMany("HinhAnhs")
                        .HasForeignKey("Id_DanhMuc");

                    b.HasOne("BaseLibary.Entities.SanPham", "SanPham")
                        .WithMany("HinhAnhs")
                        .HasForeignKey("Id_SanPham");

                    b.Navigation("DanhMuc");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("BaseLibary.Entities.KhachHang", b =>
                {
                    b.HasOne("BaseLibary.Entities.TaiKhoan", "TaiKhoan")
                        .WithMany()
                        .HasForeignKey("Id_TaiKhoan");

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("BaseLibary.Entities.SanPham", b =>
                {
                    b.HasOne("BaseLibary.Entities.DanhMuc", "DanhMuc")
                        .WithMany("SanPhams")
                        .HasForeignKey("Id_DanhMuc");

                    b.HasOne("BaseLibary.Entities.DoiTuong", "DoiTuong")
                        .WithMany("SanPhams")
                        .HasForeignKey("Id_DoiTuong");

                    b.Navigation("DanhMuc");

                    b.Navigation("DoiTuong");
                });

            modelBuilder.Entity("BaseLibary.Entities.SanPhamChiTiet", b =>
                {
                    b.HasOne("BaseLibary.Entities.KichThuoc", "KichThuoc")
                        .WithMany("SanPhamChiTiets")
                        .HasForeignKey("Id_KichThuoc");

                    b.HasOne("BaseLibary.Entities.MauSac", "MauSac")
                        .WithMany("SanPhamChiTiets")
                        .HasForeignKey("Id_MauSac");

                    b.HasOne("BaseLibary.Entities.SanPham", "SanPham")
                        .WithMany("SanPhamChiTiets")
                        .HasForeignKey("Id_SanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KichThuoc");

                    b.Navigation("MauSac");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("BaseLibary.Entities.SanPham_SuKien", b =>
                {
                    b.HasOne("BaseLibary.Entities.SanPham", "SanPham")
                        .WithMany("SanPham_SuKiens")
                        .HasForeignKey("Id_SanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseLibary.Entities.SuKien", "SuKien")
                        .WithMany("Sukien_SanPhams")
                        .HasForeignKey("Id_SuKien")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");

                    b.Navigation("SuKien");
                });

            modelBuilder.Entity("BaseLibary.Entities.TaiKhoan", b =>
                {
                    b.HasOne("BaseLibary.Entities.VaiTro", "VaiTro")
                        .WithMany("TaiKhoans")
                        .HasForeignKey("Id_VaiTro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VaiTro");
                });

            modelBuilder.Entity("BaseLibary.Entities.ThanhToan", b =>
                {
                    b.HasOne("BaseLibary.Entities.DonHang", "DonHang")
                        .WithMany()
                        .HasForeignKey("Id_DonHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonHang");
                });

            modelBuilder.Entity("BaseLibary.Entities.districts", b =>
                {
                    b.HasOne("BaseLibary.Entities.administrative_units", "Administrative_Units")
                        .WithMany("Districts")
                        .HasForeignKey("Administrative_Unitsid");

                    b.HasOne("BaseLibary.Entities.provinces", "Provinces")
                        .WithMany("Districts")
                        .HasForeignKey("Provincescode");

                    b.Navigation("Administrative_Units");

                    b.Navigation("Provinces");
                });

            modelBuilder.Entity("BaseLibary.Entities.provinces", b =>
                {
                    b.HasOne("BaseLibary.Entities.administrative_regions", "Administrative_Regions")
                        .WithMany("Provinces")
                        .HasForeignKey("Administrative_Regionsid");

                    b.HasOne("BaseLibary.Entities.administrative_units", "Administrative_Units")
                        .WithMany("Provinces")
                        .HasForeignKey("Administrative_Unitsid");

                    b.Navigation("Administrative_Regions");

                    b.Navigation("Administrative_Units");
                });

            modelBuilder.Entity("BaseLibary.Entities.wards", b =>
                {
                    b.HasOne("BaseLibary.Entities.administrative_units", "Administrative_Units")
                        .WithMany("Wards")
                        .HasForeignKey("Administrative_Unitsid");

                    b.HasOne("BaseLibary.Entities.districts", "Districts")
                        .WithMany("Wards")
                        .HasForeignKey("Districtscode");

                    b.Navigation("Administrative_Units");

                    b.Navigation("Districts");
                });

            modelBuilder.Entity("BaseLibary.Entities.DanhMuc", b =>
                {
                    b.Navigation("HinhAnhs");

                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("BaseLibary.Entities.DoiTuong", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("BaseLibary.Entities.DonHang", b =>
                {
                    b.Navigation("DonHangChiTiets");

                    b.Navigation("DonHang_TrangThaiDonHangs");
                });

            modelBuilder.Entity("BaseLibary.Entities.KhachHang", b =>
                {
                    b.Navigation("DiaChis");

                    b.Navigation("DonHangs");

                    b.Navigation("GioHangChiTiets");
                });

            modelBuilder.Entity("BaseLibary.Entities.KichThuoc", b =>
                {
                    b.Navigation("SanPhamChiTiets");
                });

            modelBuilder.Entity("BaseLibary.Entities.MauSac", b =>
                {
                    b.Navigation("SanPhamChiTiets");
                });

            modelBuilder.Entity("BaseLibary.Entities.SanPham", b =>
                {
                    b.Navigation("HinhAnhs");

                    b.Navigation("SanPhamChiTiets");

                    b.Navigation("SanPham_SuKiens");
                });

            modelBuilder.Entity("BaseLibary.Entities.SuKien", b =>
                {
                    b.Navigation("Sukien_SanPhams");
                });

            modelBuilder.Entity("BaseLibary.Entities.TrangThaiDonHang", b =>
                {
                    b.Navigation("DonHang_TrangThaiDonHangs");
                });

            modelBuilder.Entity("BaseLibary.Entities.TrangThaiGioHang", b =>
                {
                    b.Navigation("GioHangChiTiets");
                });

            modelBuilder.Entity("BaseLibary.Entities.VaiTro", b =>
                {
                    b.Navigation("TaiKhoans");
                });

            modelBuilder.Entity("BaseLibary.Entities.administrative_regions", b =>
                {
                    b.Navigation("Provinces");
                });

            modelBuilder.Entity("BaseLibary.Entities.administrative_units", b =>
                {
                    b.Navigation("Districts");

                    b.Navigation("Provinces");

                    b.Navigation("Wards");
                });

            modelBuilder.Entity("BaseLibary.Entities.districts", b =>
                {
                    b.Navigation("Wards");
                });

            modelBuilder.Entity("BaseLibary.Entities.provinces", b =>
                {
                    b.Navigation("Districts");
                });
#pragma warning restore 612, 618
        }
    }
}
