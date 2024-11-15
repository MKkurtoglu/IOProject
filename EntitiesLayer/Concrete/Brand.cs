using Base.EntitiesBase;


namespace EntitiesLayer.Concrete
{
    public class Brand : IEntity
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation property
      
        public virtual ICollection<Model> Models { get; set; } // bir markanın birden fazla modeli olabilir demek bu.
        public virtual ICollection<Product> Products{ get; set; } // bir markanın birden fazla modeli olabilir demek bu.
        public virtual ICollection<EntityImage> Images { get; set; }
    }
}
