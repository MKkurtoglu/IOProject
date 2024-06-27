using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data,string message) :base(data,true,message)
        {
            
        }
        public SuccessDataResult(T data) :base(data,true)
        {
            
        }
        public SuccessDataResult(string message):base(default,true,message) 
        {
            // buradaki default şudur. örn geriye dönecek değer int 'tir ancak biz geriye bir değer döndürmek istemedik ve yazmadık int'in defaultunu döner kendisi.
            //fazla kullanılmaz sadece alternatif olsun diye yazdık.
        }
        public SuccessDataResult() :base(default,true)
        {
            
        }
    }
}
