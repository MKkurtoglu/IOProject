using Base.EntitiesBase;
using EntitiesLayer.Concrete;

namespace EntitiesLayer.DTOs
{
    public class CategoryDto : IDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }
        public string ImagePaths { get; set; } // Kategoriye ait resim
        public int ProductCount { get;set; }
    }
}
