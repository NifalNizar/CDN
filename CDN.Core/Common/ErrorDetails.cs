using System.Net;
using System.Text.Json;

namespace CDN.Core.Common;
public class ErrorDetails(HttpStatusCode StatusCode, string Message)
{
    public HttpStatusCode StatusCode { get; set; } = StatusCode;
    public string Message { get; set; } = Message;
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
