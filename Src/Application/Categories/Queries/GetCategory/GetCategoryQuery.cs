using MediatR;

namespace Northwind.Application.Categories.Queries.GetCategory {
    public class GetCategoryQuery : IRequest<CategoryDto> {
        public long Id { get; set; }
    }
}
