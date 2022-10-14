using System.Net;

namespace Figure.Infrastructure;
public class APIResponse {
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; } = true;
    public List<string> ErrorMessages { get; set; } = new List<string>();
    public object? Content { get; set; }

    public void PrepForException(Exception e) {
        IsSuccess = false;
        ErrorMessages = new List<string> { e.Message };
    }
    public void PrepForSuccess(object? content) {
        IsSuccess = true;
        ErrorMessages = new List<string>();
        StatusCode = HttpStatusCode.OK;
        Content = content;
    }

    public void PrepForNoContent() {
        IsSuccess = true;
        ErrorMessages = new List<string>();
        StatusCode = HttpStatusCode.NoContent;
        Content = null;
    }

    public void PrepForCreated(Guid Id) {
        IsSuccess = true;
        ErrorMessages = new List<string>();
        StatusCode = HttpStatusCode.Created;
        Content = Id;
    }

    public void PrepForBadRequest(List<string> errors) {
        IsSuccess = false;
        ErrorMessages = errors;
        StatusCode = HttpStatusCode.BadRequest;
        Content = null;
    }

    public void PrepForNotFound(string notFoundMessage) {
        IsSuccess = false;
        ErrorMessages = new List<string> { notFoundMessage };
        StatusCode = HttpStatusCode.NotFound;
        Content = null;
    }
}
