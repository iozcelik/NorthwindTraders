using System.Linq;
using FluentValidation;
using Northwind.Application.Common.Interfaces;

namespace Northwind.Application.Categories.Commands.UpsertCategory {
    public class UpsertCategoryCommandValidator : AbstractValidator<UpsertCategoryCommand> {
        private readonly INorthwindDbContext _context;

        public UpsertCategoryCommandValidator(INorthwindDbContext context) {
            _context = context;

            RuleFor(x => x.Name).MaximumLength(100).NotEmpty().NotNull();
            RuleFor(x => x.Name)
                .Must(UniqueName)
                .WithMessage("Category name must be unique."); ;
        }

        private bool UniqueName(UpsertCategoryCommand category, string name) {
            var dbCategory = _context.Categories
                                .Where(x => x.CategoryName.ToLower() == name.ToLower())
                                .SingleOrDefault();

            if (dbCategory == null)
                return true;

            return dbCategory.CategoryId == category.Id;
        }
    }
}
