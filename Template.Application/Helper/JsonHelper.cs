using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Template.Application.Helper;


public static class JsonHelper
{
    // Global options accessible anywhere
    public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        WriteIndented = true // optional, makes JSON readable
    };

    // Optional: convenience methods
    public static string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value, Options);
    }

    public static T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, Options);
    }
}

