using API.Common.Persistence.Registrations;
using Application.MappingRegistration;
using Microsoft.AspNetCore.HttpOverrides;
using Persistence.Context;
using Persistence.Mapping;
using WatchDog;


var builder = WebApplication.CreateBuilder(args);

builder.Services.MappingAddPersistenceServices(builder.Configuration);
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddControllers();

#region Registration
builder.Services.AddPersistenceServices<ApplicationDbContext,WatchDogDbContext>(builder.Configuration,"Admin", "AdminLog");
builder.Services.AddIdentityRegistration<ApplicationDbContext>(builder.Configuration);
builder.Services.IoCServices(builder.Configuration);
builder.Services.MediaTrMappingAddPersistenceServices(builder.Configuration);
builder.Services.AddSignalR();

#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseWebSockets();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication(); 
app.UseAuthorization();  
app.UseCors("CorsPolicy");

app.UseWatchDogExceptionLogger();

// app.UseMiddleware<MenuMiddleware>();
app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = "admin";
    opt.WatchPagePassword = "Qwerty@123";
});

app.MapControllers();
app.UseForwardedHeaders();
app.Run();