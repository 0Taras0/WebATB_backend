using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Domain;
using Core.Models.Category;

namespace Core.Validators.Category;

public class CategoryUpdateValidator : AbstractValidator<CategoryEditModel>
{
    public CategoryUpdateValidator(AppDbAtbContext db)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustAsync(async (id, cancellation) =>
                await db.Categories.AnyAsync(c => c.Id == id, cancellation))
            .WithMessage("Категорію не знайдено.")
            .WithName("Id");

        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Назва є обов'язковою")
            .MaximumLength(250).WithMessage("Назва повинна містити не більше 250 символів")
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Назва не може бути порожньою або null")
            .MustAsync(async (model, name, cancellation) =>
                !await db.Categories
                    .AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != model.Id && !c.IsDeleted, cancellation))
            .WithMessage("Інша категорія з таким іменем вже існує");

        RuleFor(x => x.Slug)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Слаг є обов'язковим")
            .MaximumLength(250).WithMessage("Слаг повинен містити не більше 250 символів")
            .MustAsync(async (model, slug, cancellation) =>
            {
                var normalized = slug.Trim().ToLower().Replace(" ", "-");
                return !await db.Categories
                    .AnyAsync(c => c.Slug == normalized && c.Id != model.Id && !c.IsDeleted, cancellation);
            })
            .WithMessage("Інша категорія з таким слагом вже існує");
    }
}
