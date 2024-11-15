// See https://aka.ms/new-console-template for more information
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore.Infrastructure;



CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
foreach (var item in customerManager.GetAllByCountry("Ca"))
{
    Console.WriteLine(item.City);
}


