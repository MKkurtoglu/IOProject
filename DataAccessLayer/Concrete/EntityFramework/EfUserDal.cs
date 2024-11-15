using Base.DataAccessBase.EfWorkBase;
using Base.EntitiesBase.Concrete;
using DataAccessLayer.Abstract;
using EntitiesLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfUserDal : EfGenericRepositoryDal<User, ProjectDbContext>, IUserDal
    {
        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            using (var context = new ProjectDbContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

                return await result.ToListAsync();
            }
        }

        public async Task<UserProfileDto> GetProfileDtoAsync(int userId)
        {
            using (var context = new ProjectDbContext())
            {
                var userProfile = await (from u in context.Users
                                         join ui in context.ProfileImages on u.Id equals ui.UserId into userImages
                                         from ui in userImages.DefaultIfEmpty()  // Left join for optional profile image
                                         where u.Id == userId
                                         select new UserProfileDto
                                         {
                                             Id = u.Id,
                                             FirstName = u.FirstName,
                                             LastName = u.LastName,
                                             Email = u.Email,
                                             ProfileImageUrl = ui != null ? ui.Url : null
                                         }).FirstOrDefaultAsync();

                return userProfile;
            }
        }
    }
}
