using Figure.Application._Commands.Order;
using Figure.Application.Exceptions;
using Figure.Core._Queries.Order;
using Figure.Core.Models.Order;
using Figure.DataAccess.Entities;
using Figure.Infrastructure;
using Microsoft.AspNetCore.JsonPatch;
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
		[FromServices] IQueryHandler<GetAllOrdersQuery, IEnumerable<ReadOrderModel>> handler,
		int pageSize,
		int pageNumber,
		CancellationToken cancellationToken) {

		try {
			var result = await handler.Handle(GetAllOrdersQuery.With(pageSize, pageNumber), cancellationToken);
			_response.PrepForSuccess(result);
			return Ok(_response);
		}
		catch (Exception e) {
			_response.PrepForException(e);
			return _response;
		}
	}

    [HttpGet("archive")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetArchivedOrders(
        [FromServices] IQueryHandler<GetArchivedOrdersQuery, IEnumerable<ReadOrderModel>> handler,
        int pageSize,
        int pageNumber,
        CancellationToken cancellationToken) {

        try {
            var result = await handler.Handle(GetArchivedOrdersQuery.With(pageSize, pageNumber), cancellationToken);
            _response.PrepForSuccess(result);
            return Ok(_response);
        }
        catch (Exception e) {
            _response.PrepForException(e);
            return _response;
        }
    }

    [HttpGet("dearchive")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetNotArchivedOrders(
        [FromServices] IQueryHandler<GetNotArchivedOrdersQuery, IEnumerable<ReadOrderModel>> handler,
        int pageSize,
        int pageNumber,
        CancellationToken cancellationToken) {

        try {
            var result = await handler.Handle(GetNotArchivedOrdersQuery.With(pageSize, pageNumber), cancellationToken);
            _response.PrepForSuccess(result);
            return Ok(_response);
        }
        catch (Exception e) {
            _response.PrepForException(e);
            return _response;
        }
    }

    [HttpGet("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<APIResponse>> GetOrder(
		[FromServices] IQueryHandler<GetOrderQuery, ReadOrderModel> handler,
		[FromRoute] Guid id,
		CancellationToken cancellationToken) {
		try {
			var result = await handler.Handle(GetOrderQuery.With(id), cancellationToken);
			if (result == null) {
				_response.PrepForNotFound("Couldn't find the order in the database.");
				return NotFound(_response);
			}
			_response.PrepForSuccess(result);
			return Ok(_response);
		}
		catch (Exception e) {
			_response.PrepForException(e);
			return _response;
		}
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<APIResponse>> PostOrder(
		[FromServices] ICommandHandler<PostOrderCommand> handler,
		[FromBody] PostOrderCommand command,
		CancellationToken cancellationToken) {
		try {
			if (command == null) {
				_response.PrepForBadRequest(new List<string> { "Command is empty" });
				return _response;
			}
			await handler.Handle(command, cancellationToken);
			_response.PrepForSuccess(null);
			return Ok(_response);
		}
		catch (Exception e) {
			_response.PrepForException(e);
			return _response;
		}

	}

	[HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> UpdateOrder(
		[FromServices] ICommandHandler<UpdateOrderCommand> handler,
		[FromBody] UpdateOrderCommand command,
		CancellationToken cancellationToken) {
		try {
			if (command == null) {
				_response.PrepForBadRequest(new List<string> { "Command is empty" });
				return _response;
			}
			await handler.Handle(command, cancellationToken);
			_response.PrepForNoContent();
			return Ok(_response);
		}
		catch (NotFoundException nfe) {
			_response.PrepForNotFound(nfe.Message);
			return _response;
		}
		catch (Exception e) {
			_response.PrepForException(e);
			return _response;
		}
	}

	[HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<APIResponse>> DeleteOrder(
		[FromServices] ICommandHandler<DeleteOrderCommand> handler,
		[FromRoute] Guid id,
		CancellationToken cancellationToken) {
		try {
			await handler.Handle(DeleteOrderCommand.With(id), cancellationToken);
			_response.PrepForNoContent();
			return Ok(_response);
		}
		catch (NotFoundException nfe) {
			_response.PrepForNotFound(nfe.Message);
			return _response;
		}
		catch (Exception e) {
			_response.PrepForException(e);
			return _response;
		}
	}

	[HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> PatchOrder(
		[FromServices] ICommandHandler<PatchOrderCommand> handler,
		[FromRoute] Guid id,
		[FromBody] JsonPatchDocument<Order> jsonPatchDocument,
		CancellationToken cancellationToken) {
        try {
			if(jsonPatchDocument == null) {
                _response.PrepForBadRequest(new List<string>() { "Patch document must not be empty" });
                return _response;
            }
			PatchOrderCommand command = new(Id: id, JsonPatchDocument: jsonPatchDocument);
            await handler.Handle(command, cancellationToken);
            _response.PrepForNoContent();
            return Ok(_response);
        }
        catch (NotFoundException nfe) {
            _response.PrepForNotFound(nfe.Message);
            return _response;
        }
        catch (Exception e) {
            _response.PrepForException(e);
            return _response;
        }
    }

	[HttpPatch("archive/{id:guid}")]
    public async Task<ActionResult<APIResponse>> ArchiveOrder(
        [FromServices] ICommandHandler<ArchiveOrderCommand> handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken) {
        try {
			await handler.Handle(ArchiveOrderCommand.With(id:id), cancellationToken);
            _response.PrepForSuccess(null);
            return Ok(_response);
        }
        catch (NotFoundException nfe) {
            _response.PrepForNotFound(nfe.Message);
            return _response;
        }
        catch (Exception e) {
            _response.PrepForException(e);
            return _response;
        }
    }

    [HttpPatch("dearchive/{id:guid}")]
    public async Task<ActionResult<APIResponse>> DeArchiveOrder(
        [FromServices] ICommandHandler<DeArchiveOrderCommand> handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken) {
        try {
            await handler.Handle(DeArchiveOrderCommand.With(id: id), cancellationToken);
            _response.PrepForSuccess(null);
            return Ok(_response);
        }
        catch (NotFoundException nfe) {
            _response.PrepForNotFound(nfe.Message);
            return _response;
        }
        catch (Exception e) {
            _response.PrepForException(e);
            return _response;
        }
    }
}
