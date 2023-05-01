using System.Security.Cryptography;
using System.Text;
using WeCare.Application.Common.Interfaces;

namespace WeCare.Infrastructure.Services;
public class PasswordGenerator : IPasswordGenerator
{

    public string GeneratePassword()
    {
        byte[] rgb = new byte[8];
        RNGCryptoServiceProvider rngCrypt = new RNGCryptoServiceProvider();
        rngCrypt.GetBytes(rgb);
        return Convert.ToBase64String(rgb);
    }
   
}
