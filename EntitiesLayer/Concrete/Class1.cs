using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Concrete
{
    using Base.EntitiesBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace ECommerce.Models.LegalDocuments
    {
        // Temel varlık sınıfı
        public abstract class BaseEntity
        {
            public int Id { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
            public bool IsActive { get; set; } = true;
            public string CreatedBy { get; set; }
            public string UpdatedBy { get; set; }
        }

        // Şirket bilgileri
        public class CompanyInformation : BaseEntity,IEntity
        {
            [Required]
            [StringLength(250)]
            public string CompanyName { get; set; }

            [Required]
            [StringLength(500)]
            public string Address { get; set; }

            [Required]
            [StringLength(20)]
            public string Phone { get; set; }

            [Required]
            [EmailAddress]
            [StringLength(100)]
            public string Email { get; set; }

            [Required]
            [Url]
            [StringLength(100)]
            public string Website { get; set; }

            [Required]
            [StringLength(100)]
            public string TaxOffice { get; set; }

            [Required]
            [StringLength(20)]
            public string TaxNumber { get; set; }

            [StringLength(50)]
            public string TradeRegistryNo { get; set; }

            [StringLength(50)]
            public string MersisNo { get; set; }

            // Varsa diğer şirket bilgileri
            public string LogoUrl { get; set; }
            public string CompanyType { get; set; }
        }

        // Yasal döküman tipleri için enum
        public enum LegalDocumentType
        {
            DistanceSalesAgreement = 1,
            MembershipAgreement = 2,
            PrivacyPolicy = 3,
            ShippingPolicy = 4,
            RefundPolicy = 5,
            WarrantyTerms = 6
        }

        // Yasal döküman şablonu
        public class LegalDocumentTemplate : BaseEntity,IEntity
        {
            [Required]
            public LegalDocumentType DocumentType { get; set; }

            [Required]
            [StringLength(250)]
            public string Title { get; set; }

            [Required]
            public string Content { get; set; }

            public int Version { get; set; }

            // Şablon değişkenleri için JSON olarak saklanacak ayarlar
            public string Variables { get; set; }

            // Geçerlilik süresi
            public DateTime? ValidFrom { get; set; }
            public DateTime? ValidTo { get; set; }

            // İlişkili dökümanlar
            public virtual ICollection<CustomerAgreement> CustomerAgreements { get; set; }
        }

        // Müşteri sözleşmeleri
        public class CustomerAgreement : BaseEntity ,IEntity
        {
            [Required]
            public int CustomerId { get; set; }

            public int? OrderId { get; set; }

            [Required]
            public int LegalDocumentTemplateId { get; set; }

            [Required]
            public LegalDocumentType DocumentType { get; set; }

            // Müşterinin kabul ettiği içerik (doldurulmuş hali)
            [Required]
            public string AcceptedContent { get; set; }

            [Required]
            public DateTime AcceptedAt { get; set; }

            [Required]
            [StringLength(50)]
            public string IpAddress { get; set; }

            public string UserAgent { get; set; }

            // Kabul edilen sözleşme versiyonu
            public int DocumentVersion { get; set; }

            // Navigation properties
            [ForeignKey("CustomerId")]
            public virtual Customer Customer { get; set; }

            [ForeignKey("LegalDocumentTemplateId")]
            public virtual LegalDocumentTemplate Template { get; set; }
        }

        // Döküman değişken ayarları
        public class DocumentSettings : BaseEntity , IEntity
        {
            [Required]
            public LegalDocumentType DocumentType { get; set; }

            // Teslimat süresi (gün)
            public int DeliveryTime { get; set; }

            // İade süresi (gün)
            public int ReturnPeriod { get; set; }

            // Cayma hakkı süresi (gün)
            public int CancellationPeriod { get; set; }

            // Garanti süresi (ay)
            public int WarrantyPeriod { get; set; }

            // Minimum sipariş tutarı
            public decimal MinimumOrderAmount { get; set; }

            // Ücretsiz kargo limiti
            public decimal FreeShippingAmount { get; set; }

            // Diğer ayarlar için JSON alanı
            public string AdditionalSettings { get; set; }
        }

        // Değişken değerleri için helper sınıf
        public class DocumentVariables
        {
            public CompanyInformation Company { get; set; }
            public DateTime CurrentDate { get; set; }
            public DocumentSettings Settings { get; set; }
            public Dictionary<string, string> CustomVariables { get; set; }
        }

        // Döküman oluşturucu servis
        public interface ILegalDocumentService
        {
            Task<string> GenerateDocument(LegalDocumentType type, DocumentVariables variables);
            Task<CustomerAgreement> CreateCustomerAgreement(int customerId, LegalDocumentType type, int? orderId = null);
            Task<bool> ValidateCustomerAgreements(int customerId);
            Task<List<CustomerAgreement>> GetCustomerAgreements(int customerId);
        }
    }
}
