using Base.DataAccessBase;
using DataAccessLayer.Concrete;
using EntitiesLayer.Concrete.ECommerce.Models.LegalDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ILegalDocumentTemplateDal : IGenericDal<LegalDocumentTemplate>
    {
    }
}
