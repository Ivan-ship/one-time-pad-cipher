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
        using var stream = new MemoryStream(Key);

        var res = await FileSaver.Default.SaveAsync(
            "EncryptionKey.bin",
            stream,
            CancellationToken.None
        );

        if (!res.IsSuccessful)
            throw new Exception("Не удалось сохранить ключ!", res.Exception);
    }
}