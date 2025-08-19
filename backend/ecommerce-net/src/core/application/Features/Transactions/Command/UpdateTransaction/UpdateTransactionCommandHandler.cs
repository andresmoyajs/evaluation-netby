using AutoMapper;
using application.Features.Transactions.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.Features.Transactions.Command.UpdateTransaction;
using application.Persistence;
using domain;

namespace application.Features.Transactions.Command.UpdateTransactions
{
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, TransactionVm>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateTransactionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<TransactionVm> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {

            var order = await unitOfWork.Repository<Transaction>().GetByIdAsync(request.TransactionId);
            unitOfWork.Repository<Transaction>().UpdateEntity(order);

            var resultado = await unitOfWork.Complete();

            if (resultado <= 0)
            {
                throw new Exception("No se pudo actualizar el status de la orden de compra");
            }

            return mapper.Map<TransactionVm>(order);
        }
    }
}
