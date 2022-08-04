using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {// Microsoft.Identity.Model.Tokens nuget paketini yüklememiz gerekiyor SecurityKey class'ına ulaşabilmek için.
        public static SecurityKey CreateSecurityKey(string securityKey)
        {// Daha önce appsettings dosyasında oluşturduğumuz SecurityKey'i Byte tipine çeviren motor.
            // Yani bizim oluşturduğumuz string ifadeyi SecurityKey tipine cast ediyoruz.
            // Client'lerin anlayacağı dile çeviriyoruz.
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            // Simetrik bir securityKey oluşturduk. Simetrik ve asimetrik olarak 2'e ayrılıyor. Araştırma konusu
        }
    }
}
