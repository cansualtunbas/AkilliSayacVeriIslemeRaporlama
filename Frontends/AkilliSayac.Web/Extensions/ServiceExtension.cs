using AkilliSayac.Web.Handler;
using AkilliSayac.Web.Models;
using AkilliSayac.Web.Services.Interfaces;
using AkilliSayac.Web.Services;

namespace AkilliSayac.Web.Extensions
{
    public static class ServiceExtension
    {
        public static void AddHttpClientServices(this IServiceCollection services, IConfiguration Configuration)
        {
            var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
            //services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();
            services.AddHttpClient<IIdentityService, IdentityService>();
            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<ICounterService, CounterService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Counter.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            //services.AddHttpClient<IDiscountService, DiscountService>(opt =>
            //{
            //    opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Discount.Path}");
            //}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();




        }
    }
}
