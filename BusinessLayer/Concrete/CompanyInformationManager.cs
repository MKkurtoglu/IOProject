using Base.Aspects.Autofac.Validation;
using Base.Utilities.Results;
using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules.FluentValidation;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete.ECommerce.Models.LegalDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CompanyInformationManager : ICompanyInformationService
    {
        private readonly ICompanyInformationDal _companyInformationDal;

        public CompanyInformationManager(ICompanyInformationDal companyInformationDal)
        {
            _companyInformationDal = companyInformationDal;
        }
        [ValidationAspect(typeof(CompanyInformationValidator))]
        public Task<IResult> AddAsync(CompanyInformation entity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> DeleteAsync(CompanyInformation entity)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<List<CompanyInformation>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<CompanyInformation>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<CompanyInformation>> GetCompanyInfoAsync()
        {
            var company = await _companyInformationDal.GetCompanyInfoAsync();
            if (company == null)
                return new ErrorDataResult<CompanyInformation>("Şirket bilgisi bulunamadı");

            return new SuccessDataResult<CompanyInformation>(company);
        }

        public Task<IResult> UpdateAsync(CompanyInformation entity)
        {
            throw new NotImplementedException();
        }
        [ValidationAspect(typeof(CompanyInformationValidator))]

        public async Task<IResult> UpdateCompanyInfoAsync(CompanyInformation entity)
        {
            var result = await _companyInformationDal.UpdateCompanyInfoAsync(entity);
            return result
                ? new SuccessResult("Şirket bilgileri başarıyla güncellendi")
                : new ErrorResult("Şirket bilgileri güncellenirken hata oluştu");
        }
      


    }
}
