using System.Security.Cryptography;
using System.Text;

namespace Identity.Application.Helpers;

public class HashPasswordHelper
{
    private const int KeySize = 64;
    private const int Iterators = 350000;
    private static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA512;

    public static string HashPassword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(KeySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, Iterators, HashAlgorithmName, KeySize);

        return Convert.ToHexString(hash);
    }
}