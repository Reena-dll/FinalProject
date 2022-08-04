using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        // SecurityKey tipine dönüştürdüğümüz string ifadeyi burada kullanacağız.
        // Anahtar olarak SecurityKey'i kullan, güvenlik algoritmaları olarak ise HmacSha512'i kullan diyoruz.
        // Has'i oluştururken ve doğrularken HmacSha512 kullandık. Aynı şekilde bu sisteme ASP.Net'in de ihtiyacı var, WebAPI'nın ihtiyacı var.
        //JWT'i doğrulaması gerekiyor Web.Api.
        // Anahtarı ve algoritmayı yolluyoruz.
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
