using EntitiesLayer.Concrete.ECommerce.Models.LegalDocuments;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class CompanyInformationValidator : AbstractValidator<CompanyInformation>
    {
        public CompanyInformationValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Şirket adı boş olamaz.")
                .MaximumLength(250).WithMessage("Şirket adı 250 karakterden uzun olamaz.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres boş olamaz.")
                .MaximumLength(500).WithMessage("Adres 500 karakterden uzun olamaz.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                .MaximumLength(20).WithMessage("Telefon numarası 20 karakterden uzun olamaz.")
                .Matches(@"^[0-9\-\+\s\(\)]+$").WithMessage("Geçerli bir telefon numarası giriniz.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(100).WithMessage("E-posta adresi 100 karakterden uzun olamaz.");

            RuleFor(x => x.Website)
                .NotEmpty().WithMessage("Website adresi boş olamaz.")
                .MaximumLength(100).WithMessage("Website adresi 100 karakterden uzun olamaz.")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Geçerli bir website adresi giriniz.");

            RuleFor(x => x.TaxOffice)
                .NotEmpty().WithMessage("Vergi dairesi boş olamaz.")
                .MaximumLength(100).WithMessage("Vergi dairesi 100 karakterden uzun olamaz.");

            RuleFor(x => x.TaxNumber)
                .NotEmpty().WithMessage("Vergi numarası boş olamaz.")
                .MaximumLength(20).WithMessage("Vergi numarası 20 karakterden uzun olamaz.")
                .Matches(@"^[0-9]+$").WithMessage("Vergi numarası sadece rakamlardan oluşmalıdır.");
        }
    }
}
