using AutoMapper;
using application.Features.Transactions.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using application.Features.Transactions.Queries.GetTransactionsByProductId;
using application.Persistence;
using domain;

namespace application.Features.Transactions.Queries.GetTransactionsByProductId
{
    public class GetTransactionsByProductIdQueryHandler : IRequestHandler<GetTransactionsByProductIdQuery, IReadOnlyList<TransactionVm>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetTransactionsByProductIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IReadOnlyList<TransactionVm>> Handle(GetTransactionsByProductIdQuery request, CancellationToken cancellationToken)
        {
            var transaction = await unitOfWork.Repository<Transaction>().GetAsync(
                x => x.ProductId == request.ProductId,
                x => x.OrderBy(y => y.CreatedDate)
            );
            return mapper.Map<IReadOnlyList<TransactionVm>>(transaction);
        }
    }
}
