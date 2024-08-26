using System.Text.Json;

namespace ClientLibary.Helpers
{
    public static class Serializations
    {
        // Chuyên đổi 1 obj thành chuỗi string
        public static string SerializeObj<T>(T obj) => JsonSerializer.Serialize(obj);

        // Chuyển đổi 1 string thành đối tượng mong muốn
        public static T DeserializeJsonString<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString);

        // Chuyển đổi string thành 1 List đối tượng
        public static IList<T> DeserializeJsonStringList<T>(string jsonString) => JsonSerializer.Deserialize<IList<T>>(jsonString);

    }
}
