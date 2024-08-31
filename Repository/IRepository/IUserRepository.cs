using TrackR_API.Models;

namespace TrackR_API.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUser(string emailAddress);

        Task Update(User entity);

        Task<List<User>> GetUsers();

        Task Create(User entity);

    }
}
