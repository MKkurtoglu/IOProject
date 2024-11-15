using Base.EntitiesBase;

namespace EntitiesLayer.DTOs
{
    public class AboutUs : IDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Vision { get; set; }
        public string Mission { get; set; }
       
    }
}
