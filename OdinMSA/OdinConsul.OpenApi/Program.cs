
using Microsoft.OpenApi.Models;
using OdinModels.Constants;
using OdinModels.Core.ConfigModels;
using OdinModels.OdinRedis;
using OdinMSA.Core.Extensions;
using OdinMSA.Core.Models;
using OdinMSA.OdinConsul;
using OdinMSA.OdinConsul.Extensions;
using OdinMSA.OdinLog;
using OdinMSA.OdinLog.Core;
using OdinMSA.OdinLog.Core.Models;
using OdinMSA.OdinRedis;
using OdinMSA.SnowFlake;
using SqlSugar;

var builder = WebApplication.CreateBuilder(args);
builder.BuilderAddJsonFile(new []
{
    new JsonFile(){FilePath = "config/consul.json"}
});
var config = builder.Configuration;
var serverConfig = builder.Configuration.GetSectionModel<ServerConfig>("Server");
var ips = serverConfig.GetAllUrls();
builder.WebHost.UseUrls(
    ips.ToArray()
);
//注入 odinlogs
builder.Services.AddOdinSingletonOdinLogs(config);
// builder.Services.AddOdinSingletonOdinLogs(option =>
// {
//     option.Config = new LogConfig()
//     {
//         ConnectionString = config.GetConnectionString("dbString"),
//         LogSaveType = new EnumLogSaveType[] { EnumLogSaveType.File },
//         DataBaseType = DbType.MySql
//     };
// });
//健康检查
builder.Services.AddHealthChecks();
//注入 httpclient
builder.BuilderAddHttpClient(new HttpClientInfo()
{
    ClientName = "default",
});
//注入 webapi
builder.Services.AddSingletonMsaWebApi();
//注入 consul  restTemplate
builder.Services.AddSingletonMsaRestTemplate();
//注入 雪花ID
builder.Services.AddSingletonSnowFlake(1, 1);
//注入 redis 
builder.Services.AddTransientMsaRedis(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});
var app = builder.Build();
var configuration = app.Configuration;
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Configure the HTTP request pipeline.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseHealthChecks(SystemConstant.KEY_OF_HEALTH_CHECK_PATH);
app.RegisterConsul(app.Lifetime, configuration,serverConfig);
app.Run();