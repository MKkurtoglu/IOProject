using Base.EntitiesBase;

namespace EntitiesLayer.DTOs
{
    public class UserForRegisterDto : IDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
