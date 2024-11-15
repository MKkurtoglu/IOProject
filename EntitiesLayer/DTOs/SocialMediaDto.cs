using Base.EntitiesBase;

namespace EntitiesLayer.DTOs
{
    public class SocialMediaDto : IDTO
    {
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public string YouTube { get; set; }
    }
}
