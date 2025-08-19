using AutoMapper;
using application.Features.Products.Queries.Vms;
using MediatR;
using System.Net.Http.Json;
using application.Exceptions;
using application.Persistence;
using domain;

namespace application.Features.Products.Commands.SellProduct
{
    public class SellProductCommandHandler : IRequestHandler<SellProductCommand, ProductVm>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly HttpClient httpClient;

        public SellProductCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpClientFactory httpClientFactory
            )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpClient = httpClientFactory.CreateClient("TransactionsService");

        }
        public async Task<ProductVm> Handle(SellProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = await unitOfWork.Repository<Product>().GetByIdAsync(request.Id);
            if (productToUpdate is null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            if (request.Stock <= 0)
            {
                throw new BadRequestException("La cantidad a vender debe ser mayor a 0");
            }

            if (request.Stock > productToUpdate.Stock)
            {
                throw new BadRequestException($"No puedes vender más del stock disponible ({productToUpdate.Stock})");
            }

            productToUpdate.Stock -= request.Stock;
            await unitOfWork.Repository<Product>().UpdateAsync(productToUpdate);

            var subTotal = Math.Round(productToUpdate.Price * request.Stock, 2);
            var taxes = Math.Round(subTotal * Convert.ToDecimal(0.15), 2);
            var total = subTotal + taxes;

            var transactionCommand = new
            {
                ProductId = productToUpdate.Id,
                Type = TransactionType.Venta,
                Tax = taxes,
                Subtotal = subTotal,
                Total = total,
                Quantity = request.Stock,
            };
            
            // Llamar al microservicio de transacciones
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
