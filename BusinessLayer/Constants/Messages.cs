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

        
    }
}
// bu sınıf Business'e yazılır. 
// core'a yazılacaklar evrensel herkes de geçrli olan özellikler yazılır. 
