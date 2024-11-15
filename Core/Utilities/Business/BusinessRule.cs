using Base.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utilities.Business
{
    public class BusinessRule
    {
        public static IResult Run(params IResult[] results)
        {
            foreach (var result in results)
            {
                if (!result.IsSuccess)
                {
                    return result;
                }

            }
            return null;
        }

        public static IResult Run(IEnumerable<IResult> results)
        {
            foreach (var result in results)
            {
                if (result != null && !result.IsSuccess)
                {
                    return result;  // İlk başarısız sonucu döner
                }
            }
            return new SuccessResult(); // Tüm sonuçlar başarılıysa başarı sonucu döner
        }

        public static async Task<IResult> RunAsync(params Task<IResult>[] results)
        {
            foreach (var result in results)
            {
                if (result != null && !result.Result.IsSuccess)
                {
                    return result.Result;  // İlk başarısız sonucu döner
                }
            }
            return new SuccessResult();
        }
    }
}
