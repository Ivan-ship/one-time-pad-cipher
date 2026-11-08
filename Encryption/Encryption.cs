using System.Data;
using CommunityToolkit.Maui.Storage;
using NetworkExtension;

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

    //Чтение ключа шифрования
    public async Task<byte[]> GetKeyBytes(FileResult file)
    {
        using var stream = await file.OpenReadAsync();
        using var memoryStream = new MemoryStream();

        await stream.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    //Шифрования файла

    public async Task EncryptFile(byte[] fileBytes, byte[] keyBytes, string fileName)
    {
        //Проверка размера файла и ключа
        if(fileBytes.Length != keyBytes.Length)
        {
            throw new Exception("Разиер файла и ключа не совпадают!");
            throw new Exception("Размер файла и ключа не совпадают!");
        }

        //Массив байтов файла(подлежащего кодировке)
        byte[] enctyptedBytes = new byte[fileBytes.Length];
        for(int i = 0; i < fileBytes.Length; i++)
        {
            enctyptedBytes[i] = (byte)(
                (fileBytes[i] + keyBytes[i]) % 256
            );
        }

        using var stream = new MemoryStream(enctyptedBytes);
        var result = await FileSaver.Default.SaveAsync(fileName, stream, CancellationToken.None);

        if (!result.IsSuccessful)
        {
            throw new Exception("Не удалось сохранить файл!");
        }
    }
}
