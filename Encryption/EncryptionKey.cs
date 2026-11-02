using OtpNet;
using System.Security.Cryptography;
using CommunityToolkit.Maui.Storage;
using System.Text;

namespace LAB2.Encryption;

public class EncryptionKey
{
    private byte[] Key {get;}

    public EncryptionKey(int size)
    {
        if(size <=3)
            throw new ArgumentException("Размер ключа не должен быть менее 3 символов!");
        Key = RandomNumberGenerator.GetBytes(size);
    }

    //Копия массива
    async public Task EncryptArray()
    {
        string keyText = string.Join(" ", Key);
        using var stream = new MemoryStream(
            Encoding.UTF8.GetBytes(keyText)
        );

        var res = await FileSaver.Default.SaveAsync(
            "EncryptionKey.txt",
            stream,
            CancellationToken.None
        );

        if (!res.IsSuccessful)
            throw new Exception("Не удалось сохранить ключ!", res.Exception);
    }
}