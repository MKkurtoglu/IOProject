using Base.EntitiesBase;


namespace EntitiesLayer.Concrete
{
    public class Model : IEntity
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }

        // Foreign key
        public int BrandId { get; set; }

        // Navigation property
       
        public virtual Brand Brand { get; set; }
    }
}
