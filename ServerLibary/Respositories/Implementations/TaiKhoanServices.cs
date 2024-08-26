using BaseLibary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibary.Context;
using ServerLibary.Model.Params;
using ServerLibary.Model.Responses;
using ServerLibary.Respositories.Contracts;

namespace ServerLibary.Respositories.Implementations
{
    public class TaiKhoanServices : ITaiKhoanServices
    {
        private DuAnWebCanifaDbContext _context;

        public TaiKhoanServices(DuAnWebCanifaDbContext context)
        {
            _context = context;
        }

        public async Task<ThongTinKhachHangResp> ChiTietThongTinKhachHang(int? id)
        {
            var resp = new ThongTinKhachHangResp();
            if (id == null)
            {
                return resp;
            }
            var acc = await _context.TaiKhoans.FirstOrDefaultAsync(x => x.Id == id);
            if (acc == null)
            {
                return resp;
            }
            var kh = await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id_TaiKhoan == acc.Id);
            if (kh == null)
            {
                return resp;
            }
            resp = new ThongTinKhachHangResp
            {
                id_TaiKhoan = acc?.Id,
                id_KhachHang = kh?.Id,
                Email = acc?.Email,
                GioiTinh = kh?.GioiTinh,
                NgaySinh = kh?.NgaySinh,
                Sdt = kh?.Sdt,
                Ten = kh?.Ten
            };
            return resp;
        }

        public async Task<GeneralRespone> CapNhatThongTinTaiKhoan(ThongTinKhachHangParam? param)
        {
            if (param == null || param.id == null)
            {
                return new GeneralRespone(false, "Model null");
            }
            var acc = await _context.TaiKhoans.FirstOrDefaultAsync(x => x.Id == param.id);
            if (acc == null)
            {
                return new GeneralRespone(false, "Cập nhật thất bại");
            }
            //     public string? Ten { get; set; }
            //public bool? GioiTinh { get; set; }
            //public string? Sdt { get; set; }
            //public DateTime? NgaySinh { get; set; }
            var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id_TaiKhoan == acc.Id);
            khachHang.Ten = string.IsNullOrEmpty(param?.Ten) ? khachHang?.Ten : param.Ten;
            khachHang.GioiTinh = param.GioiTinh == null ? khachHang?.GioiTinh : param?.GioiTinh;
            khachHang.Sdt = string.IsNullOrEmpty(param?.Sdt) ? khachHang?.Sdt : param.Sdt;
            khachHang.NgaySinh = param?.NgaySinh == null ? khachHang?.NgaySinh : param.NgaySinh;
            await _context.SaveChangesAsync();

            return new GeneralRespone(true, "Cập nhật thành công");
        }

        public async Task<List<DonHangResp>> DanhSachDonHang(int? id)
        {
            var resp = new List<DonHangResp>();
            if (id == null)
            {
                return resp;
            }
            var kh = await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id_TaiKhoan == id);
            if (kh == null)
            {
                return resp;
            }
            var dsDonHang = await _context.DonHangs.Where(x => x.Id_KhachHang == kh.Id)?.OrderBy(x => x.ThoiGianTao)?.ToListAsync();
            if (dsDonHang == null || dsDonHang.Count == 0)
            {
                return resp;
            }
            var lstIdDonHang = dsDonHang.Select(x => x.Id).Distinct().ToList();
            var lstDonHangChiTiet = await _context.DonHangChiTiets.Where(x => lstIdDonHang.Contains(x.Id_DonHang)).OrderBy(x => x.Id_DonHang).ToListAsync();

            foreach (var idDonHang in lstIdDonHang)
            {
                var soLuong = lstDonHangChiTiet.Where(x => x.Id_DonHang == idDonHang).Sum(x => x.SoLuong);
                var tongTien = lstDonHangChiTiet.Where(x => x.Id_DonHang == idDonHang).Sum(x => x.SoLuong * x.DonGia);
                resp.Add(new DonHangResp
                {
                    Id = idDonHang,
                    SoLuong = soLuong,
                    TongTien = tongTien
                });
            }
            // lay ds trang thai don hang
            var lstTrangThaiDonHang = await _context.TrangThaiDonHangs.ToListAsync();
            // ls ds trangthaidonhang theo don hang
            var lstHonHangTrangThai = await _context.DonHang_TrangThaiDonHangs.Where(x => lstIdDonHang.Contains(x.Id_DonHang)).OrderBy(x => x.Id_DonHang).ToListAsync();
            var timeAndTrangThai = new List<object>();
            foreach (var idDonHang in lstIdDonHang)
            {
                var first = lstHonHangTrangThai.Where(x => x.Id_DonHang == idDonHang).OrderBy(x => x.Id).FirstOrDefault();
                var last = lstHonHangTrangThai.Where(x => x.Id_DonHang == idDonHang).OrderBy(x => x.Id).LastOrDefault();
                resp.Where(x => x.Id == idDonHang).Select(x => new DonHangResp
                {
                    Id = x?.Id,
                    SoLuong = x?.SoLuong,
                    TongTien = x?.TongTien,
                    NgayDatHang = first?.ThoiGianTao,
                    TrangThaiDonHang = lstTrangThaiDonHang?.Where(a => a.Id == last?.Id_TrangThaiDonHang).Select(a => new TrangThaiDonHang
                    {
                        Id = a?.Id,
                        Ten = a?.Ten
                    }).First()
                });
            }

            return resp;
        }

        public async Task<DonHangChiTietResp> DonHangChiTiet(int? id)
        {
            var resp = new DonHangChiTietResp();
            if (id == null)
            {
                return resp;
            }

            var donHang = await _context.DonHangs.FirstOrDefaultAsync(x => x.Id == id);
            if (donHang == null)
                return resp;
            var diaChi = new BaseLibary.Entities.DiaChi();
            if (donHang.Id_DiaChi == null)
            {
                diaChi = await _context.DiaChis.FirstOrDefaultAsync(x => x.IsDefalut);
            }
            else
            {
                diaChi = await _context.DiaChis.FirstOrDefaultAsync(x => x.Id == donHang.Id_DiaChi);
            }

            if (diaChi == null)
            {
                return resp;
            }
            var trangThaiDonHang = await _context.DonHang_TrangThaiDonHangs.Include(x => x.Id_TrangThaiDonHang).LastOrDefaultAsync();
            // lay ds don hang chi tiet
            var donHangChiTiet = await _context.DonHangChiTiets.Where(x => x.Id_DonHang == donHang.Id).ToListAsync();
            //lst is sp chi tiet tu don hang
            var lstIdSpChiTiet = donHangChiTiet.Select(x => x.Id_SanPhamChiTiet).Distinct().ToList();

            // lst sp chi tiet
            var lstSpChiTiet = await _context.SanPhamChiTiets.Include(x => x.Id_KichThuoc).Include(x => x.Id_MauSac).Include(x => x.Id_SanPham).Where(x => lstIdSpChiTiet.Contains(x.Id)).ToListAsync();
            var lstSp = lstSpChiTiet.Select(x => x.SanPham).Distinct().ToList();
            var lstHinhAnh = await _context.HinhAnhs.ToListAsync();
            // lay don gia sp
            var lstDonGiaSp = lstSpChiTiet.Select(x => new
            {
                Id_Sp = x.SanPham?.Id,
                DonGia = donHangChiTiet.FirstOrDefault(a => x.Id == a.Id_SanPhamChiTiet)?.DonGia
            }).ToList();
            // lst san pham chi tiet resp
            var lstSpChiTietResp = lstSp.Select(x => new SanPhamChiTietResp
            {
                SanPham = new()
                {
                    ChatLieu = x?.ChatLieu,
                    Id = x?.Id,
                    DonGia = lstDonGiaSp.FirstOrDefault(a => a.Id_Sp == x.Id)?.DonGia,
                    MoTa = x?.MoTa,
                    HuongDanSuDung = x?.HuongDanSuDung,
                    Ten = x?.Ten,
                    TrangThai = x?.TrangThai,
                    UrlImg = lstHinhAnh?.Where(a => a.Id_SanPham == x?.Id && a.Id_MauSac == x?.Id)?.Select(a => new HinhAnhResp
                    {
                        Id = a.Id,
                        Url = a.Url,
                    }).ToList()
                },
                SanPhamChiTiet = lstSpChiTiet?.Where(a => a.Id_SanPham == x.Id)?.Select(a => new SanPhamChiTietModel
                {
                    Id = a.Id,
                    KichThuoc = new KichThuocResp
                    {
                        Id = a?.KichThuoc?.Id,
                        Ten = a?.KichThuoc?.Ten
                    },
                    MauSac = new MauSacResp()
                    {
                        Id = a?.MauSac?.Id,
                        MauSac = a?.MauSac?.Ten
                    },
                    SoLuong = donHangChiTiet?.First(b => a?.Id == b.Id_SanPhamChiTiet)?.SoLuong,

                }).ToList()
            }).ToList();
            decimal? tongTien = 0;
            lstSpChiTietResp?.Select(x => new
            {
                tongTungSp = (decimal?)(x.SanPham?.DonGia * x.SanPhamChiTiet?.Count)
            }).ToList().ForEach(x => { tongTien += x.tongTungSp; });
            resp = new DonHangChiTietResp
            {
                DiaChi = new DiaChiDatHangResp
                {
                    Id = diaChi?.Id,
                    DiaChiChiTiet = diaChi?.DiaChiChiTiet,
                    Sdt = diaChi?.Sdt,
                    TenNguoiNhan = diaChi?.TenNguoiNhan,
                    Url = diaChi?.Url
                },
                HinhThucGiaoHang = "Giao hàng tại nhà",
                HinhThucThanhToan = "Thanh toán khi nhận hàng",
                NgayMuaHang = donHang.ThoiGianTao,
                SanPhamChiTiet = lstSpChiTietResp,
                TongTien = tongTien,
                TrangThaiDonHang = new()
                {
                    Id = trangThaiDonHang?.TrangThaiDonHang?.Id,
                    Ten = trangThaiDonHang?.TrangThaiDonHang?.Ten
                }
            };
            return resp;
        }

        public async Task<List<DiaChiDatHangResp>> DanhSachDiaChiGiaoHang(int? id)
        {
            var resp = new List<DiaChiDatHangResp>();
            if (id == null)
            {
                return resp;
            }
            var kh = await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id == id);
            if (kh == null)
            {
                return resp;
            }
            var lstDiaChiKH = await _context.DiaChis.Where(x => x.Id_KhachHang == kh.Id)?.ToListAsync();
            resp = lstDiaChiKH.Select(x => new DiaChiDatHangResp
            {
                Id = x?.Id,
                DiaChiChiTiet = x?.DiaChiChiTiet,
                Sdt = x?.Sdt,
                IsDefault = x?.IsDefalut,
                TenNguoiNhan = x?.TenNguoiNhan,
                Url = x?.Url,
                Email = x?.Email,
                GhiChu = x?.GhiChu,
                ThanhPho_Code = x?.Province_code,
                Huyen_Code = x?.District_code,
                Xa_Code = x?.Ward_code,
            }).ToList();
            return resp;
        }

        public async Task<GeneralRespone> CapNhatDiaChiGiaoHang(DiaChiDatHangParam param)
        {
            if (param == null || param.Id == null)
            {
                return new GeneralRespone(false, "Cập nhật địa chỉ giao hàng thất bại");
            }

            var diaChi = await _context.DiaChis.FirstOrDefaultAsync(x => x.Id == param.Id);
            if (diaChi == null)
            {
                return new GeneralRespone(false, "Cập nhật địa chỉ giao hàng thất bại");
            }
            var thanhPho = await _context.Provinces.FirstOrDefaultAsync(x => x.code == param.ThanhPho_Code);
            var huyen = await _context.Districts.FirstOrDefaultAsync(x => x.code == param.Huyen_Code);
            var xa = await _context.Wards.FirstOrDefaultAsync(x => x.code == param.Xa_Code);
            var url = $"{xa?.full_name}, {huyen?.full_name}, {thanhPho?.full_name}";
            diaChi.Email = string.IsNullOrEmpty(param.Email) ? diaChi.Email : param.Email;
            diaChi.GhiChu = string.IsNullOrEmpty(param.GhiChu) ? diaChi.GhiChu : param.GhiChu;
            diaChi.Sdt = string.IsNullOrEmpty(param.Sdt) ? diaChi.Sdt : param.Sdt;
            diaChi.TenNguoiNhan = string.IsNullOrEmpty(param.TenNguoiNhan) ? diaChi.TenNguoiNhan : param.TenNguoiNhan;
            diaChi.DiaChiChiTiet = string.IsNullOrEmpty(param.DiaChiChiTiet) ? diaChi.DiaChiChiTiet : param.DiaChiChiTiet;
            diaChi.Url = string.IsNullOrEmpty(url) ? diaChi.Url : url;
            diaChi.IsDefalut = param.isDefault;
            diaChi.Province_code = string.IsNullOrEmpty(thanhPho?.code) ? diaChi.Province_code : thanhPho?.code;
            diaChi.District_code = string.IsNullOrEmpty(huyen?.code) ? diaChi.District_code : huyen?.code;
            diaChi.Ward_code = string.IsNullOrEmpty(xa?.code) ? diaChi.Ward_code : xa?.code;

            await _context.SaveChangesAsync();

            return new GeneralRespone(true, "Cập nhật địa chỉ giao hàng thành công");
        }

        public async Task<GeneralRespone> XoaDiaChiGiaoHang(int? idDiaChi, int? id)
        {
            if (id == null || idDiaChi == null)
            {
                return new GeneralRespone(false, "Xóa địa chỉ giao hàng thất bại");
            }
            var kh = await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id == id);
            if (kh == null)
            {
                return new GeneralRespone(false, "Xóa địa chỉ giao hàng thất bại");
            }
            var lstDiaChi = await _context.DiaChis.Where(x => x.Id_KhachHang == id).ToListAsync();
            if (lstDiaChi.Count <= 1)
            {
                return new GeneralRespone(false, "Xóa địa chỉ giao hàng thất bại");
            }
            var diaChi = await _context.DiaChis.Where(x => x.Id == idDiaChi).FirstOrDefaultAsync(x => x.Id_KhachHang == kh.Id);
            if (diaChi == null)
            {
                return new GeneralRespone(false, "Xóa địa chỉ giao hàng thất bại");
            }
            if (diaChi.IsDefalut == true)
            {
                return new GeneralRespone(false, "Xóa địa chỉ giao hàng thất bại");
            }
            _context.DiaChis.Remove(diaChi);
            await _context.SaveChangesAsync();
            return new GeneralRespone(true, "Xóa địa chỉ giao hàng thành công");
        }

        public async Task<GeneralRespone> CapNhatMatKhau(CapNhatMatKhauParam param)
        {
            if (param == null || param.Id == null)
            {
                return new GeneralRespone(false, "Cập nhật mật khẩu thất bại");
            }
            var kh = await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id == param.Id);

            if (kh == null)
            {
                return new GeneralRespone(false, "Cập nhật mật khẩu thất bại");
            }
            var taiKhoan = await _context.TaiKhoans.FirstOrDefaultAsync(x => x.Id == kh.Id_TaiKhoan);
            var mkCu = BCrypt.Net.BCrypt.HashPassword(param?.OldPassword?.Trim());
            if (!BCrypt.Net.BCrypt.Verify(mkCu, taiKhoan.MatKhau))
            {
                return new GeneralRespone(false, "Cập nhật mật khẩu thất bại");
            }
            var mkMoi = BCrypt.Net.BCrypt.HashPassword(param?.NewPassword?.Trim());
            taiKhoan.MatKhau = mkMoi;
            await _context.SaveChangesAsync();
            return new GeneralRespone(false, "Cập nhật mật khẩu thành công");
        }

        public async Task<List<ThanhPhoResp>> DanhSachThanhPho()
        {
            var lstThanhPho = await _context.Provinces.Where(x => x.administrative_unit_id == 1 || x.administrative_unit_id == 2).Select(x => new ThanhPhoResp
            {
                Code = x.code,
                Name = x.name,
                Full_Name = x.full_name,
                Code_Name = x.code_name
            }).ToListAsync();

            return lstThanhPho;
        }

        public async Task<List<HuyenResp>> DanhSachHuyenTheoId(string? code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return new List<HuyenResp>();
            }

            var thanhPho = await _context.Provinces.FirstOrDefaultAsync(x => x.code == code);
            if (thanhPho == null)
            {
                return new List<HuyenResp>();
            }
            var lstHuyen = await _context.Districts.Where(x => thanhPho.code == x.province_code).Select(x => new HuyenResp
            {
                Code = x.code,
                Name = x.name,
                Full_Name = x.full_name,
                Code_Name = x.code_name,
                Code_ThanhPho = x.province_code
            }).ToListAsync();

            return lstHuyen;
        }

        public async Task<List<PhuongResp>> DanhSachXaTheoId(string? code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return new List<PhuongResp>();
            }

            var thanhPho = await _context.Districts.FirstOrDefaultAsync(x => x.code == code);
            if (thanhPho == null)
            {
                return new List<PhuongResp>();
            }
            var lstPhuong = await _context.Wards.Where(x => thanhPho.code == x.district_code).Select(x => new PhuongResp
            {
                Code = x.code,
                Name = x.name,
                Full_Name = x.full_name,
                Code_Name = x.code_name,
                Code_Huyen = x.district_code
            }).ToListAsync();

            return lstPhuong;
        }

        public async Task<GeneralRespone> ThemMoiDiaChiGiaoHang(DiaChiDatHangParam param, int? id)
        {
            if (id == null)
            {
                return new GeneralRespone(false, "Thêm mới địa chỉ giao hàng thất bại");
            }

            var kh = await _context.KhachHangs.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (kh == null)
            {
                return new GeneralRespone(false, "Thêm mới địa chỉ giao hàng thất bại");
            }
            var thanhPho = await _context.Provinces.FirstOrDefaultAsync(x => x.code == param.ThanhPho_Code);
            var huyen = await _context.Districts.FirstOrDefaultAsync(x => x.code == param.Huyen_Code);
            var xa = await _context.Wards.FirstOrDefaultAsync(x => x.code == param.Xa_Code);
            var url = $"{xa?.full_name}, {huyen?.full_name}, {thanhPho?.full_name}";
            var isAnyDiaChi = _context.DiaChis.Any();
            var diaChiMoi = new BaseLibary.Entities.DiaChi
            {
                Email = param?.Email,
                Sdt = param?.Sdt ?? "098182737",
                Id_KhachHang = kh.Id,
                DiaChiChiTiet = param?.DiaChiChiTiet,
                Url = url,
                Province_code = param?.ThanhPho_Code,
                District_code = param?.Huyen_Code,
                Ward_code = param?.Xa_Code,
                GhiChu = param?.GhiChu,
                TenNguoiNhan = param?.TenNguoiNhan ?? "a",
            };
            if (!isAnyDiaChi)
            {
                diaChiMoi.IsDefalut = true;
                await _context.DiaChis.AddAsync(diaChiMoi);
                await _context.SaveChangesAsync();
                return new GeneralRespone(true, "Thêm mới địa chỉ giao hàng thành công");
            }
            if (isAnyDiaChi && param.isDefault)
            {
                var lstDiaChi = await _context.DiaChis.Where(x => x.Id_KhachHang == kh.Id).ToListAsync();
                foreach (var x in lstDiaChi)
                {
                    x.IsDefalut = false;
                }
                diaChiMoi.IsDefalut = true;
                await _context.DiaChis.AddAsync(diaChiMoi);
                await _context.SaveChangesAsync();
                return new GeneralRespone(true, "Thêm mới địa chỉ giao hàng thành công");
            }
            else if (isAnyDiaChi && !param.isDefault)
            {
                diaChiMoi.IsDefalut = false;
                await _context.DiaChis.AddAsync(diaChiMoi);
                await _context.SaveChangesAsync();
                return new GeneralRespone(true, "Thêm mới địa chỉ giao hàng thành công");
            }
            diaChiMoi.IsDefalut = false;
            await _context.DiaChis.AddAsync(diaChiMoi);
            await _context.SaveChangesAsync();
            return new GeneralRespone(true, "Thêm mới địa chỉ giao hàng thành công");
        }
    }
}
