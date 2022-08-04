using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün İsmi Geçersiz";
        public static string MaintenanceTime = "Sistem Bakımda";
        public static string ProductListed = "Ürünler Listelendi";
        public static string ProductCountOfCategoryError = "Bir Kategoride En Fazla 10 Ürün Olabilir";
        public static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var.";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor.";
        public static string AuthorizationDenied= "Yetkiniz Yok";
        internal static string UserRegistered="Kayıt Oldu";
        internal static string UserNotFound="Kullanıcı Bulunamadı";
        internal static string UserAlreadyExists = "Kullanıcı Mevcut";
        internal static string SuccessfulLogin = "Başarılı Giriş";
        internal static string PasswordError = "Parola Hatası";
        internal static string AccessTokenCreated = "Giriş Kimliği Oluşturuldu";
    }
}
