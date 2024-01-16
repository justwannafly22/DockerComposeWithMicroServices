using System.Net;

namespace Shared;

public class BaseResponseModel
{
    public string? Message { get; set; }
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
}
