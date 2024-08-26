using BaseLibary.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerLibary.Context;
using ServerLibary.Model.Responses;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThemDuLieuController : ControllerBase
    {
        private readonly DuAnWebCanifaDbContext _context;
        public ThemDuLieuController(DuAnWebCanifaDbContext context)
        {
            _context = context;
        }
        // GET: ThemDuLieuController
        [HttpGet("add-data-danh-muc")]
        public async Task<IActionResult> AddDataDanhMuc()
        {
            try
            {
                var dataDMCha = new List<DanhMuc>
            {
                new DanhMuc{ Ten = "Áo"},
                new DanhMuc{ Ten = "Áo khoác"},
                new DanhMuc{ Ten = "Quần"}
            };
                //_context.AddRange(dataDMCha);
                //await _context.SaveChangesAsync();
                var entityDmCha = await AddRangeToDatabase<DanhMuc>(dataDMCha);

                //var entityDmCha = new List<DanhMuc>();
                if (entityDmCha != null || entityDmCha.Count > 0)
                {
                    foreach (var dmCha in entityDmCha)
                    {
                        var data = new List<DanhMuc>();
                        switch (dmCha.Ten)
                        {
                            case "Áo":
                                data = new List<DanhMuc>
                                {
                                    new DanhMuc{Id_DanhMuc_Cha = dmCha.Id, Ten = "Áo chống nắng" },
                                    new DanhMuc{Id_DanhMuc_Cha = dmCha.Id, Ten = "Áo Polo"},
                                    new DanhMuc{Id_DanhMuc_Cha = dmCha.Id, Ten = "Áo len giữ nhiệt"},
                                    new DanhMuc{Id_DanhMuc_Cha = dmCha.Id, Ten = "Áo len"},
                                    new DanhMuc{Id_DanhMuc_Cha = dmCha.Id, Ten = "Áo Sơ mi"},
                                };
                                var dataDm = await AddRangeToDatabase(data);

                                break;
                            case "Áo khoác":
                                data = new List<DanhMuc>
                                {
                                    new DanhMuc{Id_DanhMuc_Cha = dmCha.Id, Ten = "Áo khoác phao"},
                                    new DanhMuc{Id_DanhMuc_Cha = dmCha.Id, Ten = "Áo khoác nỉ"},
                                    new DanhMuc{Id_DanhMuc_Cha = dmCha.Id, Ten = "Áo khoác"},
                                    new DanhMuc{Id_DanhMuc_Cha = dmCha.Id, Ten = "Áo khoác gió"},
                                };
                                await AddRangeToDatabase(data);
                                break;
                            case "Quần":
                                data = new List<DanhMuc>
                                {
                                    new DanhMuc{Id_DanhMuc_Cha = dmCha.Id, Ten = "Quần giữ nhiệt"},
                                    new DanhMuc{Id_DanhMuc_Cha = dmCha.Id, Ten = "Quần đùi"},
                                    new DanhMuc{Id_DanhMuc_Cha = dmCha.Id, Ten = "Quần Jeans"},
                                    new DanhMuc{Id_DanhMuc_Cha = dmCha.Id, Ten = "Quần âu"},
                                };
                                await AddRangeToDatabase(data);
                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


            return Ok("Thành công tạo mới token");
        }

        [HttpPost("them-hinh-anh")]
        public async Task<HinhAnh> ThemMoiHinhAnh(HinhAnhResp param)
        {
            var resp = await AddObjToDatabase(new HinhAnh
            {
                Id_DanhMuc = param?.Id_DanhMuc,
                Id_MauSac = param?.Id_MauSac,
                Id_SanPham = param?.Id_SanPham,
                Id_SanPhamChiTiet = param?.Id_SanPhamChiTiet,
                Url = param?.Url,
                isBanner = param?.IsBanner ?? false,
                Kieu = param?.Kieu ?? 0,

            });
            return resp;
        }
        [HttpGet("get-all-danh-muc")]
        public async Task<IActionResult> GetAllDanhMuc()
        {
            var resp = await _context.DanhMucs.ToListAsync();
            return new OkObjectResult(resp);
        }

        [HttpGet("get-all-san-pham")]
        public async Task<IActionResult> GetAllSP()
        {
            var resp = await _context.SanPhams.ToListAsync();
            return new OkObjectResult(resp);
        }
        [HttpGet("add-data-san-pham")]
        public async Task<IActionResult> AddDataSanPham()
        {
            try
            {
                //var trangThaiSp = await AddRangeToDatabase(new List<TrangThaiSanPham>
                //{
                //   new TrangThaiSanPham{ Ten = "Đang bán" }
                //});
                var doiTuongSp = await AddRangeToDatabase(new List<DoiTuong>
                {
                   new DoiTuong{ Ten = "Nam" }
                });
                var lstKichThuoc = await AddRangeToDatabase(new List<KichThuoc>
                {
                    new KichThuoc{ Ten ="S"},
                    new KichThuoc{ Ten ="M"},
                    new KichThuoc{ Ten ="L" },
                    new KichThuoc{ Ten ="XL"},
                    new KichThuoc{ Ten ="XXL"},
                });
                var lstMS = await AddRangeToDatabase(new List<MauSac> {
                                                            new MauSac{Ten="Xanh", MaMau="SB001",Url="https://media.canifa.com/attribute/swatch/images/sb001.webp"},
                                                            new MauSac{Ten="Xám nhạt", MaMau="SA180",Url="https://media.canifa.com/attribute/swatch/images/sa180.webp"},
                                                            new MauSac{Ten="Xám đậm", MaMau="SA206",Url="https://media.canifa.com/attribute/swatch/images/sa206.webp"},
                                                            new MauSac{Ten="Xanh nhạt", MaMau="SB060",Url="https://media.canifa.com/attribute/swatch/images/sb060.webp"},
                                                            new MauSac{Ten="Đen", MaMau="SK010",Url="https://media.canifa.com/attribute/swatch/images/sk010.webp"},
                                                            new MauSac{Ten="Xám", MaMau="SA422",Url="https://media.canifa.com/attribute/swatch/images/sa422.webp"},
                                                            new MauSac{Ten="Xanh", MaMau="SB001",Url="https://media.canifa.com/attribute/swatch/images/sb001.webp"},
                                                            new MauSac{Ten="Xanh", MaMau="SB175",Url="https://media.canifa.com/attribute/swatch/images/sb175.webp"},
                                                            new MauSac{Ten="Trắng", MaMau="SW001",Url="https://media.canifa.com/attribute/swatch/images/sw001.webp"},
                                                            new MauSac{Ten="Xám trắng", MaMau="SA344",Url="https://media.canifa.com/attribute/swatch/images/sa344.webp"},
                                                            new MauSac{Ten="Xám trắng", MaMau="SA799",Url="https://media.canifa.com/attribute/swatch/images/sa799.webp"},
                                                            new MauSac{Ten="Đen", MaMau="SK327",Url="https://media.canifa.com/attribute/swatch/images/sk327.webp"},
                                                            new MauSac{Ten="Xanh đen", MaMau="SL146",Url="https://media.canifa.com/attribute/swatch/images/sl146.webp"},
                                                            new MauSac{Ten="Xanh da trời nhạt", MaMau="SL168",Url="https://media.canifa.com/attribute/swatch/images/sl168.webp"},
                                                            new MauSac{Ten="Đen trắng xen kẽ", MaMau="PW172",Url="https://media.canifa.com/attribute/swatch/images/pw172.webp"},
                                                            new MauSac{Ten="Xanh da trời", MaMau="SL234",Url="https://media.canifa.com/attribute/swatch/images/sl234.webp"},
                                                            new MauSac{Ten="Xanh biển", MaMau="SB150",Url="https://media.canifa.com/attribute/swatch/images/sb150.webp"},
                                                            new MauSac{Ten="Xanh da trời nhạt", MaMau="SB194",Url="https://media.canifa.com/attribute/swatch/images/sb194.webp"},
                                                         });
                var listDmCha = await _context.DanhMucs.Where(x => x.Id_DanhMuc_Cha == null).ToListAsync();

                if (listDmCha != null)
                {
                    foreach (var dmCha in listDmCha)
                    {

                        switch (dmCha.Ten)
                        {
                            case "Áo":
                                var lstDmCon = await _context.DanhMucs.Where(x => x.Id_DanhMuc_Cha == dmCha.Id).ToListAsync();
                                foreach (var dmCon in lstDmCon)
                                {
                                    var data = new List<SanPham>();
                                    var listSpTraVe = new List<SanPham>();
                                    switch (dmCon.Ten)
                                    {
                                        case "Áo chống nắng":
                                            data = new List<SanPham>
                                {
                                    new SanPham{
                                    Ten="Áo khoác chống nắng nam",
                                    HuongDanSuDung="Giặt máy ở chế độ nhẹ, nhiệt độ thường.\r\nKhông sử dụng chất tẩy.\r\nPhơi trong bóng mát.\r\nSấy khô ở nhiệt độ thấp.\r\nLà ở nhiệt độ thấp 110 độ C.\r\nGiặt với sản phẩm cùng màu.\r\nKhông là lên chi tiết trang trí.",
                                    MoTa="Áo khoác chống nắng chất liệu polyester, có mũ, kéo khoá, túi 2 bên.\r\nChất liệu co dãn thoải mái khi mặc.",
                                    ChatLieu="94% polyester 6% spandex.",
                                    Id_DanhMuc = dmCon.Id,
                                    DanhMuc = dmCon,
                                    DonGia =549000,
                                    TrangThai = true,

                                    Id_DoiTuong = doiTuongSp.FirstOrDefault().Id,
                                    },
                                    new SanPham{
                                    Ten="Áo khoác chống nắng nam",
                                    HuongDanSuDung="Giặt máy ở chế độ nhẹ, nhiệt độ thường.\r\nKhông sử dụng chất tẩy.\r\nPhơi trong bóng mát.\r\nSấy khô ở nhiệt độ thấp.\r\nLà ở nhiệt độ thấp 110 độ C.\r\nGiặt với sản phẩm cùng màu.\r\nKhông là lên chi tiết trang trí.",
                                    MoTa="Áo khoác chống nắng chất liệu polyester co giãn, có mũ kéo khoá.\r\nTính năng: AntiUV.",
                                    ChatLieu="94% polyester 6% spandex",
                                    Id_DanhMuc = dmCon.Id,
                                    DanhMuc = dmCon,
                                    DonGia =314300,
                                    TrangThai = true,

                                    Id_DoiTuong = doiTuongSp.FirstOrDefault().Id,
                                    },
                                };
                                            listSpTraVe = await AddRangeToDatabase(data);
                                            // Thêm hình ảnh
                                            foreach (var sp in listSpTraVe)
                                            {
                                                switch (sp.DonGia)
                                                {
                                                    case 549000:
                                                        await AddRangeToDatabase(new List<HinhAnh>()
                                                        {
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot24s001-sa422-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot24s001-sa422-1-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot24s001-sa422-3.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot24s001-sa422-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot24s001-sa422-xl-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot24s001-sb001-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot24s001-sb001-1-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot24s001-sb001-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot24s001-sb001-xl-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot24s001-sk010-1-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot24s001-sk010-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot24s001-sk010-4.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot24s001-sk010-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot24s001-sk010-xl-2.webp" },
                                                        });
                                                        break;
                                                    case 314300:
                                                        await AddRangeToDatabase(new List<HinhAnh>()
                                                        {
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot23s001-sk010-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot23s001-sk010-2-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot23s001-sk010-3.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot23s001-sk010-4.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot23s001-sk010-5.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/o/8ot23s001-sk010-xl-1.webp" },
                                                        });
                                                        break;
                                                }
                                            }

                                            // Thêm sản phẩm chi tiết
                                            foreach (var sp in listSpTraVe)
                                            {
                                                var lstMsLoc = new List<MauSac>();
                                                switch (sp.DonGia)
                                                {
                                                    case 549000:
                                                        lstMsLoc = lstMS.Where(x => x.MaMau == "SA422" || x.MaMau == "SB001" || x.MaMau == "SK010").ToList();

                                                        AddSanPhamChiTiet(sp.Id, lstMsLoc, lstKichThuoc);
                                                        break;
                                                    case 314300:
                                                        lstMsLoc = lstMS.Where(x => x.MaMau == "SK010").ToList();

                                                        AddSanPhamChiTiet(sp.Id, lstMsLoc, lstKichThuoc);
                                                        break;
                                                }
                                            }
                                            break;
                                        case "Áo Polo":
                                            data = new List<SanPham>
                                {
                                    new SanPham{
                                    Ten="Áo polo active nam",
                                    HuongDanSuDung="Giặt máy ở chế độ nhẹ, nhiệt độ thường.\r\nKhông sử dụng chất tẩy.\r\nPhơi trong bóng mát.\r\nSấy khô ở nhiệt độ thấp.\r\nLà ở nhiệt độ thấp 110 độ C.\r\nGiặt với sản phẩm cùng màu.\r\nKhông là lên chi tiết trang trí.",
                                    MoTa="Áo polo active nam với nhiều tính năng và công nghê như: AntiUV, Wicking, Drytech mang lại hiệu quả chống nắng và thấm hút mồ hôi vượt trội. Kết hợp cùng chất liệu mềm mát, là lựa chọn tối ưu cho các hoạt động ngoài trời và hoạt động thể thao.",
                                    ChatLieu="87,3% Nylon 12,7% spandex.",
                                    Id_DanhMuc = dmCon.Id,
                                    DanhMuc = dmCon,
                                    DonGia =499000 ,
                                    TrangThai = true,

                                    Id_DoiTuong = doiTuongSp.FirstOrDefault().Id,
                                    },
                                   new SanPham{
                                    Ten="Áo polo nam",
                                    HuongDanSuDung="Giặt máy ở chế độ nhẹ, nhiệt độ thường.\r\nKhông sử dụng hóa chất tẩy có chứa Clo.\r\nPhơi trong bóng mát.\r\nSấy khô ở nhiệt độ thấp.\r\nLà ở nhiệt độ thấp 110 độ C.\r\nGiặt với sản phẩm cùng màu.\r\nKhông là lên chi tiết trang trí.",
                                    MoTa="Áo t-shirt basic có cổ bẻ, nẹp cài cúc, bề mặt vải có họa tiết in tạo điểm nhấn mới cho sản phẩm.",
                                    ChatLieu="70% cotton 25% polyester 5% spandex.",
                                    Id_DanhMuc = dmCon.Id,
                                    DanhMuc = dmCon,
                                    DonGia =499000 ,
                                    TrangThai = true,

                                    Id_DoiTuong = doiTuongSp.FirstOrDefault().Id,
                                    },
                                };
                                            listSpTraVe = await AddRangeToDatabase(data);
                                            // them hinh anh
                                            foreach (var sp in listSpTraVe)
                                            {
                                                switch (sp.Ten)
                                                {
                                                    case "Áo polo active nam":
                                                        await AddRangeToDatabase(new List<HinhAnh>()
                                                        {
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s004-sb175-3.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s004-sb175-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s004-sb175-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s004-sb175-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s004-sb175-xl-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s004-sb175-5.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s004-sb175-6.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s004-sa422-1a.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s004-sa422-thumba.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s004-sa422-xl-1-thumb-ua.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s004-sa422-xl-2a.webp" },
                                                        });
                                                        break;
                                                    case "Áo polo nam":
                                                        await AddRangeToDatabase(new List<HinhAnh>()
                                                        {
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s007-sw001-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s007-sw001-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s007-sw001-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s007-sw001-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s007-sw001-xl-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s007-sk010-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s007-sk010-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s007-sk010-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8tp24s007-sk010-xl-2.webp" },
                                                        });
                                                        break;
                                                }
                                            }

                                            // Thêm sản phẩm chi tiết
                                            foreach (var sp in listSpTraVe)
                                            {
                                                var lstMsLoc = new List<MauSac>();
                                                var lstKichThuocLoc = new List<KichThuoc>();
                                                switch (sp.Ten)
                                                {
                                                    case "Áo polo active nam":
                                                        lstMsLoc = lstMS.Where(x => x.MaMau == "SA422" || x.MaMau == "SB175").ToList();

                                                        lstKichThuocLoc = lstKichThuoc.Where(x => !(x.Ten.Equals("XXL"))).ToList();
                                                        AddSanPhamChiTiet(sp.Id, lstMsLoc, lstKichThuocLoc);
                                                        break;
                                                    case "Áo polo nam":
                                                        lstMsLoc = lstMS.Where(x => x.MaMau == "SW001" || x.MaMau.Equals("SK010")).ToList();

                                                        lstKichThuocLoc = lstKichThuoc;
                                                        AddSanPhamChiTiet(sp.Id, lstMsLoc, lstKichThuocLoc);
                                                        break;
                                                }
                                            }
                                            break;
                                        case "Áo len giữ nhiệt":
                                            data = new List<SanPham>
                                {
                                    new SanPham{
                                    Ten="Áo giữ nhiệt nam cổ 3 phân",
                                    HuongDanSuDung="Giặt máy ở chế độ nhẹ, nhiệt độ thường.\r\nKhông sử dụng chất tẩy.\r\nPhơi trong bóng mát.\r\nSấy khô ở nhiệt độ thấp.\r\nLà ở nhiệt độ thấp 110 độ C.\r\nGiặt với sản phẩm cùng màu.\r\nKhông là lên chi tiết trang trí.",
                                        MoTa="Áo giữ nhiệt nam được làm từ nguyên liệu heattech. Nguyên liệu được nghiên cứu kĩ về thành phần sợi, tỉ lệ các thành phần để mang lại khả năng tăng sinh nhiệt mạnh khi mặc.\r\nLưu ý: Hiệu quả giữa ấm tối đa khi mặc lớp trong cùng và kết hợp với len, nỉ, áo khoác Canifa.\r\nTính năng giữ nhiệt (heatplus).\r\nCấu trúc sợi được thiết kế đặc biệt với nhiều khoang rỗng trên thân sợi do đó nhiệt sinh ra do cọ xát, nhiệt từ cơ thể người mặc sẽ được giữa trong các khoang rỗng này giúp cơ thể ấm dần và không bị mất nhiệt..\r\nSự kết hợp nhiều thành phần tạo nên sợi có đặc trưng trơn bóng, mượt mà, mềm mịn .Thành phần spandex giúp sản phẩm có độ co giãn, ôm sát cơ thể giúp tăng khả năng sinh nhiệt và giữ nhiệt. Thành phần Acrylic giúp sản phẩm giữ được form dáng và có độ bền cao.\r\nSợi có chi số nhỏ, mật độ dệt cao giúp sản phẩm mỏng nhẹ mịn màng tạo cảm giác thoải mái khi mặc.",
                                        ChatLieu="56% acrylic 38% viscose 6% spandex",
                                    Id_DanhMuc = dmCon.Id,
                                    DanhMuc = dmCon,
                                    DonGia =320000 ,
                                    TrangThai = true,

                                    Id_DoiTuong = doiTuongSp.FirstOrDefault().Id,
                                    },
                                   new SanPham{
                                    Ten="Áo giữ nhiệt nam cổ tròn",
                                    HuongDanSuDung="Giặt máy ở chế độ nhẹ, nhiệt độ thường.\r\nKhông sử dụng chất tẩy.\r\nPhơi trong bóng mát.\r\nSấy khô ở nhiệt độ thấp.\r\nLà ở nhiệt độ thấp 110 độ C.\r\nGiặt với sản phẩm cùng màu.\r\nKhông là lên chi tiết trang trí.",
                                    MoTa="Áo giữ nhiệt nam được làm từ nguyên liệu heattech. Nguyên liệu được nghiên cứu kĩ về thành phần sợi, tỉ lệ thành phần để mang lại khả năng tăng sinh nhiệt mạnh khi mặc.\r\nHiệu quả giữa ấm tối đa khi mặc lớp trong cùng và kết hợp với áo len, nỉ, áo khoác Canifa.\r\nCấu trúc sợi được thiết kế đặc biệt với nhiều khoang rỗng trên thân sợi do đó nhiệt sinh ra do cọ xát, nhiệt từ cơ thể người mặc sẽ được giữa trong các khoang rỗng này giúp cơ thể ấm dần và không bị mất nhiệt.\r\nSự kết hợp nhiều thành phần tạo nên sợi có đặc trưng trơn bóng, mượt mà, mềm mịn.\r\nThành phần spandex giúp sản phẩm có độ co giãn, ôm sát cơ thể giúp tăng khả năng sinh nhiệt và giữ nhiệt. Thành phần Acrylic giúp sản phẩm giữ được form dáng và có độ bền cao.\r\nSợi có chi số nhỏ, mật độ dệt cao giúp sản phẩm mỏng nhẹ mịn màng tạo cảm giác thoải mái khi mặc.",
                                    ChatLieu="56% acrylic 38% viscose 6% spandex",
                                    Id_DanhMuc = dmCon.Id,
                                    DanhMuc = dmCon,
                                    DonGia =299000 ,
                                    TrangThai = true,

                                    Id_DoiTuong = doiTuongSp.FirstOrDefault().Id,
                                    },
                                };
                                            listSpTraVe = await AddRangeToDatabase(data);
                                            // Thêm hình ảnh
                                            foreach (var sp in listSpTraVe)
                                            {
                                                switch (sp.Ten)
                                                {
                                                    case "Áo giữ nhiệt nam cổ 3 phân":
                                                        await AddRangeToDatabase(new List<HinhAnh>()
                                                        {
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sb001-1-n.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sb001-1-thumb-n.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sb001-3-n.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sb001-m-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sb001-m-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sk010-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sk010-1-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sk010-3.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sk010-m-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sk010-m-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sa180-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sa180-1-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sa180-xl-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sa206-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sa206-1-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sa206-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w001-sa206-xl-2.webp" },
                                                        });
                                                        break;
                                                    case "Áo giữ nhiệt nam cổ tròn":
                                                        await AddRangeToDatabase(new List<HinhAnh>()
                                                        {
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sk010-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sk010-1-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sk010-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sk010-xl-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sa206-1-n.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sa206-1-thumb-n.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sa206-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sa206-xl-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sa344-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sa344-1-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sa344-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sa344-xl-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sb001-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sb001-1-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sb001-3.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sb001-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/i/8it23w002-sb001-xl-2.webp" },
                                                        });
                                                        break;
                                                }
                                            }

                                            // Thêm sản phẩm chi tiết
                                            foreach (var sp in listSpTraVe)
                                            {
                                                var lstMsLoc = new List<MauSac>();
                                                var lstKichThuocLoc = new List<KichThuoc>();
                                                switch (sp.Ten)
                                                {
                                                    case "Áo giữ nhiệt nam cổ 3 phân":
                                                        lstMsLoc = lstMS.Where(x => x.MaMau == "SB001" || x.MaMau == "SA180" || x.MaMau == "SA206" || x.MaMau == "SB060" || x.MaMau == "SK010").ToList();

                                                        lstKichThuocLoc = lstKichThuoc.Where(x => !(x.Ten.Equals("XXL"))).ToList();
                                                        AddSanPhamChiTiet(sp.Id, lstMsLoc, lstKichThuocLoc);
                                                        break;
                                                    case "Áo giữ nhiệt nam cổ tròn":
                                                        lstMsLoc = lstMS.Where(x => x.MaMau == "SW001" || x.MaMau.Equals("SA344") || x.MaMau == "SA206" || x.MaMau == "SK010").ToList();

                                                        lstKichThuocLoc = lstKichThuoc;
                                                        AddSanPhamChiTiet(sp.Id, lstMsLoc, lstKichThuocLoc);
                                                        break;
                                                }
                                            }
                                            break;
                                        case "Áo len":
                                            data = new List<SanPham>
                                {
                                    new SanPham{
                                    Ten="Áo len nam cổ tròn dáng suông",
                                    HuongDanSuDung="Giặt máy ở chế độ nhẹ, nhiệt độ thường.\r\nKhông sử dụng hóa chất tẩy có chứa clo.\r\nPhơi vắt ngang, trong bóng mát.\r\nKhông sử dụng máy sấy.\r\nLà ở nhiệt độ trung bình 150 độ C.\r\nGiặt mặt trái sản phẩm.\r\nGiặt với sản phẩm cùng màu.\r\nLàm ẩm sản phẩm khi là.",
                                        MoTa="Áo len nam dài tay, cổ tròn, dáng regular không quá ôm cũng không quá rộng tạo cho người mặc thấy thoải mái, vừa vặn. Phần thân áo dệt bằng chất liệu len dày dặn, ấm áp nhưng vẫn nhẹ nhàng, thoải mái khi mặc.\r\nChất liệu dầy dặn, mềm mại, nhẹ nhưng ấm tạo cảm giác thoải mái cho người mặc trong thời tiết thu đông. Màu sắc rất đa dạng, bắt mắt.",
                                        ChatLieu="50% acrylic 50% polyester",
                                    Id_DanhMuc = dmCon.Id,
                                    DanhMuc = dmCon,
                                    DonGia =499000 ,
                                    TrangThai = true,
                                    DoiTuong = doiTuongSp.FirstOrDefault(),
                                    Id_DoiTuong = doiTuongSp.FirstOrDefault().Id,
                                    },
                                   new SanPham{
                                    Ten="Áo len nam dệt kẻ",
                                    HuongDanSuDung="Giặt máy ở chế độ nhẹ, nhiệt độ thường.\r\nKhông sử dụng hóa chất tẩy có chứa clo.\r\nPhơi vắt ngang, trong bóng mát.\r\nKhông sử dụng máy sấy.\r\nLà ở nhiệt độ trung bình 150 độ C.\r\nGiặt mặt trái sản phẩm.\r\nGiặt với sản phẩm cùng màu.\r\nLàm ẩm sản phẩm khi là.",
                                       MoTa="Áo len nam dài tay, dệt kẻ ngang có hình thêu xù ở ngực áo. Áo dễ dàng phối với nhiều kiểu trang phục và phong cách khác nhau. Có thể mix cùng áo sơ mi, áo polo hoặc áo thun để tạo ra nhiều diện mạo và phong cách thời trang đa dạng. Sản phẩm áo len nam kẻ ngang phù hợp với nhiều độ tuổi và phong cách thời trang. Áo len sẽ mang đến cho bạn vẻ ngoài ấm áp, trẻ trung và phong cách.\r\nChất liệu dầy dặn, mềm mại, nhẹ nhưng ấm tạo cảm giác thoải mái cho người mặc trong thời tiết thu đông. Màu sắc rất đa dạng, bắt mắt.",
                                       ChatLieu="50% acrylic 50% polyester",
                                    Id_DanhMuc = dmCon.Id,
                                    DanhMuc = dmCon,
                                    DonGia =599000 ,
                                    TrangThai = true,

                                    Id_DoiTuong = doiTuongSp.FirstOrDefault().Id,
                                    },
                                };
                                            listSpTraVe = await AddRangeToDatabase(data);
                                            // Thêm hình ảnh
                                            foreach (var sp in listSpTraVe)
                                            {
                                                switch (sp.Ten)
                                                {
                                                    case "Áo giữ nhiệt nam cổ tròn":
                                                        await AddRangeToDatabase(new List<HinhAnh>()
                                                        {
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sa799-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sa799-1-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sa799-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sa799-xl-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sk327-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sk327-1-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sk327-3.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sk327-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sk327-xl-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sl146-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sl146-1-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sl146-3.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sl146-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sl146-xl-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sl168-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sl168-2-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sl168-3.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sl168-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w022-sl168-xl-2.webp" },
                                                        });
                                                        break;
                                                    case "Áo len nam dệt kẻ":
                                                        await AddRangeToDatabase(new List<HinhAnh>()
                                                        {
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w019-pw172-1.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w019-pw172-1-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8te23w019-pw172-3.webp" }
                                                        });
                                                        break;
                                                }
                                            }

                                            // Thêm sản phẩm chi tiết
                                            foreach (var sp in listSpTraVe)
                                            {
                                                var lstMsLoc = new List<MauSac>();
                                                var lstKichThuocLoc = new List<KichThuoc>();
                                                switch (sp.Ten)
                                                {
                                                    case "Áo len nam cổ tròn dáng suông":
                                                        lstMsLoc = lstMS.Where(x => x.MaMau == "SA799" || x.MaMau == "SK327" || x.MaMau == "SL146" || x.MaMau == "SL168").ToList();

                                                        lstKichThuocLoc = lstKichThuoc.Where(x => !(x.Ten.Equals("XXL"))).ToList();
                                                        AddSanPhamChiTiet(sp.Id, lstMsLoc, lstKichThuocLoc);
                                                        break;
                                                    case "Áo len nam dệt kẻ":
                                                        lstMsLoc = lstMS.Where(x => x.MaMau == "PW172").ToList();

                                                        lstKichThuocLoc = lstKichThuoc.Where(x => !(x.Ten.Equals("XXL"))).ToList();
                                                        AddSanPhamChiTiet(sp.Id, lstMsLoc, lstKichThuocLoc);
                                                        break;
                                                }
                                            }
                                            break;
                                        case "Áo Sơ mi":
                                            data = new List<SanPham>
                                {
                                    new SanPham{
                                    Ten="Áo sơ mi nam cộc tay",
                                    HuongDanSuDung="Giặt máy ở chế độ nhẹ, nhiệt độ thường.\r\nKhông sử dụng chất tẩy.\r\nPhơi trong bóng mát.\r\nSấy khô ở nhiệt độ thấp.\r\nLà ở nhiệt độ thấp 110 độ C.\r\nGiặt với sản phẩm cùng màu.\r\nKhông là lên chi tiết trang trí.",
                                        MoTa="Áo sơ mi nam cotton, phom basic. Thích hợp trong nhiều hoàn cảnh.\r\nNguyên liệu cotton thoáng mát, mỏng nhẹ, tạo cảm giác dễ chịu khi mặc.",
                                        ChatLieu="51% cotton 49% polyester.",
                                    Id_DanhMuc = dmCon.Id,
                                    DanhMuc = dmCon,
                                    DonGia =499000 ,
                                    TrangThai = true,
                                    DoiTuong = doiTuongSp.FirstOrDefault(),
                                    Id_DoiTuong = doiTuongSp.FirstOrDefault().Id,
                                    },
                                   new SanPham{
                                    Ten="Áo sơ mi nam cotton dài tay",
                                    HuongDanSuDung="Giặt máy ở nhiệt độ thường.\r\nKhông sử dụng hóa chất tẩy có chứa Clo.\r\nPhơi trong bóng mát.\r\nSấy khô ở nhiệt độ trung bình.\r\nLà ở nhiệt độ trung bình 150 độ C.\r\nGiặt với sản phẩm cùng màu.\r\nKhông là lên chi tiết trang trí.",
                                       MoTa="Áo sơ mi nam dáng regular. Chất liệu 100% cotton mềm, mát.\r\nPhù hợp mặc đi làm, kết hợp cùng quần vải, quần khaki.",
                                       ChatLieu="100% cotton.",
                                    Id_DanhMuc = dmCon.Id,
                                    DanhMuc = dmCon,
                                    DonGia =449000 ,
                                    TrangThai = true,
                                    DoiTuong = doiTuongSp.FirstOrDefault(),
                                    Id_DoiTuong = doiTuongSp.FirstOrDefault().Id,
                                    },
                                };
                                            listSpTraVe = await AddRangeToDatabase(data);
                                            // Thêm hình ảnh
                                            foreach (var sp in listSpTraVe)
                                            {
                                                switch (sp.Ten)
                                                {
                                                    case "Áo sơ mi nam cộc tay":
                                                        await AddRangeToDatabase(new List<HinhAnh>()
                                                        {
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24s002-sw001-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24s002-sw001-xl-3.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24s002-sl234-m-1-u.webp " },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24s002-sl234-m-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24s002-sl234-m-3.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24s002-sl234-m-5.webp" },
                                                        });
                                                        break;
                                                    case "Áo sơ mi nam cotton dài tay":
                                                        await AddRangeToDatabase(new List<HinhAnh>()
                                                        {
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sb194-3.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sb194-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sb194-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sb194-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sb194-xl-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sb194-xl-3.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sw001-3.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sw001-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sw001-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sw001-4.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sw001-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sw001-xl-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sw001-xl-3.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sb150-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sb150-thumb.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sb150-3.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sb150-xl-1-u.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sb150-xl-2.webp" },
                                                            new HinhAnh { Id_SanPham = sp.Id, Url = "https://canifa.com/img/1517/2000/resize/8/t/8th24a004-sb150-xl-3.webp" },
                                                        });
                                                        break;
                                                }
                                            }

                                            // Thêm sản phẩm chi tiết
                                            foreach (var sp in listSpTraVe)
                                            {
                                                var lstMsLoc = new List<MauSac>();
                                                var lstKichThuocLoc = new List<KichThuoc>();
                                                switch (sp.Ten)
                                                {
                                                    case "Áo sơ mi nam cộc tay":
                                                        lstMsLoc = lstMS.Where(x => x.MaMau == "SL234" || x.MaMau == "SW001").ToList();

                                                        lstKichThuocLoc = lstKichThuoc.Where(x => !(x.Ten.Equals("XXL"))).ToList();
                                                        AddSanPhamChiTiet(sp.Id, lstMsLoc, lstKichThuocLoc);
                                                        break;
                                                    case "Áo sơ mi nam cotton dài tay":
                                                        lstMsLoc = lstMS.Where(x => x.MaMau == "SW001" || x.MaMau == "SB194" || x.MaMau == "SB150").ToList();

                                                        lstKichThuocLoc = lstKichThuoc.Where(x => !(x.Ten.Equals("XXL"))).ToList();
                                                        AddSanPhamChiTiet(sp.Id, lstMsLoc, lstKichThuocLoc);
                                                        break;
                                                }
                                            }
                                            break;

                                    }
                                }
                                break;
                            case "Áo khoác":

                            case "Quần":

                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


            return Ok("Thành công tạo mới token");
        }

        private async void AddSanPhamChiTiet(int idSp, List<MauSac> lstMauSacs, List<KichThuoc> lstKichThuoc)
        {
            var lstSpCT = new List<SanPhamChiTiet>();
            foreach (var ms in lstMauSacs)
            {
                foreach (var kt in lstKichThuoc)
                {
                    lstSpCT.Add(new()
                    {
                        Id_SanPham = idSp,
                        Id_MauSac = ms.Id,
                        Id_KichThuoc = kt.Id,
                        SoLuong = 100,
                    });
                    //await AddObjToDatabase<SanPhamChiTiet>(new()
                    //{
                    //    Id_SanPham = idSp,
                    //    Id_MauSac = ms.Id,
                    //    Id_KichThuoc = kt.Id,
                    //    SoLuong = 100,
                    //    MauSac = ms,
                    //    KichThuoc = kt
                    //});
                }
            }
            await AddRangeToDatabase(lstSpCT);
        }
        private async Task<T> AddObjToDatabase<T>(T model)
        {
            if (model == null)
            {
                return default(T);
            }
            var tb = await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return (T)tb.Entity;
        }
        private async Task<List<T>> AddRangeToDatabase<T>(List<T> model)
        {
            if (model == null)
            {
                return default(List<T>);
            }
            //_context.DanhMucs.Ad
            foreach (var item in model)
            {
                await _context.AddAsync(item);

            }
            _context.SaveChanges();
            return model;
        }
    }
}
