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
    public class EfCategoryDal : EfGenericRepositoryDal<Category, ProjectDbContext>, ICategoryDal
    {
        public async Task<List<CategoryDto>> GetAllCategoryDtoAsync(Expression<Func<CategoryDto, bool>> filter = null)
        {
            using (var _context = new ProjectDbContext())
            {
                var result = from category in _context.Categories
                             where !category.IsDeleted
                             join image in _context.EntityImages
                             on category.CategoryId equals image.EntityId into categoryImages
                             from image in categoryImages.DefaultIfEmpty() // Left join to include categories without images
                             group new { category, image } by new
                             {
                                 category.CategoryId,
                                 category.CategoryName,
                                 category.IsDeleted
                             } into grouped
                             select new CategoryDto
                             {
                                 CategoryId = grouped.Key.CategoryId,
                                 CategoryName = grouped.Key.CategoryName,
                                 IsDeleted = grouped.Key.IsDeleted,
                                 ImagePaths = grouped
                                     .Where(g => g.image != null)
                                     .OrderBy(g => g.image.Image.UploadDate)
                                     .Select(g => g.image.Image.FilePath)
                                     .FirstOrDefault(), // Get the first non-null image URL or null if none exist
                                 ProductCount = _context.Products.Count(p => p.CategoryId == grouped.Key.CategoryId)
                             };

                // Eğer bir filtre varsa, uygula
                if (filter != null)
                {
                    result = result.Where(filter);
                }

                return await result.ToListAsync();
            }
        }

        public async Task<CategoryDto> GetCategoryDtoByIdAsync(Expression<Func<CategoryDto, bool>> filter = null)
        {
            using (var _context = new ProjectDbContext())
            {
                var query = from category in _context.Categories
                            join image in _context.EntityImages on category.CategoryId equals image.EntityId into categoryImages
                            from image in categoryImages.DefaultIfEmpty()
                            where !category.IsDeleted // Kategori silinmediyse
                            select new CategoryDto
                            {
                                CategoryId = category.CategoryId,
                                CategoryName = category.CategoryName,
                                IsDeleted = category.IsDeleted,
                                ImagePaths = image != null ? image.Image.FilePath : null, // Resim varsa yolunu al, yoksa null
                                ProductCount = _context.Products.Count(p => p.CategoryId == category.CategoryId)
                            };

                // Eğer bir filtre varsa, uygula
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return await query.FirstOrDefaultAsync();
            }
        }
    }
}
