using AutoMapper;
using application.Features.Products.Queries.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using application.Exceptions;
using application.Persistence;
using domain;

namespace application.Features.Products.Commands.BuyProduct
{
    public class BuyProductCommandHandler : IRequestHandler<BuyProductCommand, ProductVm>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly HttpClient httpClient;

        public BuyProductCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpClientFactory httpClientFactory
            )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpClient = httpClientFactory.CreateClient("TransactionsService");
        }
        public async Task<ProductVm> Handle(BuyProductCommand request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Product, object>>>();
            includes.Add(p => p.Images!);

            var productToUpdate = await unitOfWork.Repository<Product>().GetEntityAsync(
                x => x.Id == request.Id,
                includes,
                true
            );

            if (productToUpdate is null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            if (request.Stock <= 0)
            {
                throw new BadRequestException("La cantidad a comprar debe ser mayor a 0");
            }

            productToUpdate.Stock += request.Stock;
            await unitOfWork.Repository<Product>().UpdateAsync(productToUpdate);

            var subTotal = Math.Round(productToUpdate.Price * request.Stock, 2);
            var taxes = Math.Round(subTotal * Convert.ToDecimal(0.15), 2);
            var total = subTotal + taxes;

            var transactionCommand = new
            {
                ProductId = productToUpdate.Id,
                Type = TransactionType.Compra,
                Tax = taxes,
                Subtotal = subTotal,
                Total = total,
                Quantity = request.Stock,
            };

            var response = await httpClient.PostAsJsonAsync("", transactionCommand, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al registrar la transacción. Status: {response.StatusCode}");
            }
            await unitOfWork.Complete();
            return mapper.Map<ProductVm>(productToUpdate);
        }
    }
}
