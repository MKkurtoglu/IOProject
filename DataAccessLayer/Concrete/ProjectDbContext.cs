using Base.EntitiesBase.Concrete;
using EntitiesLayer.Concrete;
using EntitiesLayer.Concrete.ECommerce.Models.LegalDocuments;
using EntityLayer.Concrete;
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

       public DbSet<Product> Products { get; set; } //bu prop'lar database'deki hangi tabloya hangi class ile eşleşeceğini belirttik.
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<BaseImage>  BaseImages{ get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
        public DbSet<EntityImage> EntityImages { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CompanyInformation> CompanyInformations{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(e => e.ProductId);

                // Sütun eşleştirmeleri
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                ////İlişki yapılandırmaları
                //entity.HasOne(p => p.Category)
                //    .WithMany()
                //    .HasForeignKey(p => p.CategoryId)
                //    .HasConstraintName("FK_Products_Categories");

                //entity.HasOne(p => p.Brand)
                //    .WithMany()
                //    .HasForeignKey(p => p.BrandId)
                //    .HasConstraintName("FK_Products_Brands");

                //entity.HasOne(p => p.Model)
                //    .WithMany()
                //    .HasForeignKey(p => p.ModelId)
                //    .HasConstraintName("FK_Products_Models");
            });
            
        }
    }

    
}
