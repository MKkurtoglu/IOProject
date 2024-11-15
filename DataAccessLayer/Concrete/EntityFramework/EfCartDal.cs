using Base.DataAccessBase.EfWorkBase;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfCartDal : EfGenericRepositoryDal<Cart, ProjectDbContext>, ICartDal
    {
        public async Task<List<CartDto>> GetAllCartItemsByUserAsync(int userId)
        {
            using (var projectContext = new ProjectDbContext())
            {
                var cartItems = await projectContext.Carts
                    .Where(c => c.UserId == userId)
                    .ToListAsync();

                var cartDtos = await Task.WhenAll(cartItems.Select(async c => new CartDto
                {
                    CartId = c.CartId,
                    Quantity = c.Quantity,
                    Product = new ProductDto
                    {
                        ProductId = c.ProductId,
                        ProductName = await projectContext.Products
                            .Where(p => p.ProductId == c.ProductId)
                            .Select(p => p.ProductName)
                            .FirstOrDefaultAsync(),
                        BrandName = await projectContext.Brands
                            .Where(b => b.BrandId == projectContext.Products
                                .Where(p => p.ProductId == c.ProductId)
                                .Select(p => p.BrandId)
                                .FirstOrDefault())
                            .Select(b => b.BrandName)
                            .FirstOrDefaultAsync(),
                        CategoryName = await projectContext.Categories
                            .Where(cat => cat.CategoryId == projectContext.Products
                                .Where(p => p.ProductId == c.ProductId)
                                .Select(p => p.CategoryId)
                                .FirstOrDefault())
                            .Select(cat => cat.CategoryName)
                            .FirstOrDefaultAsync(),
                        ModelName = await projectContext.Models
                            .Where(m => m.ModelId == projectContext.Products
                                .Where(p => p.ProductId == c.ProductId)
                                .Select(p => p.ModelId)
                                .FirstOrDefault())
                            .Select(m => m.ModelName)
                            .FirstOrDefaultAsync(),
                        UnitPrice = await projectContext.Products
                            .Where(p => p.ProductId == c.ProductId)
                            .Select(p => p.UnitPrice)
                            .FirstOrDefaultAsync(),
                        UnitsInStock = await projectContext.Products
                            .Where(p => p.ProductId == c.ProductId)
                            .Select(p => p.UnitsInStock)
                            .FirstOrDefaultAsync(),
                        ImagePaths = await projectContext.EntityImages
                            .Where(pi => pi.EntityId == c.ProductId && pi.EntityType == "Product")
                            .Select(pi => pi.Image.FilePath)
                            .Take(5)
                            .ToListAsync(),
                        PrimaryImagePath = await projectContext.EntityImages
                            .Where(pi => pi.EntityId == c.ProductId && pi.EntityType == "Product" && pi.IsPrimary)
                            .Select(pi => pi.Image.FilePath)
                            .FirstOrDefaultAsync()
                    },
                    SubTotal = (await projectContext.Products
                        .Where(p => p.ProductId == c.ProductId)
                        .Select(p => p.UnitPrice)
                        .FirstOrDefaultAsync()) * c.Quantity
                }).ToList());

                return cartDtos.ToList();
            }
        }

        public async Task<decimal> GetCartTotalAsync(int userId)
        {
            using (var projectContext = new ProjectDbContext())
            {
                var total = await projectContext.Carts
                    .Where(c => c.UserId == userId)
                    .SumAsync(c => c.Quantity * projectContext.Products
                        .Where(p => p.ProductId == c.ProductId)
                        .Select(p => p.UnitPrice)
                        .FirstOrDefault());

                return total;
            }
        }
    }
}
