using Base.DataAccessBase.EfWorkBase;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete.ECommerce.Models.LegalDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfLegalDocumentTemplateDal : EfGenericRepositoryDal<LegalDocumentTemplate, ProjectDbContext>, ILegalDocumentTemplateDal
    {
    }
}
