using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utilities.Results
{
    public interface IDataResult<T> : IResult
        // aynı zmaan da bu bir mesaj ve işlem sonucu belirteceğimiz için IResulttan kalıtım alacak.
    {
T Data {  get; }
    }
}
