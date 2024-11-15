using Base.DataAccessBase.EfWorkBase;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfBrandDal : EfGenericRepositoryDal<Brand,ProjectDbContext> , IBrandDal
    {
    }
}
