using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.EntitiesBase;


namespace EntitiesLayer.Concrete
{
    public class Category : IEntity
    {
        
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }
       
        
        public virtual EntityImage Images { get; set; }
    }

    
}
