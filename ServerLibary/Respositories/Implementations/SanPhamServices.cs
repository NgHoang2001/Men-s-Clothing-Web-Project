using BaseLibary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibary.Context;
using ServerLibary.Model.Params;
using ServerLibary.Model.Responses;
using ServerLibary.Respositories.Contracts;

namespace ServerLibary.Respositories.Implementations
{
    public class SanPhamServices : ISanPhamServices
    {
        private DuAnWebCanifaDbContext _context;
        public SanPhamServices(DuAnWebCanifaDbContext context)
        {
            _context = context;
        }

        public async Task<List<DanhMucResp>> DanhSachDanhMuc(DanhMucParam? param)
        {
            var resp = new List<DanhMucResp>();
            // Trả ra toàn bộ danh sách danh mục theo dạng cây
            if (param == null || param.id_danhmuc == null)
            {
                var listDm = await _context.DanhMucs.OrderBy(x => x.Id_DanhMuc_Cha).ToListAsync();
                if (listDm.Count == 0 || listDm == null)
                {
                    return resp;
                }
                // ds hinhanh
                var lsHinhAnh = await _context.HinhAnhs.Where(x => x.Id_DanhMuc != null).OrderByDescending(x => x.Id).ToListAsync();
                // lay ds id cha
                var listIdDmCha = listDm.Where(x => x.Id_DanhMuc_Cha != null).Select(x => x.Id_DanhMuc_Cha).Distinct().ToList();
                // lay ds danh muc cha
                var lsDmCha = listDm.Where(x => listIdDmCha.Contains(x.Id)).Distinct().OrderBy(x => x.Id).ToList();
                // lay ds danh muc khong co id cha
                var lsDmKoCha = listDm.Where(x => !listIdDmCha.Contains(x.Id)).Distinct().OrderBy(x => x.Id).ToList();
                // ds hinh anh phan loai theo danh muc
                var lsIdDm = listDm.Select(x => x.Id).ToList();
                var lsHADm = lsHinhAnh.Where(x => lsIdDm.Contains((int)x.Id_DanhMuc)).ToList();

                resp = lsDmCha.Select(x => new DanhMucResp
                {
                    Id = x.Id,
                    Ten = x.Ten,
                    Id_DanhMuc_Cha = x.Id_DanhMuc_Cha,
                    HinhAnh = lsHADm.Where(a => a.Id_DanhMuc == x.Id && a.isBanner == false).Select(a => new HinhAnhResp
                    {
                        Id = a.Id,
                        Kieu = a.Kieu,
                        Url = a.Url,
                        Id_DanhMuc = a.Id_DanhMuc,

                    }).FirstOrDefault(),
                    HinhAnhBanner = lsHADm.Where(a => a.Id_DanhMuc == x.Id && a.isBanner == true).Select(a => new HinhAnhResp
                    {
                        Id = a.Id,
                        Kieu = a.Kieu,
                        Url = a.Url,
                        Id_DanhMuc = a.Id_DanhMuc,
                        IsBanner = true
                    }).FirstOrDefault(),
                    DanhMucChildren = listDm.Where(a => a.Id_DanhMuc_Cha == x.Id).Select(a => new DanhMucChild
                    {
                        Id = a.Id,
                        Ten = a.Ten,
                        Id_DanhMuc_Cha = a.Id_DanhMuc_Cha,
                        HinhAnh = lsHADm.Where(b => b.Id_DanhMuc == a.Id && b.isBanner == false).Select(b => new HinhAnhResp
                        {
                            Id = b.Id,
                            Kieu = b.Kieu,
                            Url = b.Url,
                            Id_DanhMuc = b.Id_DanhMuc,
                        }).FirstOrDefault(),
                        HinhAnhBanner = lsHADm.Where(b => b.Id_DanhMuc == a.Id && b.isBanner == true).Select(b => new HinhAnhResp
                        {
                            Id = b.Id,
                            Kieu = b.Kieu,
                            Url = b.Url,
                            Id_DanhMuc = b.Id_DanhMuc,
                            IsBanner = true
                        }).FirstOrDefault(),
                    }).ToList(),
                }).ToList();
            }
            else
            {
                var dmCha = await _context.DanhMucs.FirstOrDefaultAsync(x => x.Id == param.id_danhmuc);
                if (dmCha == null)
                {
                    return resp;
                }
                // ds dm theo dm cha
                var listDm = await _context.DanhMucs.Where(x => x.Id_DanhMuc_Cha == dmCha.Id).ToListAsync();
                // ds hinhanh
                var lsHinhAnh = await _context.HinhAnhs.Where(x => x.Id_DanhMuc != null).Where(x => !(param.isBanner != null) || x.isBanner == param.isBanner).OrderByDescending(x => x.Id).ToListAsync();
                // ds hinh anh phan loai theo danh muc
                var lsIdDm = listDm.Select(x => x.Id).ToList();
                var lsHADm = lsHinhAnh.Where(x => lsIdDm.Contains((int)x.Id_DanhMuc)).ToList();

                resp = new List<DanhMucResp>(){ new DanhMucResp
                {
                    Id = dmCha.Id,
                    Ten = dmCha.Ten,
                    HinhAnh = lsHinhAnh.Where(a => a.Id_DanhMuc == dmCha.Id)?.Select(a => new HinhAnhResp
                    {
                        Id = a.Id,
                        Kieu = a.Kieu,
                        Url = a.Url,
                        Id_DanhMuc = a.Id_DanhMuc,
                    })?.FirstOrDefault(),
                    HinhAnhBanner = lsHinhAnh.Where(a => a.Id_DanhMuc == dmCha.Id && a.isBanner)?.Select(a => new HinhAnhResp
                    {
                        Id = a.Id,
                        Kieu = a.Kieu,
                        Url = a.Url,
                        Id_DanhMuc = a.Id_DanhMuc,
                        IsBanner = true
                    })?.FirstOrDefault(),
                    DanhMucChildren = listDm.Select(a => new DanhMucChild
                    {
                        Id = a.Id,
                        Ten = a.Ten,
                        HinhAnh = lsHADm.Where(a => a.Id_DanhMuc == dmCha.Id)?.Select(a => new HinhAnhResp
                        {
                            Id = a.Id,
                            Kieu = a.Kieu,
                            Url = a.Url,
                            Id_DanhMuc = a.Id_DanhMuc,
                        })?.FirstOrDefault(),
                        HinhAnhBanner = lsHADm.Where(a => a.Id_DanhMuc == dmCha.Id && a.isBanner)?.Select(a => new HinhAnhResp
                        {
                            Id = a.Id,
                            Kieu = a.Kieu,
                            Url = a.Url,
                            Id_DanhMuc = a.Id_DanhMuc,
                            IsBanner = true
                        })?.FirstOrDefault(),
                    }).ToList(),
                }}.ToList();
            }
            return resp;
        }

        public async Task<List<HinhAnhResp>> DanhSachHinhAnhBanner()
        {
            var resp = new List<HinhAnhResp>();

            var lstHinhAnh = await _context.HinhAnhs.Where(x => x.isBanner).Where(x => x.Id_DanhMuc == null && x.Id_MauSac == null && x.Id_SanPham == null && x.Id_SanPhamChiTiet == null).OrderByDescending(x => x.Id).Take(3).ToListAsync();
            if (lstHinhAnh == null || lstHinhAnh.Count == 0)
            {
                return resp;
            }
            resp = lstHinhAnh.Select(x => new HinhAnhResp
            {
                Id = x.Id,
                Url = x.Url,
                IsBanner = x.isBanner
            }).ToList();

            return resp;
        }

        // id san pham
        public async Task<List<KichThuocResp>> DanhSachKichThuoc(int? id)
        {
            var resp = new List<KichThuocResp>();
            if (id == null)
            {
                return new List<KichThuocResp>();
            }
            var sp = await _context.SanPhams.FirstOrDefaultAsync(x => x.Id == id);
            if (sp == null)
            {
                return resp;
            }

            var lsChiTietSp = await _context.SanPhamChiTiets.Where(x => x.Id_SanPham == sp.Id).ToListAsync();
            if (lsChiTietSp.Count == 0 || lsChiTietSp == null)
            {
                return resp;
            }

            var lsIdKichThuocSp = lsChiTietSp.Select(x => x.Id_KichThuoc).Distinct().ToList();
            var lsKichThuoc = await _context.KichThuocs.ToListAsync();
            resp = lsKichThuoc.Where(x => lsIdKichThuocSp.Contains(x.Id)).Select(x => new KichThuocResp
            {
                Id = x.Id,
                Ten = x.Ten
            }).ToList();
            return resp;
        }

        public async Task<List<MauSacResp>> DanhSachMauSac(int? id)
        {
            var resp = new List<MauSacResp>();
            if (id == null)
            {
                return new List<MauSacResp>();
            }
            var sp = await _context.SanPhams.FirstOrDefaultAsync(x => x.Id == id);
            if (sp == null)
            {
                return resp;
            }

            var lsChiTietSp = await _context.SanPhamChiTiets.Where(x => x.Id_SanPham == sp.Id).ToListAsync();
            if (lsChiTietSp.Count == 0 || lsChiTietSp == null)
            {
                return resp;
            }

            var lsIdMauSacSp = lsChiTietSp.Select(x => x.Id_MauSac).Distinct().ToList();
            var lsKichThuoc = await _context.MauSacs.ToListAsync();
            resp = lsKichThuoc.Where(x => lsIdMauSacSp.Contains(x.Id)).Select(x => new MauSacResp
            {
                Id = x.Id,
                Ten = x.Ten,
                MauSac = x.Url
            }).ToList();
            return resp;
        }

        // Lay ds san pham theo danh muc
        public async Task<List<SanPhamResp>> DanhSachSanPham(SanPhamParam? param)
        {
            var resp = new List<SanPhamResp>();
            var dsDm = new List<DanhMuc>();
            if (param == null || param.id_danhmuc == null)
            {
                // Lấy toàn bộ danh sách sản phẩm cho trang trang chủ
                dsDm = await _context.DanhMucs.Where(x => x.Id_DanhMuc_Cha != null).Take(3).ToListAsync();
            }
            else
            {
                // ds dm cha va dm con
                dsDm = await _context.DanhMucs.Where(x => x.Id == param.id_danhmuc || x.Id_DanhMuc_Cha == param.id_danhmuc)?.ToListAsync();

            }
            if (dsDm == null || dsDm.Count == 0)
            {
                return resp;
            }
            var lsSp = await _context.SanPhams.Where(x => x.TrangThai).ToListAsync();
            if (lsSp == null || lsSp.Count == 0)
            {
                return resp;
            }
            // Loc sp them danh muc
            var lsIdDm = dsDm.Select(x => x.Id).ToList();
            var lsSpDm = lsSp.Where(x => lsIdDm.Contains((int)x.Id_DanhMuc))?.ToList();
            if (lsSpDm == null || lsSpDm.Count == 0)
            {
                return resp;
            }
            // Ds hinh anh
            var lsHinhAnh = await _context.HinhAnhs.Where(x => x.Id_SanPham != null).ToListAsync();
            var lsHinhAnhDm = await _context.HinhAnhs.Where(x => x.Id_DanhMuc != null).OrderByDescending(x => x.Id).ToListAsync();

            resp = dsDm.Select(x => new SanPhamResp
            {
                DanhMuc = new DanhMucResp
                {
                    Id = x?.Id,
                    Ten = x?.Ten,
                    Id_DanhMuc_Cha = x?.Id_DanhMuc_Cha,
                    HinhAnh = lsHinhAnhDm?.Where(a => a.Id_DanhMuc == x?.Id && a.isBanner == false)?.Select(a => new HinhAnhResp
                    {
                        Id = a?.Id,
                        Id_DanhMuc = a?.Id_DanhMuc,
                        Kieu = a?.Kieu,
                        Url = a?.Url
                    }).FirstOrDefault(),
                    HinhAnhBanner = lsHinhAnhDm?.Where(a => a.Id_DanhMuc == x?.Id && a.isBanner == true)?.Select(a => new HinhAnhResp
                    {
                        Id = a?.Id,
                        Id_DanhMuc = a?.Id_DanhMuc,
                        Kieu = a?.Kieu,
                        Url = a?.Url
                    }).FirstOrDefault()
                },
                SanPham = lsSpDm?.Where(a => a.Id_DanhMuc == x?.Id)?.Select(a => new SanPhamModel
                {
                    Id = a?.Id,
                    ChatLieu = a?.ChatLieu,
                    DonGia = a?.DonGia,
                    MoTa = a?.MoTa,
                    HuongDanSuDung = a?.HuongDanSuDung,
                    Ten = a?.Ten,
                    TrangThai = a?.TrangThai,
                    UrlImg = lsHinhAnh?.Where(b => b.Id_SanPham == a?.Id)?.Select(b => new HinhAnhResp
                    {
                        Id = b?.Id,
                        Id_DanhMuc = b?.Id_DanhMuc,
                        Kieu = b?.Kieu,
                        Url = b?.Url,
                        Id_SanPham = b?.Id_SanPham,
                    }).Take(1).ToList(),
                }).ToList(),
            }).ToList();
            return resp;
        }

        public async Task<SanPhamChiTietResp> SanPhamChiTiet(int? id_SanPham)
        {
            var resp = new SanPhamChiTietResp();
            if (id_SanPham == null)
            {
                return resp;
            }
            var sp = await _context.SanPhams.FirstOrDefaultAsync(x => x.Id == id_SanPham);
            if (sp == null)
            {
                return resp;
            }
            var lsSpChiTiet = await _context.SanPhamChiTiets.Where(x => x.Id_SanPham == sp.Id).ToListAsync();

            var lsMau = await _context.MauSacs.ToListAsync();
            var lsKichThuoc = await _context.KichThuocs.ToListAsync();
            var lsHinhAnh = await _context.HinhAnhs.Where(x => x.Id_SanPham == sp.Id)?.ToListAsync();
            var dm = await _context.DanhMucs.FirstOrDefaultAsync(x => x.Id == sp.Id_DanhMuc);
            resp = new SanPhamChiTietResp
            {
                SanPham = new SanPhamModel
                {
                    Id = sp.Id,
                    Ten = sp?.Ten,
                    ChatLieu = sp?.ChatLieu,
                    DonGia = sp?.DonGia,
                    HuongDanSuDung = sp?.HuongDanSuDung,
                    MoTa = sp?.MoTa,
                    TrangThai = sp?.TrangThai,
                    UrlImg = lsHinhAnh.Select(x => new HinhAnhResp
                    {
                        Id = x.Id,
                        Id_SanPham = x.Id_SanPham,
                        Kieu = x.Kieu,
                        Url = x.Url,
                    }).ToList()
                },
                DanhMuc = new DanhMucResp
                {
                    Id = dm?.Id,
                    Ten = dm?.Ten,
                    Id_DanhMuc_Cha = dm?.Id_DanhMuc_Cha,

                },
                SanPhamChiTiet = lsSpChiTiet?.Select(x => new SanPhamChiTietModel
                {
                    Id = x?.Id,
                    SoLuong = x?.SoLuong,
                    KichThuoc = lsKichThuoc?.Where(a => a.Id == x.Id_KichThuoc)?.Select(a => new KichThuocResp
                    {
                        Id = a?.Id,
                        Ten = a?.Ten
                    }).FirstOrDefault(),
                    MauSac = lsMau?.Where(a => a.Id == x.Id_MauSac)?.Select(a => new MauSacResp
                    {
                        Id = a?.Id,
                        Ten = a?.Ten,
                        MauSac = a?.Url
                    }).FirstOrDefault(),
                }).ToList()
            };
            return resp;
        }
    }
}
