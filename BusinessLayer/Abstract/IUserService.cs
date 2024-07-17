
using Base.DataAccessBase;
using Base.EntitiesBase.Concrete;
using Base.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUserService : IGenericService<User>
    {
        IDataResult< List<OperationClaim> > GetClaims(User user);
        
        IDataResult< User > GetByMail(string email);
    }
}
