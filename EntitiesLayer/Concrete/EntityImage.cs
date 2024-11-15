using Base.EntitiesBase;


namespace EntitiesLayer.Concrete
{
    public class EntityImage : IEntity
    {
        public int EntityImageId { get; set; }
        public int ImageId { get; set; }
        public string EntityType { get; set; } // "Product", "Category", "BlogPost","User"  gibi
        public int EntityId { get; set; }
        public bool IsPrimary { get; set; } // Ana resim mi?
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual BaseImage Image { get; set; }
    }
}
