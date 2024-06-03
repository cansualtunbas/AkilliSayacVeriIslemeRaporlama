
using AkilliSayac.Services.Report.Consumers;
using AkilliSayac.Services.Report.Dtos;
using AkilliSayac.Services.Report.Services;
using AkilliSayac.Services.Report.Settings;
using AkilliSayac.Shared.Classes;
using AkilliSayac.Shared.Enums;
using AkilliSayac.Shared.Services;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ReportStatusChangedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        //dockerize edilseydi burdaki adres dockerize adresi olacakti
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
        cfg.ReceiveEndpoint(RabbitMQSettingsConst.ReportChangedEventQueueName, e =>
        {
            e.ConfigureConsumer<ReportStatusChangedEventConsumer>(context);
        });
    });

});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_report";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});


var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    var serviceProvider = scope.ServiceProvider;

//    var counterService = serviceProvider.GetRequiredService<IReportService>();

//    if (!(await counterService.GetAllAsync()).Data.Any())
//    {
//        await counterService.CreateAsync(new ReportDto { Id= Guid.NewGuid().ToString(), RequestedDate=DateTime.Now, ReportStatus=0,CounterSerialNumber= "12345678", Counter = { }, CreatedTime=DateTime.Now });
      

  
//    }
//}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    IdentityModelEventSource.ShowPII = true;
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();















