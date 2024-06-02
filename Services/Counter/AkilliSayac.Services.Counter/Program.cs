

using AkilliSayac.Services.Counter.Services;
using AkilliSayac.Services.Counter.Settings;
using AkilliSayac.Shared.Services;
using MassTransit;
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


//builder.Services.AddMassTransit(x =>
//{
   
//    // Default Port : 5672
//    x.UsingRabbitMq((context, cfg) =>
//    {

//        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
//        {
//            host.Username("guest");
//            host.Password("guest");
//        });
//    });
//});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_counter";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<ICounterService, CounterService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});


var app = builder.Build();

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















