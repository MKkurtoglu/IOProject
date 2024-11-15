using EntitiesLayer.Concrete.ECommerce.Models.LegalDocuments;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class LegalDocumentTemplateValidator : AbstractValidator<LegalDocumentTemplate>
    {
        public LegalDocumentTemplateValidator()
        {
            RuleFor(x => x.DocumentType)
                .IsInEnum().WithMessage("Geçerli bir döküman tipi seçiniz.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz.")
                .MaximumLength(250).WithMessage("Başlık 250 karakterden uzun olamaz.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("İçerik boş olamaz.");

            RuleFor(x => x.Version)
                .GreaterThan(0).WithMessage("Versiyon numarası 0'dan büyük olmalıdır.");

            RuleFor(x => x.ValidTo)
                .GreaterThan(x => x.ValidFrom)
                .When(x => x.ValidTo.HasValue && x.ValidFrom.HasValue)
                .WithMessage("Geçerlilik bitiş tarihi başlangıç tarihinden sonra olmalıdır.");
        }
    }
}
