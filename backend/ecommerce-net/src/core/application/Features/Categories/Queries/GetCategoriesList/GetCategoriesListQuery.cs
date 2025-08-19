using application.Features.Categories.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<IReadOnlyList<CategoryVm>>
    {
    }
}
