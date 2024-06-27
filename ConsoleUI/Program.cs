// See https://aka.ms/new-console-template for more information
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;



//CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
//foreach (var item in customerManager.GetAllByCountry("Ca"))
//{
//    Console.WriteLine(item.City);
//} 
ProductManager productManager = new ProductManager(new EfProductDal());
var result = productManager.GetAll();
if (result.IsSuccess==true)
{
	foreach (var item in result.Data)
	{
        Console.WriteLine(item.ProductName +" " +item.CategoryId);
    }
    
}
else
{
    Console.WriteLine(result.Message);
}