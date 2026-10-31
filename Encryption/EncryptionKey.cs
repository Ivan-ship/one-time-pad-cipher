using OtpNet;
using System.Security.Cryptography;

namespace LAB2.Encryption;

public class EncryptionKey
{
    private byte[] Key {get;}

    public EncryptionKey(int size)
    {
        if(size <= 0)
            throw new ArgumentException("Невалидный ключ!");
        
        Key = RandomNumberGenerator.GetBytes(size);
    }

}