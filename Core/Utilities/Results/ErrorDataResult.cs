using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {

        }
        public ErrorDataResult(T data) : base(data, false)
        {

        }
        public ErrorDataResult(string message) : base(default, false, message)
        {
            // buradaki default şudur. örn egeriye dönecek değer int 'tir ancak biz geriye bir değer döndürmek sitemedik ve yazmadık int'in defaultunu döner kendisi.
            //fazla kullanılmaz sadece alternatif olsun diye yazdık.
            // referans tip --> default değeri null
            // char tip --> default değeri \0
            // bool tip --> default değeri false
            //  sayı türleri tip --> default değeri 0

        }
        public ErrorDataResult() : base(default, false)
        {

        }
    }
}
