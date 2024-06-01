using AkilliSayac.Shared.Dtos;
using AkilliSayac.Web.Models;
using IdentityModel.Client;

namespace AkilliSayac.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SigninInput signinInput);

        Task<TokenResponse> GetAccessTokenByRefreshToken();

        Task RevokeRefreshToken();
    }
}
