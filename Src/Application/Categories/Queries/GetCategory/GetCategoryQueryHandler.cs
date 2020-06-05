using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Categories.Queries.GetCategory {
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryDto> {
        private readonly INorthwindDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(INorthwindDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken) {
            var brand = await _context.Categories
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

            return brand;
        }
    }
}
