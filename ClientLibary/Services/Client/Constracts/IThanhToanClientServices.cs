using BaseLibary.Responses;
using Client.Models;

namespace ClientLibary.Services.Client.Constracts
{
    public interface IThanhToanClientServices
    {
        Task<GeneralRespone> ThanhToanGioHang(ThanhToanSubmit param);
    }
}
