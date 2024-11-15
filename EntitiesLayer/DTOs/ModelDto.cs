using Base.EntitiesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.DTOs
{
    public class ModelDto :IDTO
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }  // DTO içerisine marka ismini direkt ekliyoruz
        public bool IsDeleted { get; set; }  // DTO içerisine marka ismini direkt ekliyoruz
    }
}
