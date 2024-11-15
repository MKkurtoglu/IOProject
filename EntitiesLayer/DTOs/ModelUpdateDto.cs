using Base.EntitiesBase;

namespace EntitiesLayer.DTOs
{
    public class ModelUpdateDto : IDTO
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }
    }
}
