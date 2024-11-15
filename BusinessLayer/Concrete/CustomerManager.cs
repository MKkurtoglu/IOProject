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

        // Asynchronous Delete
        public async Task<IResult> DeleteAsync(Customer entity)
        {
            await _customerDal.DeleteAsync(entity);
            return new SuccessResult();
        }

        // Asynchronous Get by Id
        public async Task<IDataResult<Customer>> GetByIdAsync(int id)
        {
            var stringId = id.ToString();
            var result = await _customerDal.GetAsync(c => c.CustomerId == stringId);
            if (result == null)
            {
                return new ErrorDataResult<Customer>($"Customer with ID {id} not found.");
            }
            return new SuccessDataResult<Customer>(result);
        }

        // Asynchronous Get All
        public async Task<IDataResult<List<Customer>>> GetAllAsync()
        {
            var result = await _customerDal.GetAllAsync();
            return new SuccessDataResult<List<Customer>>(result);
        }

        // Asynchronous Get All By Country
        

        // Asynchronous Insert
        public async Task<IResult> AddAsync(Customer entity)
        {
            await _customerDal.AddAsync(entity);
            return new SuccessResult();
        }

        // Asynchronous Update
        public async Task<IResult> UpdateAsync(Customer entity)
        {
            await _customerDal.UpdateAsync(entity);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<Customer>>> GetAllByCountry(string country)
        {
            var result = await _customerDal.GetAllAsync(c => c.City == country);
            return new SuccessDataResult<List<Customer>>(result);
        }
    }
}
