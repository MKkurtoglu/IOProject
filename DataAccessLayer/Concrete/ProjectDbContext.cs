using Base.EntitiesBase.Concrete;
using EntitiesLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    // DbContext mirası bizim db ile entity nesnelerimizin bağlantı kurmasını sağlayan bir sınıftır.
    public class ProjectDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // bu metot bizim projemizi hangi veri tabanı ile ilişkileneceğini  belirttiğimiz metottur.
        {
            //normalde gerçek projede server'ın nerede olduğunu belirtmemmiz için
            // aşağıdaki metot içerisine @"server= 180.141.1.12 gibi ip adresi girilir.
            optionsBuilder.UseSqlServer(@"server=DESKTOP-F2H9TMP;Database=Northwind;integrated security=true;Trusted_Connection=True;encrypt=false;");
            // yukarıdaki parametrede trusted_con.. kısmına true yaptığımız da kullanıcı şifre gerektirmiyor. direkt bağlantı. 
            // bu güvensiz lduğunu anlamına gelmiyor. güçlü güvenilir domainlerde bu şekilde kullanılır. 
            // ya da alternatif çözüm olarak kullancıı adı vs girilebilir o kısıma
        }

        DbSet<Product> Products { get; set; } //bu prop'lar database'deki hangi tabloya hangi class ile eşleşeceğini belirttik.
        DbSet<Category> Categories { get; set; }
        DbSet<Customer> Customers { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
