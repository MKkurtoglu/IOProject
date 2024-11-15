using Base.Utilities.Results;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IProfileImageService : IGenericService<ProfileImage>
    {
        Task<IResult> AddProfileImageAsync(IFormFile formFile);
        Task<IResult> UpdateImageAsync(ProfileImage profileImage, IFormFile formFile);
        Task<IDataResult<ProfileImage>> GetAllImageByUserAsync();
    }
}
