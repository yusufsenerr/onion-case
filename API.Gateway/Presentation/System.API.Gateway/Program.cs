using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Registrations

#endregion

builder.Services.AddSwaggerGen();

#region Ocelot
builder.Services.AddOcelot();
#endregion

#region Docker

#endregion

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Log User Panel

#endregion

//app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
try
{
    app.UseOcelot().Wait();
}
catch (Exception ex)
{
    WatchLogger.Log(ex.Message);
    WatchLogger.Log(ex.InnerException?.Message);
}
app.Run();
