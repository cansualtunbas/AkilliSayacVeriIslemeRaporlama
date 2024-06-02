using AkilliSayac.Shared.Classes;
using AkilliSayac.Shared.Services;
using AkilliSayac.Web.Extensions;
using AkilliSayac.Web.Handler;
using AkilliSayac.Web.Models;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddMassTransit(x =>
{

    x.UsingRabbitMq((context, cfg) =>
    {
        //dockerize edilseydi burdaki adres dockerize adresi olacakti
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

    });

});

builder.Services.AddFluentValidationAutoValidation();

builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAccessTokenManagement();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
//builder.Services.AddScoped<ClientCredentialTokenHandler>();

builder.Services.AddHttpClientServices(builder.Configuration);



//cookie ayarlarý-cookie bazlý bir yetkilendirme
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
{
    opts.LoginPath = "/Auth/SignIn";
    opts.ExpireTimeSpan = TimeSpan.FromDays(60);
    opts.SlidingExpiration = true;
    opts.Cookie.Name = "webcookie";
});






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
