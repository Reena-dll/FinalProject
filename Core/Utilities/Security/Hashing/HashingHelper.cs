using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        // Bu metotta gelen şifreyi Hashleyip saltlıyoruz.
        public static void CreatePasswordHash // Biz password vereceğiz ve dışarıya 2 değer yollayacağız. out passwordHas, out passwordSalt
            (string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) // HMACSHA512 algoritmasından yararlanarak passwordSalt ve passwordHash oluşturacağız. 
            {
                // Key = Her kullanıcı için bir key oluşturur. Salt değerini de her kullanıcı için veritabanında tutacağız.
                passwordSalt = hmac.Key;// Tuz değeri olarak kullandığımız algoritmanın key değerini kullanıyoruz.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                // Salt standartmak olmak zorunda çünkü şifreyi çözerken o salta ihtiyacımız olacak.
            }
        }

        // PasswordHash'i doğrulayacağız. Daha öncesinde oluşturulan Hash ile şu an sisteme giriş yapmayı denerken ki
        // oluşturulan Hash'i karşılaştıracağız. Eğer hash'ler uyuşuyor ise şifre doğrudur :p 
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) // Daha önce oluşturduğumuz Saltı parametre içerisine veriyoruz.
            {
                // ComoytedHash = Hesaplanan Hash.
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {// Bu iki hash değeri byte olduğu için for döngüsüyle indekslerini karşılaştırıyoruz.
                    // İndeksler bir birine uyuyorsa TRUE.
                    if (computedHash[i]!=passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
