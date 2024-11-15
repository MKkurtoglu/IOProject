using Base.DataAccessBase.EfWorkBase;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete.ECommerce.Models.LegalDocuments;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfCustomerAgreementDal : EfGenericRepositoryDal<CustomerAgreement, ProjectDbContext>, ICustomerAgreementDal
    {
    }
}
