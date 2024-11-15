using Base.Utilities.Results;
using EntitiesLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IImageService : IGenericService<EntityImage>
    {
        Task<IResult> AddImageAsync(int entityId, string entityType, IFormFile formFile, bool isPrimary);
        Task<IResult> UpdateImageAsync(EntityImage entity, IFormFile formFile);
        Task<IDataResult<EntityImage>> GetEntityImageByImageIdAsync(int imageId, string entityType);
        Task<IDataResult<List<EntityImage>>> GetAllImageByEntityAsync(int entityId, string entityType);
    }
}
