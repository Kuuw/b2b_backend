using Entities.DTO;
using FluentValidation;

namespace PL.FluentValidation
{
    public class ProductGetPagedDtoValidator : AbstractValidator<ProductGetPagedDto>
    {
        public ProductGetPagedDtoValidator()
        {
            RuleFor(model => model.Page).GreaterThan(0).WithMessage("Sayfa numarası 0'dan büyük olmalıdır.");
            RuleFor(model => model.PageSize).GreaterThan(0).WithMessage("Sayfa boyutu 0'dan büyük olmalıdır.");
            RuleFor(model => model.PageSize).LessThan(30).WithErrorCode("PageSizeLessThan30").WithMessage("Sayfa boyutu 30'dan küçük olmalıdır.");
        }
    }
}