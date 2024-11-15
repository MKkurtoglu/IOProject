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
    public class EfFavoriteDal : EfGenericRepositoryDal<Favorites, ProjectDbContext>, IFavoriteDal
    {
        public async Task<List<FavoriteDto>> GetAllFavoriteByUserAsync(int userId)
        {
            using var projectContext = new ProjectDbContext();
            
                // Kullanıcının favorilerini asenkron olarak filtreliyoruz
                var favorites = await projectContext.Favorites
                    .Where(f => f.UserId == userId)
                    .ToListAsync();

                var favoriteDtos = await Task.WhenAll(favorites.Select(async f => new FavoriteDto
                {
                    FavoriteId = f.FavoriteId,
                    Product = new ProductDto
                    {
                        ProductId = f.ProductId,
                        ProductName = await projectContext.Products
                            .Where(p => p.ProductId == f.ProductId)
                            .Select(p => p.ProductName)
                            .FirstOrDefaultAsync(),

                        BrandName = await projectContext.Brands
                            .Where(b => b.BrandId == projectContext.Products
                                .Where(p => p.ProductId == f.ProductId)
                                .Select(p => p.BrandId)
                                .FirstOrDefault())
                            .Select(b => b.BrandName)
                            .FirstOrDefaultAsync(),

                        CategoryName = await projectContext.Categories
                            .Where(c => c.CategoryId == projectContext.Products
                                .Where(p => p.ProductId == f.ProductId)
                                .Select(p => p.CategoryId)
                                .FirstOrDefault())
                            .Select(c => c.CategoryName)
                            .FirstOrDefaultAsync(),

                        ModelName = await projectContext.Models
                            .Where(m => m.ModelId == projectContext.Products
                                .Where(p => p.ProductId == f.ProductId)
                                .Select(p => p.ModelId)
                                .FirstOrDefault())
                            .Select(m => m.ModelName)
                            .FirstOrDefaultAsync(),

                        UnitPrice = await projectContext.Products
                            .Where(p => p.ProductId == f.ProductId)
                            .Select(p => p.UnitPrice)
                            .FirstOrDefaultAsync(),

                        UnitsInStock = await projectContext.Products
                            .Where(p => p.ProductId == f.ProductId)
                            .Select(p => p.UnitsInStock)
                            .FirstOrDefaultAsync(),

                        // Resim yollarını getiriyoruz
                        ImagePaths = await projectContext.EntityImages
                            .Where(pi => pi.EntityId == f.ProductId && pi.EntityType == "Product")
                            .Select(pi => pi.Image.FilePath)
                            .Take(5)
                            .ToListAsync(),

                        PrimaryImagePath = await projectContext.EntityImages
                            .Where(pi => pi.EntityId == f.ProductId && pi.EntityType == "Product" && pi.IsPrimary)
                            .Select(pi => pi.Image.FilePath)
                            .FirstOrDefaultAsync()
                    }
                }));

            return favoriteDtos.ToList();

        }
    }
}
