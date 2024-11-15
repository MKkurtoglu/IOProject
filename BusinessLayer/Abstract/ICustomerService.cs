using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Utilities.Results;
using EntitiesLayer.Concrete;
namespace BusinessLayer.Abstract
{
    public interface ICustomerService : IGenericService<Customer>
    {
        Task<IDataResult<List<Customer>>> GetAllByCountry(string country);
    }
}
