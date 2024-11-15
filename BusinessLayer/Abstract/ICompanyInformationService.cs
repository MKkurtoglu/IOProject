using Base.Utilities.Results;
using EntitiesLayer.Concrete.ECommerce.Models.LegalDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICompanyInformationService : IGenericService<CompanyInformation>
    {
        Task<IDataResult<CompanyInformation>> GetCompanyInfoAsync();
        Task<IResult> UpdateCompanyInfoAsync(CompanyInformation entity);
    }
}
