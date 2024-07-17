using Base.EntitiesBase.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Constants
{
    public static class Messages 
        //mesajları newlememek için static yaptık. static yaptığımız da new'leme işlemi yapmıyoruz.

    {// bu içeride kullanılacak bir propert olsa idi küçük harf ile başlardık.
        //ancak public olduğu için büyük harf ile başladık. -- PascalCase
        
        public static string ProductAdded = " Ürün eklenmiştir.";
        public static string ProductNameInvalid = " Ürün ismi geçersizdir.";
        public static string ProductCategoryCountError ="Bu kategoride en fazla 10 ürün yerleştirilebilirsiniz.";
        public static string HaveSameProductName="Aynı isimde ürün adı olamaz";
        public static string CategoryCountLimited="Toplam kategori sayısı 15'i geçemez. ";
        public static string? AuthorizationDenied = "Yetkin yok";
        public static string UserRegistered = "kullanıcı kayıt edildi";
        public static string UserNotFound = "kullanıcı bulunamadı";
        public static string PasswordError = "parola hatası";
        public static string SuccessfulLogin = "Giriş başarılı";
        public static string UserAlreadyExists = "kullanıcı zaten mevcut";
        public static string AccessTokenCreated = "token oluşturuldu";
    }
}
// bu sınıf Business'e yazılır. 
// core'a yazılacaklar evrensel herkes de geçrli olan özellikler yazılır. 
