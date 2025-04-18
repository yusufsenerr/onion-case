using API.Common.Domain.Commons;

namespace API.Common.Domain.LogModel;

public class CustomLogModel<T>
{
    public string UserId { get; set; }  
    public string PageUrl { get; set; } 
    public string TableName { get; set; } 
    public string CrudOperation { get; set; } 
    public string IpAddress { get; set; } 
    public DateTime ActionTime { get; set; }
    public string? UserName { get; set; }   
    public T? Data { get; set; }
}