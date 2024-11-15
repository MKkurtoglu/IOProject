using EntitiesLayer.Concrete.ECommerce.Models.LegalDocuments;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class CustomerAgreementValidator : AbstractValidator<CustomerAgreement>
    {
        public CustomerAgreementValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("Geçerli bir müşteri seçilmelidir.");

            RuleFor(x => x.LegalDocumentTemplateId)
                .GreaterThan(0).WithMessage("Geçerli bir döküman şablonu seçilmelidir.");

            RuleFor(x => x.DocumentType)
                .IsInEnum().WithMessage("Geçerli bir döküman tipi seçiniz.");

            RuleFor(x => x.AcceptedContent)
                .NotEmpty().WithMessage("Kabul edilen içerik boş olamaz.");

            RuleFor(x => x.AcceptedAt)
                .NotEmpty().WithMessage("Kabul tarihi boş olamaz.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Kabul tarihi gelecek bir tarih olamaz.");

            RuleFor(x => x.IpAddress)
                .NotEmpty().WithMessage("IP adresi boş olamaz.")
                .MaximumLength(50).WithMessage("IP adresi 50 karakterden uzun olamaz.")
                .Matches(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$|^([0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}$")
                .WithMessage("Geçerli bir IP adresi giriniz.");

            RuleFor(x => x.DocumentVersion)
                .GreaterThan(0).WithMessage("Döküman versiyonu 0'dan büyük olmalıdır.");
        }
    }
}
