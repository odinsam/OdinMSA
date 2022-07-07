using System.Configuration;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OdinMSA.OdinEF;
using OdinMSA.OdinLog;
using OdinMSA.OdinLog.Core;
using OdinMSA.OdinLog.Core.Models;
using OdinMSA.SnowFlake;
using OdinPush.SignalrPush;
using OdinPush.SignalrPush.SignalRServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("OdinPush");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISignalREventMonitorService, SignalREventMonitorService>();
builder.Services.AddSingletonSqlSugar(builder.Configuration,"OdinPush");
builder.Services.AddOdinSingletonOdinLogs(opt=>
    opt.Config=new LogConfig {
        LogSaveType=new EnumLogSaveType[]{EnumLogSaveType.All},
        ConnectionString = connectionString});
builder.Services.AddSignalR();
builder.Services.AddSingletonSnowFlake(1, 1);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();
app.MapHub<OdinSignalR>("/OdinSignalR");
app.MapControllers();

app.Run();