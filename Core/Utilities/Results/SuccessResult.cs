using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utilities.Results
{
    public class SuccessResult : Result
    {
        // bu class'ın oluşturulma sebebi kullanıcıyı rahatlatmak içindir. eğer başarılı ise bu sınıfı çağıracak 
        // eğer başarısız ise errorresult diye oluşturduğumuz sınıfı çağıracaktır.
        public SuccessResult( string message) : base(true, message)
        {
            // bunun olmasının sebebi biz bu nesneden oluşturduğumuz da 
            //otomatik olarak kalıtım aldığı sınıftan da bir nesne oluşturalacak. bu sebeple onun ctor'larına 
            // istediği değerleri gödnermek zorundayız.
        }
        public SuccessResult() : base(true) // mesaj göndermeyip sadece true olduğunu belirtmek istediğimiz ctor
        {

        }
    }
}
