using TrackR_API.Models;
using TrackR_API.Models.RequestModel;
using TrackR_API.Models.ResponseModel;

namespace TrackR_API.Repository.IRepository
{
    public interface ITripRepository
    {
        Task<List<Trip>> GetTripsByUserId(int userId);

        Task<Trip> GetTripById (int id);

        Task Create(Trip entity);

        Task<Trip> CreateTrip(TripRequest entity);
    }
}
