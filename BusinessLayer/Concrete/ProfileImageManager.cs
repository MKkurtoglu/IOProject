using Base.Extensions;
using Base.Utilities.Business;
using Base.Utilities.ImageHelper;
using Base.Utilities.Results;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProfileImageManager : IProfileImageService
    {
        string guid;
        private readonly IProfileImageDal _profileImageDal;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileImageManager(IProfileImageDal profileImageDal, IHttpContextAccessor httpContextAccessor)
        {
            _profileImageDal = profileImageDal;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResult> AddProfileImageAsync(IFormFile formFile)
        {
            var checkAllowExtension = CheckAllowExtension(guid);
            var checkMIME = CheckMIME(formFile);
            var result = BusinessRule.Run(checkAllowExtension, checkMIME);

            if (result != null)
                return result;

            var id = _httpContextAccessor.HttpContext.User.FindId();
            var userId = int.Parse(id);

             ProfileGuidHelper.CreaterProfileGuid(formFile, out guid);
            await EditImageSizeAsync(formFile, guid);

            var profile = new ProfileImage
            {
                UserId = userId,
                Url = guid,
                UploadedAt = DateTime.Now
            };

            await _profileImageDal.AddAsync(profile);
            var data = await _profileImageDal.GetAsync(c => c.UserId == userId);

            return data != null
                ? new SuccessResult()
                : new ErrorResult();
        }

        public async Task<IResult> DeleteAsync(ProfileImage entity)
        {
            ProfileGuidHelper.Delete(entity.Url);

            // Sonra veritabanı işlemini yap (async)
            await _profileImageDal.DeleteAsync(entity);
            return new SuccessResult();
        }

        public async Task<IDataResult<ProfileImage>> GetByIdAsync(int id)
        {
            var data = await _profileImageDal.GetAsync(r => r.Id == id);
            return new SuccessDataResult<ProfileImage>(data);
        }

        public async Task<IDataResult<List<ProfileImage>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<ProfileImage>> GetAllImageByUserAsync()
        {
            var id = _httpContextAccessor.HttpContext.User.FindId();
            var userId = int.Parse(id);
            var data = await _profileImageDal.GetAsync(r => r.UserId == userId);
            return new SuccessDataResult<ProfileImage>(data);
        }

        public async Task<IResult> AddAsync(ProfileImage entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> UpdateAsync(ProfileImage entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> UpdateImageAsync(ProfileImage profileImage, IFormFile formFile)
        {
            var checkAllowExtension = CheckAllowExtension(profileImage.Url);
            var checkMIME = CheckMIME(formFile);
            var result = BusinessRule.Run(checkAllowExtension, checkMIME);

            if (result != null)
                return result;

            ProfileGuidHelper.Update(formFile, profileImage.Url!, out guid);
            await EditImageSizeAsync(formFile, guid);

            profileImage.UploadedAt = DateTime.Now;
            await _profileImageDal.UpdateAsync(profileImage);
            return new SuccessResult();
        }

        private IResult CheckAllowExtension(string extension)
        {
            var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
            return !allowedExtensions.Contains(extension)
                ? new ErrorResult("Invalid image format. Only JPG and PNG are allowed.")
                : new SuccessResult();
        }

        private IResult CheckMIME(IFormFile formFile)
        {
            return (formFile.ContentType != "image/jpeg" && formFile.ContentType != "image/png")
                ? new ErrorResult("Invalid image format. Only JPG and PNG are allowed.")
                : new SuccessResult();
        }

        private async Task EditImageSizeAsync(IFormFile formFile, string guid)
        {
            using (var image = await Image.LoadAsync(formFile.OpenReadStream()))
            {
                int width = 300;
                int height = 300;

                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(width, height),
                    Mode = ResizeMode.Crop
                }));

                await image.SaveAsync(guid);
            }
        }
    }
}
