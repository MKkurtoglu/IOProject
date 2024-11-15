using EntitiesLayer.Concrete.ECommerce.Models.LegalDocuments;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class DocumentSettingsValidator : AbstractValidator<DocumentSettings>
    {
        public DocumentSettingsValidator()
        {
            RuleFor(x => x.DocumentType)
                .IsInEnum().WithMessage("Geçerli bir döküman tipi seçiniz.");

            RuleFor(x => x.DeliveryTime)
                .GreaterThanOrEqualTo(0).WithMessage("Teslimat süresi 0'dan küçük olamaz.");

            RuleFor(x => x.ReturnPeriod)
                .GreaterThanOrEqualTo(0).WithMessage("İade süresi 0'dan küçük olamaz.");

            RuleFor(x => x.CancellationPeriod)
                .GreaterThanOrEqualTo(0).WithMessage("Cayma hakkı süresi 0'dan küçük olamaz.");

            RuleFor(x => x.WarrantyPeriod)
                .GreaterThanOrEqualTo(0).WithMessage("Garanti süresi 0'dan küçük olamaz.");

            RuleFor(x => x.MinimumOrderAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Minimum sipariş tutarı 0'dan küçük olamaz.");

            RuleFor(x => x.FreeShippingAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Ücretsiz kargo limiti 0'dan küçük olamaz.");
        }
    }
}
