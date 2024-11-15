using Base.EntitiesBase;
using EntitiesLayer.Concrete;

namespace EntitiesLayer.DTOs
{
    public class ProductDto :IDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string ModelName { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public string PrimaryImagePath { get; set; }
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public List<string> ImagePaths { get; set; } // Ürüne ait resimler
    }
}
