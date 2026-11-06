namespace LAB2.Encryption;

public class FileEncryption
{
    //Чтение байтов файла
    public async Task<byte[]> GetFileBytes(FileResult file)
    {
        using var stream = await file.OpenReadAsync();
        using var memoryStream = new MemoryStream();

        await stream.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}
