using Base.EntitiesBase;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace EntitiesLayer.Concrete
{
    public class BaseImage : IEntity
    {
        [Key]
        public int ImageId { get; set; }
       
        public string FilePath { get; set; }
        
        public DateTime UploadDate { get; set; }
    }
}
