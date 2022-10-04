using System.Net;

namespace Figure.DataAccess;
internal class APIResponse {
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public List<string> ErrorMessages { get; set; } = new List<string>();
    public object? Content { get; set; }
}
