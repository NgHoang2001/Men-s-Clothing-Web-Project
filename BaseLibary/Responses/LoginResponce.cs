namespace BaseLibary.Responses
{
    // Sử dụng keyword record để định hình cấu trúc trả về của 1 login.
    // Nhắc lại tính chất record: có tính chất của class và struct.
    // Class: có các tính chất quan trọng của class.
    // Struct: so sánh theo giá trị.
    // Bản thân: tính bất biết giá trị khi lần đầu gán giá trị.
    // -> giống như class nhưng có cho phép so sanh theo giá trị và tính bất biến của giá trị.
    public record LoginResponce(bool Flag, string Message = null!, string Token = null!, string RefreshToken = null!);
}
