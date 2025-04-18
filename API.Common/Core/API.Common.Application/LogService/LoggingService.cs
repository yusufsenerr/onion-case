using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using WatchDog;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using API.Common.Domain.LogModel;

namespace API.Common.Application.Services.LogService;
public class LoggingService( IHttpContextAccessor httpContextAccessor)
{
    readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
    public void LogAction<T>(string tableName, string operation,T data)
    {
        var context = this.httpContextAccessor.HttpContext;
        var name = context?.User?.Identity?.Name;
        var username = context?.User.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;
        var userId = context?.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
        var ip = context.Connection.RemoteIpAddress;
        if (ip != null && ip.IsIPv4MappedToIPv6)
        {
            ip = ip.MapToIPv4();
        }
        var ipAddress = ip?.ToString();
        var pageUrl = context?.Request.Path.Value;

        var logEntry = new CustomLogModel<T>
        {
            UserId = userId,
            PageUrl = pageUrl,
            TableName = tableName,
            CrudOperation = operation,
            IpAddress = ipAddress,
            ActionTime = DateTime.UtcNow,
            Data = data
        };

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = false,
            ReferenceHandler = ReferenceHandler.Preserve // Döngüleri kırmak için
        };

        var logJson = JsonSerializer.Serialize(logEntry, options);
        WatchLogger.Log(logJson);

    }
}