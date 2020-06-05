﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Northwind.Application.Common.Interfaces;
using Northwind.Application.Common.Mappings;
using Northwind.Domain.Entities;

namespace Northwind.Application.Categories.Commands.UpsertCategory {
    public class UpsertCategoryCommand : IRequest<int>, IMapFrom<CategoryDto> 
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertCategoryCommand, int>
        {
            private readonly INorthwindDbContext _context;

            public UpsertCategoryCommandHandler(INorthwindDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertCategoryCommand request, CancellationToken cancellationToken)
            {
                Category entity;

                if (request.Id.HasValue)
                {
                    entity = await _context.Categories.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Category();

                    _context.Categories.Add(entity);
                }

                entity.CategoryName = request.Name;
                entity.Description = request.Description;
                entity.Picture = request.Picture;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.CategoryId;
            }
        }

        public void Mapping(Profile profile) {
            profile.CreateMap<CategoryDto, UpsertCategoryCommand>();
        }
    }
}
