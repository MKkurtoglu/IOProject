using Base.DataAccessBase;
using EntitiesLayer.Concrete.ECommerce.Models.LegalDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICompanyInformationDal : IGenericDal<CompanyInformation>
    {
        Task<CompanyInformation> GetCompanyInfoAsync();
        Task<bool> UpdateCompanyInfoAsync(CompanyInformation entity);
    }
}
