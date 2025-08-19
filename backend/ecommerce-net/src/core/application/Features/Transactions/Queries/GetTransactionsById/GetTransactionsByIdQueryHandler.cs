using AutoMapper;
using application.Features.Transactions.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using application.Features.Transactions.Queries.GetTransactionsById;
using application.Persistence;
using domain;

namespace application.Features.Transactions.Queries.GetTransactionsById
{
    public class GetTransactionsByIdQueryHandler : IRequestHandler<GetTransactionsByIdQuery, TransactionVm>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetTransactionsByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<TransactionVm> Handle(GetTransactionsByIdQuery request, CancellationToken cancellationToken)
        {

            var transaction = await unitOfWork.Repository<Transaction>().GetEntityAsync(
                x => x.Id == request.TransactionId,
                null,
                false
            );

            return mapper.Map<TransactionVm>(transaction);
        }
    }
}
