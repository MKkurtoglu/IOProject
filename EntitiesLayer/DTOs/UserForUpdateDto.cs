using Base.EntitiesBase;

namespace EntitiesLayer.DTOs
{
    public class UserForUpdateDto : IDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
