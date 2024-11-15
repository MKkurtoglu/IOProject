using Base.EntitiesBase;

namespace EntitiesLayer.DTOs
{
    public class FavoriteUpdateDto : IDTO
    {
        public int FavoriteId { get; set; }
        public int ProductId { get; set; }
       
    }
}
