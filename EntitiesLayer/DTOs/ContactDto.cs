using Base.EntitiesBase;

namespace EntitiesLayer.DTOs
{
    public class Contact : IDTO
    {
      
        public SocialMediaDto SocialMedia { get; set; }    // Sosyal medya bilgileri
        public string MapUrl { get; set; }                 // Google Maps URL
        public List<string> EmailAddresses { get; set; }   // E-posta adresleri
        public List<string> PhoneNumbers { get; set; }     // Telefon numaraları
        public string WhatsappNumber { get; set; }         // WhatsApp iletişim numarası
    }
}
