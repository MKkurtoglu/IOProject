using Base.EntitiesBase;

namespace EntitiesLayer.Concrete
{
    public class Cart : IEntity
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

}
