using Figure.Core.Handlers;
using Figure.Core.Handlers.Order;
using Figure.Core.Models.Order;
using Figure.Core.Queries;
using Figure.DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Figure.API.Controllers;
[Route("api/figureapi")]
[ApiController]
public class OrdersController : ControllerBase {
	private APIResponse _response;
	private readonly IOrdersRepository _ordersRepository;

	public OrdersController(IOrdersRepository ordersRepository) {
		_ordersRepository = ordersRepository;
		_response = new();
	}

	[HttpGet]
	public async Task<ActionResult<APIResponse>> GetAllOrders(
		[FromServices] IQueryHandler<GetAllOrdersQuery,IEnumerable<ReadOrderModel>> handler,
		int pageSize,
		int pageNumber) {

		var orders = await handler.Handle(GetAllOrdersQuery.With(pageSize,pageNumber));
		_response.StatusCode = System.Net.HttpStatusCode.OK;
		_response.Content = orders;
		return Ok(_response);
	}
}
