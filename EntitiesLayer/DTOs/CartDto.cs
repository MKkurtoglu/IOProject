using Base.EntitiesBase;

namespace EntitiesLayer.DTOs
{
    public class CartDto :IDTO
    {
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public ProductDto Product { get; set; }
        public decimal SubTotal { get; set; } // Ürünün birim fiyatı ile miktarın çarpımı
    }

}
