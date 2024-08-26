namespace ClientLibary.Helpers
{
    public static class LocalStorageFieldService
    {
        private static string Token { get; set; }
        // key lưu thông tin

        // Lấy thông tin dựa trên key
        public static string GetToken() => Token;
        // Thiết lập thông tin vào local dựa vào key : value
        public static void SetToken(string item) => Token = item;
        // Xóa key khỏi local store
        public static void RemoveToken() => Token = null;
    }
}
