using Base.DataAccessBase.EfWorkBase;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete.ECommerce.Models.LegalDocuments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfCompanyInformationDal : EfGenericRepositoryDal<CompanyInformation, ProjectDbContext>, ICompanyInformationDal
    {

        public async Task<CompanyInformation> GetCompanyInfoAsync()
        {
            using (var _context = new ProjectDbContext())
            {
                return await _context.CompanyInformations
         .FirstOrDefaultAsync(x => x.IsActive);
            }
        }

        public async Task<bool> UpdateCompanyInfoAsync(CompanyInformation entity)
        {
            using (var _context = new ProjectDbContext())
            {
                var existing = await GetCompanyInfoAsync();
                if (existing == null)
                {
                    // Hiç kayıt yoksa yeni kayıt oluştur
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.UtcNow;
                    await _context.CompanyInformations.AddAsync(entity);
                }
                else
                {
                    // Varolan kaydı güncelle
                    existing.CompanyName = entity.CompanyName;
                    existing.Address = entity.Address;
                    existing.Phone = entity.Phone;
                    existing.Email = entity.Email;
                    existing.Website = entity.Website;
                    existing.TaxOffice = entity.TaxOffice;
                    existing.TaxNumber = entity.TaxNumber;
                    existing.TradeRegistryNo = entity.TradeRegistryNo;
                    existing.MersisNo = entity.MersisNo;
                    existing.UpdatedAt = DateTime.UtcNow;
                    _context.CompanyInformations.Update(existing);
                }

                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
