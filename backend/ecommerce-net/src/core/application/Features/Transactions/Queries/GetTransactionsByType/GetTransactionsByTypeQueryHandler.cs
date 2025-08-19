using AutoMapper;
using application.Features.Transactions.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using application.Persistence;
using domain;

namespace application.Features.Transactions.Queries.GetTransactionsByType
{
    public class GetTransactionsByTypeQueryHandler : IRequestHandler<GetTransactionsByTypeQuery, TransactionVm>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetTransactionsByTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<TransactionVm> Handle(GetTransactionsByTypeQuery request, CancellationToken cancellationToken)
        {

            var transaction = await unitOfWork.Repository<Transaction>().GetEntityAsync(
                x => x.Type == request.Type,
                null,
                false
            );

            return mapper.Map<TransactionVm>(transaction);
        }
    }
}
