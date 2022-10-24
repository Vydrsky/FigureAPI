using Figure.Application._Commands.Figure;
using Figure.Application._Commands.Order;
using Figure.Application._Queries.Figure;
using Figure.Application.Exceptions;
using Figure.Application.Handlers.Figure.CommandHandlers;
using Figure.Application.Models;
using Figure.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Figure.API.Controllers;

[Route("api/figures")]
[ApiController]
public class FigureController : ControllerBase {
    private APIResponse _response;

    public FigureController() {
        _response = new();
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetAllFigures(
    [FromServices] IQueryHandler<GetAllFiguresQuery, IEnumerable<ReadFigureModel>> handler,
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    CancellationToken cancellationToken) {
        try {
            var result = await handler.Handle(GetAllFiguresQuery.With(pageSize, pageNumber), cancellationToken);
            _response.PrepForSuccess(result);
        }
        catch (Exception e) {
            _response.PrepForException(e);
        }
        return _response;
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> GetFigure(
    [FromServices] IQueryHandler<GetFigureQuery, ReadFigureModel> handler,
    [FromRoute] Guid id,
    CancellationToken cancellationToken) {
        try {
            var result = await handler.Handle(GetFigureQuery.With(id), cancellationToken);
            if (result == null) {
                _response.PrepForNotFound("Couldn't find the figure in the database.");
                return _response;
            }
            _response.PrepForSuccess(result);
        }
        catch (Exception e) {
            _response.PrepForException(e);
        }
        return _response;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> PostFigure(
        [FromServices] ICommandHandler<PostFigureCommand> handler,
        [FromBody] PostFigureCommand command,
        CancellationToken cancellationToken) {
        try {
            if (command == null) {
                _response.PrepForBadRequest(new List<string> { "Command is empty" });
                return _response;
            }
            await handler.Handle(command, cancellationToken);
            _response.PrepForCreated((handler as PostFigureCommandHandler).GetCreatedId());
        }
        catch (Exception e) {
            _response.PrepForException(e);
        }
        return _response;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> UpdateFigure(
    [FromServices] ICommandHandler<UpdateFigureCommand> handler,
    [FromBody] UpdateFigureCommand command,
    CancellationToken cancellationToken) {
        try {
            if (command == null) {
                _response.PrepForBadRequest(new List<string> { "Command is empty" });
                return _response;
            }
            await handler.Handle(command, cancellationToken);
            _response.PrepForNoContent();
        }
        catch (NotFoundException nfe) {
            _response.PrepForNotFound(nfe.Message);
        }
        catch (Exception e) {
            _response.PrepForException(e);
        }
        return _response;
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> DeleteFigure(
        [FromServices] ICommandHandler<DeleteOrderCommand> handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken) {
        try {
            await handler.Handle(DeleteOrderCommand.With(id), cancellationToken);
            _response.PrepForNoContent();
        }
        catch (NotFoundException nfe) {
            _response.PrepForNotFound(nfe.Message);
        }
        catch (Exception e) {
            _response.PrepForException(e);
        }
        return _response;
    }
}
