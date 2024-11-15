using Base.EntitiesBase;
using Base.EntitiesBase.Concrete;
using EntitiesLayer.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Concrete
{
    public class Favorites : IEntity
    {
        [Key]
        public int FavoriteId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        
    }

}
