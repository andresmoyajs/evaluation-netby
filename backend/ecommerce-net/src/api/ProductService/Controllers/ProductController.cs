using System.Net;
using application.Contracts.Infrastructure;
using application.Features.Products.Commands.BuyProduct;
using application.Features.Products.Commands.ChangeStatusProduct;
using application.Features.Products.Commands.CreateProduct;
using application.Features.Products.Commands.DeleteProduct;
using application.Features.Products.Commands.SellProduct;
using application.Features.Products.Commands.UpdateProduct;
using application.Features.Products.Queries.GetProductById;
using application.Features.Products.Queries.GetProductList;
using application.Features.Products.Queries.PaginationProduct;
using application.Features.Products.Queries.Vms;
using application.Features.Shared.Queries;
using application.Models.ImageManagment;
using domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IManageImageService manageImageService;

        public ProductController(IMediator mediator, IManageImageService manageImageService)
        {
            this.mediator = mediator;
            this.manageImageService = manageImageService;
        }

        [AllowAnonymous]
        [HttpGet("list", Name = "GetProductList")]
        [ProducesResponseType(typeof(IReadOnlyList<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductList()
        {
            var query = new GetProductListQuery();

            var productos = await mediator.Send(query);

            return Ok(productos);
        }

        [AllowAnonymous]
        [HttpGet("pagination", Name = "PaginationProduct")]
        [ProducesResponseType(typeof(PaginationVm<ProductVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationVm<ProductVm>>> PaginationProduct([FromQuery] PaginationProductQuery paginationProductQuery)
        {
            paginationProductQuery.Status = ProductStatus.Activo;
            var paginationProduct = await mediator.Send(paginationProductQuery);
            return Ok(paginationProduct);
        }



        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> GetProductById(int id)
        {
            var query = new GetProductByIdQuery(id);
            return Ok(await mediator.Send(query));
        }

        [HttpPost("create", Name = "CreateProduct")]
        [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> CreateProduct([FromForm] CreateProductCommand request)
        {
            var listFotoUrls = new List<CreateProductImageCommand>();
            if (request.Fotos is not null)
            {
                foreach (var foto in request.Fotos)
                {
                    var resultImage = await manageImageService.UploadImage(new ImageData
                    {
                        ImageStream = foto.OpenReadStream(),
                        Nombre = foto.Name
                    });

                    var fotoCommand = new CreateProductImageCommand
                    {
                        PublicCode = resultImage.PublicId,
                        Url = resultImage.Url,
                    };

                    listFotoUrls.Add(fotoCommand);
                }
                request.ImageUrls = listFotoUrls;
            }

            return await mediator.Send(request);
        }


        [HttpPut("update", Name = "UpdateProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> UpdateProduct([FromForm] UpdateProductCommand request)
        {
            var listFotoUrls = new List<CreateProductImageCommand>();
            if (request.Fotos is not null)
            {
                foreach (var foto in request.Fotos)
                {
                    var resultImage = await manageImageService.UploadImage(new ImageData
                    {
                        ImageStream = foto.OpenReadStream(),
                        Nombre = foto.Name
                    });

                    var fotoCommand = new CreateProductImageCommand
                    {
                        PublicCode = resultImage.PublicId,
                        Url = resultImage.Url,
                    };

                    listFotoUrls.Add(fotoCommand);
                }
                request.ImageUrls = listFotoUrls;
            }

            return await mediator.Send(request);
        }

        [HttpDelete("status/{id}", Name = "UpdateStatusProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> UpdateStatusProduct(int id)
        {
            var request = new ChangeStatusProductCommand(id);
            return await mediator.Send(request);
        }
        
        
        [HttpDelete("{id}", Name = "DeleteProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> DeleteProduct(int id)
        {
            var request = new DeleteProductCommand(id);
            return await mediator.Send(request);
        }
        
        
        [HttpPost("buy", Name = "BuyProduct")]
        [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> BuyProduct([FromForm] BuyProductCommand request)
        {
            return await mediator.Send(request);
        }
        
        [HttpPost("sell", Name = "SellProduct")]
        [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> SellProduct([FromForm] SellProductCommand request)
        {
            return await mediator.Send(request);
        }

    }