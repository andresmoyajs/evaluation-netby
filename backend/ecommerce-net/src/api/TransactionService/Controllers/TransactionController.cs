using System.Net;
using application.Features.Transactions.Command.CreateTransaction;
using application.Features.Transactions.Command.UpdateTransaction;
using application.Features.Transactions.Queries.GetTransactionsById;

using application.Features.Transactions.Vms;
using application.Features.Shared.Queries;
using application.Features.Transactions.Queries.GetTransactionsByProductId;
using application.Features.Transactions.Queries.GetTransactionsByType;
using application.Features.Transactions.Queries.PaginationTransactions;
using domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TransactionService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly IMediator mediator;

    public TransactionController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpPost(Name = "CreateTransaction")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<TransactionVm>> CreateTransaction([FromBody] CreateTransactionCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPut(Name = "UpdateTransaction")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<TransactionVm>> UpdateTransaction([FromBody] UpdateTransactionCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpGet("{id}", Name = "GetTransactionById")]
    [ProducesResponseType(typeof(TransactionVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<TransactionVm>> GetTransactionById(int id)
    {
        var query = new GetTransactionsByIdQuery(id);
        return Ok(await mediator.Send(query));
    }
    
    [HttpGet("product/{id}", Name = "GetTransactionByProductId")]
    [ProducesResponseType(typeof(TransactionVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<TransactionVm>> GetTransactionByProductId(int id)
    {
        var query = new GetTransactionsByProductIdQuery(id);
        return Ok(await mediator.Send(query));
    }
    
    [HttpGet("type/{id}", Name = "GetTransactionByType")]
    [ProducesResponseType(typeof(TransactionVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<TransactionVm>> GetTransactionByType(TransactionType id)
    {
        var query = new GetTransactionsByTypeQuery(id);
        return Ok(await mediator.Send(query));
    }
    
    [HttpGet("paginationByUsername", Name = "PaginationByUsername")]
    [ProducesResponseType(typeof(PaginationVm<TransactionVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<TransactionVm>>> PaginationByUsername([FromQuery] PaginationTransactionsQuery paginationTransactionsParams)
    {
        return Ok(await mediator.Send(paginationTransactionsParams));
    }
    


}