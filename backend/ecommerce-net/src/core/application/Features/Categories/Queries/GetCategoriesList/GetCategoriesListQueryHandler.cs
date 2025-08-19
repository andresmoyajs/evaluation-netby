using AutoMapper;
using application.Features.Categories.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.Persistence;
using domain;

namespace application.Features.Categories.Queries.GetCategoriesList
{
    internal class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, IReadOnlyList<CategoryVm>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetCategoriesListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IReadOnlyList<CategoryVm>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var categories = await unitOfWork.Repository<Category>().GetAsync(
                null,
                x => x.OrderBy(y => y.Name),
                string.Empty,
                false
             );

            return mapper.Map<IReadOnlyList<CategoryVm>>(categories);
        }
    }
}
