using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Concrete;
namespace BusinessLayer.Abstract
{
    public interface ICustomerService : IGenericService<Customer>
    {
        List<Customer> GetAllByCountry(string country);
    }
}
