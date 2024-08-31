using TrackR_API.Models;

namespace TrackR_API.Repository.IRepository
{
    public interface IClientLocaionRepository
    {
        Task<List<ClientLocation>> GetAll();

        Task<ClientLocation> GetById(int id);
    }
}
