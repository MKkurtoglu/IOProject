using Base.DataAccessBase.EfWorkBase;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfProductDal : EfGenericRepositoryDal<Product, ProjectDbContext>, IProductDal
    {
        public async Task<ProductDto> ProductDTOAsync(Expression<Func<ProductDto, bool>> filter = null)
        {
            using (var projectContext = new ProjectDbContext())
            {
                var query = from p in projectContext.Products
                            join b in projectContext.Brands on p.BrandId equals b.BrandId
                            join c in projectContext.Categories on p.CategoryId equals c.CategoryId
                            join m in projectContext.Models on p.ModelId equals m.ModelId into productModels
                            from model in productModels.DefaultIfEmpty()
                            select new ProductDto
                            {
                                ProductId = p.ProductId,
                                ProductName = p.ProductName,
                                BrandName = b.BrandName,
                                CategoryName = c.CategoryName,
                                ModelName = model == null ? null : model.ModelName,
                                UnitPrice = p.UnitPrice,
                                UnitsInStock = p.UnitsInStock,
                                Brand = b,
                                Category = c,
                                ImagePaths = projectContext.EntityImages
                                    .Where(pi => pi.EntityId == p.ProductId && pi.EntityType == "Product")
                                    .Select(pi => pi.Image.FilePath)
                                    .Take(5)
                                    .ToList(),
                                PrimaryImagePath = projectContext.EntityImages
                                    .Where(pi => pi.EntityId == p.ProductId && pi.EntityType == "Product" && pi.IsPrimary)
                                    .Select(pi => pi.Image.FilePath)
                                    .FirstOrDefault()
                            };

                return filter == null
                    ? await query.FirstOrDefaultAsync()
                    : await query.Where(filter).FirstOrDefaultAsync();
            }
        }

        public async Task<List<ProductDto>> ProductsDTOAsync(Expression<Func<ProductDto, bool>> filter = null)
        {
            using (var projectContext = new ProjectDbContext())
            {
                var query = from p in projectContext.Products
                            join b in projectContext.Brands on p.BrandId equals b.BrandId into productBrands
                            from brand in productBrands.DefaultIfEmpty()
                            join c in projectContext.Categories on p.CategoryId equals c.CategoryId into productCategories
                            from category in productCategories.DefaultIfEmpty()
                            join m in projectContext.Models on p.ModelId equals m.ModelId into productModels
                            from model in productModels.DefaultIfEmpty()
                            select new ProductDto
                            {
                                ProductId = p.ProductId,
                                ProductName = p.ProductName,
                                BrandName = brand.BrandName,
                                CategoryName = category.CategoryName,
                                ModelName = model.ModelName,
                                UnitPrice = p.UnitPrice,
                                UnitsInStock = p.UnitsInStock,
                                Brand = brand,
                                Category = category,
                                ImagePaths = projectContext.EntityImages
                                    .Where(pi => pi.EntityId == p.ProductId && pi.EntityType == "Product")
                                    .Select(pi => pi.Image.FilePath)
                                    .Take(5)
                                    .ToList(),
                                PrimaryImagePath = projectContext.EntityImages
                                    .Where(pi => pi.EntityId == p.ProductId && pi.EntityType == "Product" && pi.IsPrimary)
                                    .Select(pi => pi.Image.FilePath)
                                    .FirstOrDefault()
                            };

                return filter == null
                    ? await query.ToListAsync()
                    : await query.Where(filter).ToListAsync();
            }
        }
    }
}
