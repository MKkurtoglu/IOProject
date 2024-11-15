using Base.EntitiesBase;

namespace EntitiesLayer.DTOs
{
    public class FavoriteDto : IDTO
    {
        public int FavoriteId { get; set; }
        public ProductDto Product { get; set; }
    }

}
