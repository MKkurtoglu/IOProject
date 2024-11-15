using Base.EntitiesBase;

namespace EntitiesLayer.DTOs
{
    public class ModelAddDto :IDTO
    {
        public string ModelName { get; set; }
        public int BrandId { get; set; }
    }

    
}
