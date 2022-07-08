using OdinModels.Constants;
using OdinModels.OdinConsul;
using OdinModels.OdinUtils.Utils.OdinNet;
using OdinMSA.Core;
using OdinMSA.OdinConsul.Extensions;

var builder = WebApplication.CreateBuilder(args);
var ip = GetStartUrls(builder.Configuration);
builder.Host.ConfigureAppConfiguration((context, build) =>
{
    build.AddJsonFile("config/consul.config.json",optional: true,reloadOnChange: true);
});
builder.WebHost.ConfigureKestrel(((context, options) =>
{
} )).UseKestrel().UseUrls(ip);
builder.Services.AddHealthChecks();
// Add services to the container.
Console.WriteLine(builder.Configuration.GetSectionValue<string>("Service:ServerIp"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
var configuration = app.Configuration;
app.UseHealthChecks(SystemConstant.KEY_OF_HEALTH_CHECK_PATH);
var configIp = configuration.GetSectionValue<string>("Service:ServerIp");
var ips = NetworkHelper.GetHostIpForFas();
var serverIp = string.IsNullOrEmpty(configIp) ? (ips != null ? ips[0] : "127.0.0.1") : configIp;
ServiceEntity serviceEntity = new ServiceEntity
{
    ServerProtocol = configuration.GetSectionValue<string>("Service:ServerProtocol"),
    ServerIP = serverIp,
    ServerPort = configuration.GetSectionValue<int>("Service:ServerPort"),
    ServiceName = configuration.GetSectionValue<string>("Service:ServiceName"),
    DeregisterCriticalServiceAfter = configuration.GetSectionValue<int>("Service:ServiceDeregisterCriticalServiceAfter"),
    HealInterval = configuration.GetSectionValue<int>("Service:ServiceHealInterval"),
    TimeOut = configuration.GetSectionValue<int>("Service:ServiceTimeOut"),
    ConsulProtocol = configuration.GetSectionValue<string>("Consul:Protocol"),
    ConsulIP = configuration.GetSectionValue<string>("Consul:IP"),
    ConsulPort = configuration.GetSectionValue<int>("Consul:Port"),
};
app.RegisterConsul(app.Lifetime, serviceEntity);
app.Run();

static string[] GetStartUrls(ConfigurationManager configuration)
{
    string protocol = configuration.GetSectionValue<string>("Service:ServerProtocol");
    string ip = configuration.GetSectionValue<string>("Service:ServerIp");
    string port = configuration.GetSectionValue<string>("Service:ServerPort");
    return new string[]{$"{protocol}://{ip}:{port}"};
}