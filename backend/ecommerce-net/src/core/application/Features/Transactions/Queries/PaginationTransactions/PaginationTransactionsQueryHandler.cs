using AutoMapper;
using application.Features.Transactions.Vms;
using application.Features.Products.Queries.Vms;
using application.Features.Shared.Queries;
using application.Specifications.Transactions;
using application.Specifications.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.Persistence;
using domain;

namespace application.Features.Transactions.Queries.PaginationTransactions
{
    public class PaginationTransactionsQueryHandler : IRequestHandler<PaginationTransactionsQuery, PaginationVm<TransactionVm>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PaginationTransactionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<PaginationVm<TransactionVm>> Handle(PaginationTransactionsQuery request, CancellationToken cancellationToken)
        {
            var orderSpecificationParams = new TransactionSpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Search = request.Search,
                Sort = request.Sort,
                Id = request.Id,
                Username = request.Username
            };

            var spec = new TransactionSpecification(orderSpecificationParams);
            var orders = await unitOfWork.Repository<Transaction>().GetAllWithSpec(spec);

            var specCount = new TransactionForCountingSpecification(orderSpecificationParams);
            var totalTransactions = await unitOfWork.Repository<Transaction>().CountAsync(specCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalTransactions) / Convert.ToDecimal(request.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var data = mapper.Map<IReadOnlyList<TransactionVm>>(orders);

            var productsByPage = orders.Count();

            var pagination = new PaginationVm<TransactionVm>
            {
                Count = totalTransactions,
                Data = data,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = productsByPage,

            };

            return pagination;
        }
    }
}
