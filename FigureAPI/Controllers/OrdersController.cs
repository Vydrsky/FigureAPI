using Figure.Core._Queries.Order;
using Figure.Core.Handlers.Order;
using Figure.Core.Models.Order;
using Figure.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Figure.API.Controllers;
[Route("api/figureapi")]
[ApiController]
public class OrdersController : ControllerBase {
	private APIResponse _response;

	public OrdersController() {
		_response = new();
	}

	[HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetAllOrders(
		[FromServices] IQueryHandler<GetAllOrdersQuery,IEnumerable<ReadOrderModel>> handler,
		int pageSize,
		int pageNumber,
		CancellationToken cancellationToken) {

		try {
			var result = await handler.Handle(GetAllOrdersQuery.With(pageSize, pageNumber),cancellationToken);
			_response.PrepForSuccess(result);
			return Ok(_response);
		}
		catch(Exception e) {
			_response.PrepForException(e);
			return _response;
		}
	}

	[HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> GetOrder(
		[FromServices] IQueryHandler<GetOrderQuery, ReadOrderModel> handler,
		[FromRoute]Guid id,
		CancellationToken cancellationToken) {
		try {
			var result = await handler.Handle(GetOrderQuery.With(id), cancellationToken);
			if(result == null) {
				_response.PrepForNotFound("Couldn't find the order in the database.");
				return NotFound(_response);
			}
			_response.PrepForSuccess(result);
			return Ok(_response);
		}
		catch(Exception e) {
            _response.PrepForException(e);
            return _response;
        }
	}
}
