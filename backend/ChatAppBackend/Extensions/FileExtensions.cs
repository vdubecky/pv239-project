namespace ChatAppBackend.Extensions;

public static class FileExtensions
{
    public static string MapExtensionToContentType(this string extension)
    {
        return extension switch
        {
            ".jpg" or ".jpeg" or ".jpe" => "image/jpeg",
            ".png" => "image/png",
            _ => "application/octet-stream"
        };
    }

    public static async Task SaveByteArrayToFile(this byte[] byteArray, string fileName)
    {
        await using var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        await fs.WriteAsync(byteArray);
    }
    
    public static byte[] ReadByteArrayFromFile(this string fileName)
    {
        try
        {
            return File.ReadAllBytes(fileName);
        } catch(Exception ex)
        {
            return new byte[] { 0 }; 
        }
       
    }

    public static async Task<byte[]> ReadByteArrayFromFileAsync(this string fileName)
    {
        try
        {
            return await File.ReadAllBytesAsync(fileName);
        } catch(Exception ex)
        {
            return new byte[] { 0 };
        }
       
    }
}
