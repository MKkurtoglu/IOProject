using Base.DataAccessBase;
using Base.EntitiesBase.Concrete;
using EntitiesLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IUserDal : IGenericDal<User>
    {
        
            List<OperationClaim> GetClaims(User user);
        
    }
}