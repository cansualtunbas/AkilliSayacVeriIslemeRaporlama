

using AkilliSayac.Services.Counter.Dtos;
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
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var counterService = serviceProvider.GetRequiredService<ICounterService>();

    if (!(await counterService.GetAllAsync()).Data.Any())
    {
        await counterService.CreateAsync(new CounterDto { Id= "ef846aa1-771e-4434-85af-4eae86dba355",SerialNumber="12345678", MeasurementTime=DateTime.Now, LatestIndex="123", VoltageValue="123", CurrentValue="1234", UserId="1", CreatedTime=DateTime.Now,UpdateTime=DateTime.Now });
        await counterService.CreateAsync(new CounterDto { Id = "ef846aa1-771e-4434-85af-4eae86dba357", SerialNumber = "12345678", MeasurementTime = DateTime.Now, LatestIndex = "123455", VoltageValue = "123455", CurrentValue = "123456789", UserId = "1", CreatedTime = DateTime.Now, UpdateTime = DateTime.Now });

        await counterService.CreateAsync(new CounterDto { Id = "ef846aa1-771e-4434-85af-4eae86dba356", SerialNumber = "31345678", MeasurementTime = DateTime.Now, LatestIndex = "456", VoltageValue = "456", CurrentValue = "456", UserId = "1", CreatedTime = DateTime.Now, UpdateTime = DateTime.Now });
    }
}
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















