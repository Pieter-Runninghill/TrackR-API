using TrackR_API.Models;

namespace TrackR_API.Repository.IRepository
{
    public interface ITripRepository
    {
        Task<List<Trip>> GetTripsByUserId(int userId);

        Task<Trip> GetTripById (int id);

        Task Create(Trip entity);
    }
}
