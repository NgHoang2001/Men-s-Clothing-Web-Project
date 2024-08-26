using BaseLibary.Responses;
using ServerLibary.Model.Params;

namespace ServerLibary.Respositories.Contracts
{
    public interface IThanhToanServices
    {
        Task<GeneralRespone> ThanhToanGioHang(ThanhToanParam param);
    }
}
