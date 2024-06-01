using AkilliSayac.Web.Models;

namespace AkilliSayac.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
