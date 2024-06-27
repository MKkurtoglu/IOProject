using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utilities.Results
{
    public class Result : IResult
    {
        // Temel void metotlarını bizim api'lerimizi kullanan ya da kullandığmız da işlem sonucu ile alakalı bilgilendirme yapmamızı sağlayan
        // bir yöntem olarak göreceğiz.
        public Result(bool ısSuccess,string message) :this(ısSuccess)
        {
             
            Message = message;
        }
        public Result (bool ısSuccess)
        {
            IsSuccess = ısSuccess;
        }
        public bool IsSuccess { get; }

        public string Message {  get; }
    }
}
