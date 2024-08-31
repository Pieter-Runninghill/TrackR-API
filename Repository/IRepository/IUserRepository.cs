using TrackR_API.Models;
using TrackR_API.Models.RequestModel;

namespace TrackR_API.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUser(string emailAddress);

        Task Update(User entity);

        Task<List<User>> GetUsers();

        Task Create(User entity);

        Task<bool> UserLogin(UserLoginRequest request);

    }
}
