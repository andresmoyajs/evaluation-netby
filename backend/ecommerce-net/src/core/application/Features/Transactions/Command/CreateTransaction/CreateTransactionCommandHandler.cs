using AutoMapper;
using application.Features.Transactions.Vms;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using application.Features.Transactions.Command.CreateTransaction;
using application.Persistence;
using domain;

namespace application.Features.Transactions.Command.CreateOrder
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionVm>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateTransactionCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<TransactionVm> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new Transaction
            {
                ProductId = request.ProductId,
                Type = request.Type,
                Tax = request.Tax,
                Subtotal = request.Subtotal,
                Total = request.Total,
                Quantity = request.Quantity,
            };
            
            unitOfWork.Repository<Transaction>().AddEntity(transaction);
            
            var resultado = await unitOfWork.Complete();

            if (resultado <=0)
            {
                throw new Exception("No se pudo ingresar la transaccion de compra");
            }
            return mapper.Map<TransactionVm>(transaction);
        }
    }
}