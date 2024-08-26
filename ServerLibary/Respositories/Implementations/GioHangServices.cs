using BaseLibary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibary.Context;
using ServerLibary.Model.Params;
using ServerLibary.Model.Responses;
using ServerLibary.Respositories.Contracts;

namespace ServerLibary.Respositories.Implementations
{
    public class GioHangServices : IGioHangServices
    {
        private DuAnWebCanifaDbContext _context;

        public GioHangServices(DuAnWebCanifaDbContext context)
        {
            _context = context;
        }

        public async Task<List<SanPhamGioHangResp>> DanhSachSanPhamGioHang(int? id)
        {
            List<SanPhamGioHangResp> resp = null;
            if (id == null)
                return resp;

            var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id == id);
            if (khachHang == null)
                return resp;

            var lstDonHang = await _context.GioHangChiTiets.Where(x => x.Id_KhachHang == khachHang.Id).ToListAsync();

            var lstIdSpCT = lstDonHang.Select(x => x.Id_SanPhamChiTiet).ToList();

            var lstSpChiTiet = await _context.SanPhamChiTiets.Where(x => lstIdSpCT.Contains(x.Id)).ToListAsync();
            var lstIdSp = lstSpChiTiet.Select(x => x.Id_SanPham).Distinct().ToList();
            var lstSp = await _context.SanPhams.Where(x => lstIdSp.Contains(x.Id)).ToListAsync();

            //lst hinh anh
            var lstHinhAnh = await _context.HinhAnhs.Where(x => lstIdSp.Contains((int)x.Id_SanPham)).Select(x => new
            {
                Id = x.Id,
                Id_SanPhamChiTiet = x.Id_SanPhamChiTiet,
                Id_SanPham = x.Id_SanPham,
                Url = x.Url,
                Kieu = x.Kieu
            }).ToListAsync();

            var lstMauSac = await _context.MauSacs.ToListAsync();
            var lstKichThuoc = await _context.KichThuocs.ToListAsync();

            resp = lstDonHang.Select(x => new SanPhamGioHangResp
            {
                Id = x?.Id,
                SoLuong = x?.SoLuong,
                IsChon = x?.IsChon ?? true,
                SanPham = lstSp.Where(a => lstSpChiTiet.First(c => c.Id == x?.Id_SanPhamChiTiet)?.Id_SanPham == a.Id)?.Select(a => new SanPhamModel
                {
                    Id = a?.Id,
                    ChatLieu = a?.ChatLieu,
                    DonGia = a?.DonGia,
                    HuongDanSuDung = a?.HuongDanSuDung,
                    MoTa = a?.MoTa,
                    Ten = a?.Ten,
                    TrangThai = a?.TrangThai,
                    UrlImg = null
                })?.FirstOrDefault(),
                SanPhamChiTiet = lstSpChiTiet.Where(a => a.Id == x?.Id_SanPhamChiTiet).Select(a => new SanPhamChiTietModel
                {
                    Id = a?.Id,
                    MauSac = lstMauSac.Where(c => a.Id_MauSac == c.Id).Select(c => new MauSacResp
                    {
                        Id = c?.Id,
                        MauSac = c?.Url,
                        Ten = c?.Ten
                    }).FirstOrDefault(),
                    HinhAnhResp = lstHinhAnh.Where(c => c.Id_SanPham == a?.Id_SanPham).Select(c => new HinhAnhResp
                    {
                        Id = c?.Id,
                        Id_SanPhamChiTiet = c?.Id_SanPhamChiTiet,
                        Url = c?.Url,
                        Kieu = c?.Kieu,
                    }).FirstOrDefault(),
                    KichThuoc = lstKichThuoc.Where(c => a?.Id_KichThuoc == c.Id).Select(c => new KichThuocResp
                    {
                        Id = c?.Id,
                        Ten = c?.Ten
                    }).FirstOrDefault()
                }).FirstOrDefault()
            }).ToList();
            return resp;
        }

        public async Task<GeneralRespone> ThemSanPhamVaoGioHang(ThemSanPhamGioHangParam param)
        {
            if (param.Id_KhachHang == null)
            {
                return new GeneralRespone(false, "Thêm mới sản phẩm vào giỏ hàng thất bại");
            }
            var kh = await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id == param.Id_KhachHang);
            if (kh == null)
            {
                return new GeneralRespone(false, "Thêm mới sản phẩm vào giỏ hàng thất bại");
            }
            var spChiTiet = await _context.SanPhamChiTiets.FirstOrDefaultAsync(x => param.Id_SanPhamChiTiet != null && x.Id == param.Id_SanPhamChiTiet && x.SoLuong != 0);
            if (spChiTiet == null)
            {
                return new GeneralRespone(false, "Thêm mới sản phẩm vào giỏ hàng thất bại");
            }
            var spGiohang = await _context.GioHangChiTiets.FirstOrDefaultAsync(x => x.Id_SanPhamChiTiet == spChiTiet.Id && x.Id_KhachHang == kh.Id);
            if (spGiohang == null)
            {
                _context.GioHangChiTiets.Add(new BaseLibary.Entities.GioHangChiTiet
                {
                    Id_KhachHang = kh.Id,
                    Id_SanPhamChiTiet = spChiTiet.Id,
                    SoLuong = 1,
                });

            }
            else
            {
                spGiohang.SoLuong = spGiohang.SoLuong + 1;
            }
            await _context.SaveChangesAsync();
            return new GeneralRespone(false, "Thêm mới sản phẩm vào giỏ hàng thành công");
        }

        public async Task<List<KichThuocVaMauSacSanPhamDonHangResp>> DanhSachKichThuocVaMauSacSanPhamDonHang(int? id)
        {
            List<KichThuocVaMauSacSanPhamDonHangResp> resp = null;
            if (id == null)
            {
                return resp;
            }
            var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id == id);
            if (khachHang == null)
            {
                return resp;
            }
            var lstGioHang = await _context.GioHangChiTiets.Where(x => khachHang.Id == x.Id_KhachHang).ToListAsync();
            if (khachHang == null)
            {
                return resp;
            }
            var lstIdSpChiTiet = lstGioHang.Select(x => x.Id_SanPhamChiTiet).Distinct().ToList();

            var lstSpChiTiet = await _context.SanPhamChiTiets.Where(x => lstIdSpChiTiet.Contains(x.Id)).ToListAsync();
            var lstMSSpChiTiet = lstSpChiTiet.Select(x => new
            {
                id_Kh = x.Id,
                id_MauSac = x.Id_MauSac
            }).Distinct().ToList();
            var lstKTSpChiTiet = lstSpChiTiet.Select(x => new
            {
                id_Kh = x.Id,
                id_KichThuoc = x.Id_KichThuoc
            }).Distinct().ToList();
            var lstMauSac = await _context.MauSacs.ToListAsync();
            var lstKichThuoc = await _context.KichThuocs.ToListAsync();

            resp = lstGioHang.Select(x => new KichThuocVaMauSacSanPhamDonHangResp
            {
                Id = x.Id,
                Id_SanPhamChiTiet = x.Id_SanPhamChiTiet,
                KichThuocs = lstKTSpChiTiet.Where(a => x.Id_SanPhamChiTiet == a.id_Kh).Select(a => new KichThuocResp
                {
                    Id = a.id_KichThuoc,
                    Ten = lstKichThuoc.FirstOrDefault(c => a.id_KichThuoc == c.Id)?.Ten,
                }).ToList(),
                MauSacs = lstMSSpChiTiet.Where(a => x.Id_SanPhamChiTiet == a.id_Kh).Select(a => new MauSacResp
                {
                    Id = a.id_MauSac,
                    Ten = lstKichThuoc.FirstOrDefault(c => a.id_MauSac == c.Id)?.Ten,
                    MauSac = lstMauSac.FirstOrDefault(c => a.id_MauSac == c.Id)?.Url,
                }).ToList(),
            }).ToList();
            return resp;
        }

        public async Task<GeneralRespone> CapNhatSanPhamGioHang(int id, int? idKichThuoc, int? idMauSac)
        {
            //if (idKichThuoc == null && idMauSac == null)
            //{
            //    return new GeneralRespone(false, "Cập nhật sản phẩm giỏ hàng thất bại");
            //}
            var gioHang = await _context.GioHangChiTiets.FirstOrDefaultAsync(x => x.Id == id);
            if (gioHang == null)
            {
                return new GeneralRespone(false, "Cập nhật sản phẩm giỏ hàng thất bại");
            }

            var spChiTiet = await _context.SanPhamChiTiets.FirstOrDefaultAsync(x => x.Id == gioHang.Id_SanPhamChiTiet);
            if (spChiTiet == null)
                return new GeneralRespone(false, "Cập nhật sản phẩm giỏ hàng thất bại");

            var spChiTietMoi = await _context.SanPhamChiTiets.FirstOrDefaultAsync(x => spChiTiet.Id_SanPham == x.Id_SanPham && x.Id_KichThuoc == idKichThuoc && x.Id_MauSac == idMauSac);
            if (spChiTietMoi == null)
                return new GeneralRespone(false, "Cập nhật sản phẩm giỏ hàng thất bại");

            var gioHangCTMoi = await _context.GioHangChiTiets.FirstOrDefaultAsync(x => x.Id_SanPhamChiTiet == spChiTietMoi.Id);
            if (gioHangCTMoi == null)
            {
                _context.GioHangChiTiets.Add(new BaseLibary.Entities.GioHangChiTiet
                {
                    Id_KhachHang = gioHang.Id_KhachHang,
                    Id_SanPhamChiTiet = spChiTietMoi.Id,
                    SoLuong = gioHang.SoLuong,

                });
            }
            else
            {
                gioHangCTMoi.SoLuong += gioHang.SoLuong;
            }
            _context.GioHangChiTiets.Remove(gioHang);
            await _context.SaveChangesAsync();
            return new GeneralRespone(true, "Cập nhật sản phẩm giỏ hàng thành công");
        }

        public async Task<GeneralRespone> CapNhatSoLuongSanPhamGioHang(CapNhatSoLuongParam param)
        {
            if (param == null || param.Id == null)
            {
                return new GeneralRespone(false, "Cập nhật số lượng sản phẩm giỏ hàng thất bại");
            }

            var gioHang = await _context.GioHangChiTiets.FirstOrDefaultAsync(x => x.Id == param.Id);
            if (gioHang == null)
            {
                return new GeneralRespone(false, "Cập nhật số lượng sản phẩm giỏ hàng thất bại");
            }
            if (param.IsTang.Equals("+"))
            {

                gioHang.SoLuong++;
            }
            else if (param.IsTang.Equals("-"))
            {
                if (gioHang.SoLuong == 1)
                {
                    return new GeneralRespone(false, "Cập nhật số lượng sản phẩm giỏ hàng thất bại");
                }
                gioHang.SoLuong--;
            }
            else { return new GeneralRespone(false, "Cập nhật số lượng sản phẩm giỏ hàng thất bại"); }
            await _context.SaveChangesAsync();
            return new GeneralRespone(true, "Cập nhật số lượng sản phẩm giỏ hàng thành công");
        }

        public async Task<GeneralRespone> XoaSanPhamGioHang(int? id)
        {
            if (id == null)
            {
                return new GeneralRespone(false, "Xóa sản phẩm giỏ hàng thất bại");
            }
            var gioHang = await _context.GioHangChiTiets.FirstOrDefaultAsync(x => x.Id == id);
            if (gioHang == null)
                return new GeneralRespone(false, "Xóa sản phẩm giỏ hàng thất bại");
            _context.GioHangChiTiets.Remove(gioHang);
            await _context.SaveChangesAsync();
            return new GeneralRespone(true, "Xóa sản phẩm giỏ hàng thành công");
        }

        public async Task<List<KichThuocGiohangResp>> DanhSachKichThuocSanPhamGioHang(int? id)
        {
            var resp = new List<KichThuocGiohangResp>();
            if (id == null)
            {
                return resp;
            }
            var kh = await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id == id);
            if (kh == null)
            {
                return resp;
            }
            var lstGioHang = await _context.GioHangChiTiets.Include(x => x.SanPhamChiTiet).Where(x => x.Id_KhachHang == kh.Id).ToListAsync();
            if (lstGioHang == null || lstGioHang.Count == 0)
            {
                return resp;
            }
            // danh sách spct theo giỏ hàng
            var lstSpChiTiet = lstGioHang.Select(x => x.SanPhamChiTiet).ToList();
            // danh sách idsp theo spct
            var lstIdSp = lstSpChiTiet.Select(x => x.Id_SanPham).Distinct().ToList();
            // danh sách toàn bộ kich thước
            var lstKichThuoc = await _context.KichThuocs.ToListAsync();
            // danh sách spct dựa theo lstIdSp
            var lstSpCt = await _context.SanPhamChiTiets.Where(x => lstIdSp.Contains(x.Id_SanPham)).ToListAsync();
            // danh sách toàn bộ lại kích thước có trong sản phẩm đó
            var lstKt = lstSpCt.Select(x => new
            {
                Id_Sp = x.Id_SanPham,
                Id_KT = x.Id_KichThuoc
            }).Distinct().ToList();

            //var lstKichThuocSpCT = lstSpChiTiet.Select(x => new
            //{
            //    Id_Sp = x?.Id_SanPham,
            //    KichThuoc = lstKichThuoc?.FirstOrDefault(a => x?.Id_KichThuoc == a.Id),
            //    Id_SpCT = x?.Id
            //}).ToList();

            resp = lstSpChiTiet.Select(x => x?.Id_SanPham).ToList().Select(x => new KichThuocGiohangResp
            {
                Id_SP = x,
                ListKichThuoc = lstKt.Where(a => x == a.Id_Sp).Select(a => new KichThuocResp
                {
                    Id = lstKichThuoc.FirstOrDefault(c => c.Id == a.Id_KT)?.Id,
                    Ten = lstKichThuoc.FirstOrDefault(c => c.Id == a.Id_KT)?.Ten
                }).ToList()
            }).ToList();

            return resp;
        }

        public async Task<List<MauSacGioHangResp>> DanhSachMauSacSanPhamGioHang(int? id)
        {
            var resp = new List<MauSacGioHangResp>();
            if (id == null)
            {
                return resp;
            }
            var kh = await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id == id);
            if (kh == null)
            {
                return resp;
            }
            var lstGioHang = await _context.GioHangChiTiets.Include(x => x.SanPhamChiTiet).Where(x => x.Id_KhachHang == kh.Id).ToListAsync();
            if (lstGioHang == null || lstGioHang.Count == 0)
            {
                return resp;
            }
            var lstSpChiTiet = lstGioHang.Select(x => x.SanPhamChiTiet).ToList();
            var lstSpId = lstSpChiTiet.Select(x => x.Id_SanPham).Distinct().ToList();
            var lstMauSac = await _context.MauSacs.ToListAsync();
            var lstSpCT = await _context.SanPhamChiTiets.Where(x => lstSpId.Contains(x.Id_SanPham)).ToListAsync();
            var lstAllMs = lstSpCT.Select(x => new
            {
                Id_Sp = x.Id_SanPham,
                Id_Ms = x.Id_MauSac
            }).Distinct().ToList();
            var lstMauSacSpCT = lstSpChiTiet.Select(x => new
            {
                Id_Sp = x?.Id_SanPham,
                MauSac = lstMauSac?.FirstOrDefault(a => x?.Id_KichThuoc == a.Id),
                Id_SpCT = x?.Id
            }).ToList();

            resp = lstSpChiTiet.Select(x => x?.Id_SanPham).ToList().Select(x => new MauSacGioHangResp
            {
                Id_SP = x,
                ListMauSac = lstAllMs.Where(a => x == a.Id_Sp).Select(a => new MauSacResp
                {
                    Id = lstMauSac.FirstOrDefault(c => c.Id == a.Id_Ms)?.Id,
                    Ten = lstMauSac.FirstOrDefault(c => c.Id == a.Id_Ms)?.Ten,
                    MauSac = lstMauSac.FirstOrDefault(c => c.Id == a.Id_Ms)?.Url,
                }).ToList()
            }).ToList();

            return resp;
        }

        public async Task<GeneralRespone> ThayDoiTrangThaiGioHang(int? id)
        {
            if (id == null)
            {
                return new GeneralRespone(false, "Thay đổi trạng thái sản phẩm giỏ hàng thất bại");
            }
            var gioHang = await _context.GioHangChiTiets.FirstOrDefaultAsync(x => x.Id == id);
            if (gioHang == null)
                return new GeneralRespone(false, "Thay đổi trạng thái sản phẩm giỏ hàng thất bại");
            gioHang.IsChon = !gioHang.IsChon;
            await _context.SaveChangesAsync();
            return new GeneralRespone(true, "Thay đổi trạng thái sản phẩm giỏ hàng thành công");
        }
    }
}
