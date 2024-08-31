using TrackR_API.Models;

namespace TrackR_API.Repository.IRepository
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAll();

        Task<Client> GetById(int id);
    }
}
