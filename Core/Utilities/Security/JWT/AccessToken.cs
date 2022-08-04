using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken
    {// AccessToken anlamsız karakterlerden oluşan bir anahtar değeridir.
        // Bitiş süresi ile birlikte tanımlıyoruz.

        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
