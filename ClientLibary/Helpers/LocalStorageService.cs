using Blazored.LocalStorage;

namespace ClientLibary.Helpers
{
    public class LocalStorageService
    {
        private ILocalStorageService localStorageService;

        public LocalStorageService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        // key lưu thông tin
        private const string StorageKey = "authentication-token";

        // Lấy thông tin dựa trên key
        public async Task<string> GetToken() => await localStorageService.GetItemAsStringAsync(StorageKey);
        // Thiết lập thông tin vào local dựa vào key : value
        public async Task SetToken(string item) => await localStorageService.SetItemAsStringAsync(StorageKey, item);
        // Xóa key khỏi local store
        public async Task RemoveToken() => await localStorageService.RemoveItemAsync(StorageKey);
    }
}
