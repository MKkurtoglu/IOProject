
using Base.EntitiesBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Concrete
{
    public class Product : IEntity
    {
        public int ProductId { get; set; }
        
        public int CategoryId { get; set; }
        

        public int? ModelId { get; set; }
        public int BrandId { get; set; }

        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }

        public virtual Model Model { get; set; }
        public virtual ICollection<EntityImage> Images { get; set; }

    }
}
