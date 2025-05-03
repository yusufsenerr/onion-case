using API.Common.Domain.ErrorDetails;
using API.Common.Persistence.Registrations;
using Application.MappingRegistration;
using EKSystemApp.Persistence.DbInitiliazers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using Persistence.Context;
using Persistence.DbInitializerService;
using Persistence.Mapping;
using System.Net;
using System.Reflection;
using WatchDog;
using WatchDog.src.Enums;


var builder = WebApplication.CreateBuilder(args);

builder.Services.MappingAddPersistenceServices(builder.Configuration);
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddControllers();

#region Registration
builder.Services.AddPersistenceServices<ApplicationDbContext>(builder.Configuration,"Admin", "AdminLog");
builder.Services.AddIdentityRegistration<ApplicationDbContext>(builder.Configuration);
builder.Services.AddScoped<IDbInitiliazerContext, DBInitiliazerContext>();

builder.Services.IoCServices(builder.Configuration);
builder.Services.MediaTrMappingAddPersistenceServices(builder.Configuration);
builder.Services.AddSignalR();
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("V1", new OpenApiInfo
{
    Version = "V1",
    Title = "IAR Case",
    Description = "İstanbul Altın Rafinesi"
});
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Name = "TOKEN",
    Description = "Lütfen başında BEARER yazmadan sadece tokenı yazınız!",
    Type = SecuritySchemeType.Http
});
options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                }
            },
            new List < string > ()
        }
    });
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
});


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment() || !app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/V1/swagger.json", "İstanbul Altın Rafinesi API");
    });
}

else
{
    WatchLogger.Log("Exception handling configuration started.");
    app.UseExceptionHandler(appError =>
    {
        appError.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var exceptions = context.Features.Get<IExceptionHandlerFeature>();
            if (exceptions != null)
            {
                // Log the exception details
                WatchLogger.Log($"Exception: {exceptions.Error.Message}, StackTrace: {exceptions.Error.StackTrace}");

                await context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exceptions.Error.Message
                }.ToString());
            }
        });
    });
    WatchLogger.Log("Exception handling configuration completed.");
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
builder.Services.AddHttpContextAccessor();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseWatchDogExceptionLogger();

app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = builder.Configuration.GetSection("Application:defaultSystemAdministratorUserName").Value;
    opt.WatchPagePassword = builder.Configuration.GetSection("Application:defaultSystemAdministratorPassword").Value;
    opt.Blacklist = "/swagger";
    opt.Serializer = WatchDogSerializerEnum.Newtonsoft;
});

app.MapControllers();
app.UseForwardedHeaders();

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitiliazerContext>();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

    await dbInitializer.Initialize(dbContext, configuration);
}


app.Run();
