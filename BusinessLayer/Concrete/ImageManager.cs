using Base.EntitiesBase.Concrete;
using Base.Utilities.Business;
using Base.Utilities.ImageHelper;
using Base.Utilities.Results;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ImageManager : IImageService
    {
        private readonly IImageDal _imageDal;
        private readonly IEntityImageDal _entityImageDal;
        private string guid;

        public ImageManager(IEntityImageDal entityImageDal, IImageDal imageDal)
        {
            _entityImageDal = entityImageDal;
            _imageDal = imageDal;
        }

        public async Task<IResult> AddImageAsync(int entityId, string entityType, IFormFile formFile, bool isPrimary = false)
        {
            var checkAllowExtension = CheckAllowExtension(guid);
            var checkMIME = CheckMIME(formFile);
            IResult[] metot = new[] { checkAllowExtension, checkMIME };

            BusinessRule.Run(metot);

            GuidHelper.CreaterGuid(formFile, entityType, out guid);
            await EditImageSizeAsync(formFile, entityType, guid);

            var baseImage = new BaseImage()
            {
                FilePath = guid,
                UploadDate = DateTime.Now,
            };

            await _imageDal.AddAsync(baseImage);

            var entityImage = new EntityImage()
            {
                EntityId = entityId,
                EntityType = entityType,
                ImageId = baseImage.ImageId,
                IsPrimary = isPrimary,
                Image = baseImage
            };
            await _entityImageDal.AddAsync(entityImage);

            var data = await _entityImageDal.GetAsync(c => c.EntityId == entityId);
            if (data != null)
            {
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }

        public async Task<IResult> DeleteAsync(EntityImage entity)
        {
            var data = await _imageDal.GetAsync(c => c.ImageId == entity.ImageId);
            await _entityImageDal.DeleteAsync(entity);
            GuidHelper.Delete(entity.Image.FilePath, entity.EntityType);
            await _imageDal.DeleteAsync(data);
            return new SuccessResult();
        }

        public async Task<IDataResult<EntityImage>> GetByIdAsync(int id)
        {
            var data = await _entityImageDal.GetAsync(c => c.EntityImageId == id);
            if (data != null)
            {
                return new SuccessDataResult<EntityImage>(data);
            }
            else
            {
                return new ErrorDataResult<EntityImage>(data);
            }
        }

        public async Task<IDataResult<List<EntityImage>>> GetAllImageByEntityAsync(int entityId, string entityType)
        {
            var images = await _entityImageDal.GetAllAsync(i => i.EntityId == entityId && i.EntityType == entityType);

            // Eğer bir resim isPrimary == true ise ilk sıraya al
            var primaryImage = images.FirstOrDefault(i => i.IsPrimary);

            if (primaryImage != null)
            {
                // isPrimary == true olan resmi listenin başına koy
                images.Remove(primaryImage);
                images.Insert(0, primaryImage);
            }
            else
            {
                // Eğer isPrimary yoksa, yüklenme tarihine göre sırala
                images = images.OrderBy(i => i.Image.UploadDate).ToList();
            }

            return new SuccessDataResult<List<EntityImage>>(images);
        }

        public async Task<IDataResult<EntityImage>> GetEntityImageByImageIdAsync(int imageId, string entityType)
        {
            var images = await _entityImageDal.GetAsync(i => i.ImageId == imageId && i.EntityType == entityType);
            if (images != null)
            {
                return new SuccessDataResult<EntityImage>(images);
            }
            else
            {
                return new ErrorDataResult<EntityImage>(images);
            }
        }

        public async Task<IResult> UpdateImageAsync(EntityImage entity, IFormFile formFile)
        {
            var checkAllowExtension = CheckAllowExtension(entity.Image.FilePath);
            var checkMIME = CheckMIME(formFile);
            IResult[] metot = new[] { checkAllowExtension, checkMIME };

            BusinessRule.Run(metot);

            GuidHelper.Update(formFile, entity.EntityType, entity.Image.FilePath, out guid);
            await EditImageSizeAsync(formFile, entity.EntityType, guid);

            entity.Image.UploadDate = DateTime.Now;
            await _imageDal.UpdateAsync(entity.Image);
            return new SuccessResult();
        }

        private IResult CheckAllowExtension(string extension)
        {
            var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(extension))
            {
                return new ErrorResult("Invalid image format. Only JPG and PNG are allowed.");
            }
            return new SuccessResult();
        }

        private IResult CheckMIME(IFormFile formFile)
        {
            if (formFile.ContentType != "image/jpeg" && formFile.ContentType != "image/png")
            {
                return new ErrorResult("Invalid image format. Only JPG and PNG are allowed.");
            }
            return new SuccessResult();
        }

        private async Task EditImageSizeAsync(IFormFile formFile, string entityType, string guid)
        {
            using (var image = await Image.LoadAsync(formFile.OpenReadStream()))
            {
                int width = 300;
                int height = 300;
                // Resmi yeniden boyutlandır (örnek olarak 300x300 piksel)
                if (entityType == "product")
                {
                    width = 400;
                    height = 400;
                }
                if (entityType == "category")
                {
                    width = 200;
                    height = 200;
                }

                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(width, height),
                    Mode = ResizeMode.Crop // Resmi kırparak yeniden boyutlandır
                }));

                // Resmi diske kaydediyoruz
                await image.SaveAsync(guid);
            }
        }

        public Task<IDataResult<List<EntityImage>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IResult> AddAsync(EntityImage entity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateAsync(EntityImage entity)
        {
            throw new NotImplementedException();
        }
    }
}
