using BusinessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Concrete;
using DataAccessLayer.Abstract;
using Base.Utilities.Results;
namespace BusinessLayer.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Customer> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Customer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAllByCountry(string country)
        {
            throw new NotImplementedException();
        }

        public IResult Insert(Customer entity)
        {
            throw new NotImplementedException();
        }

        public IResult Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
