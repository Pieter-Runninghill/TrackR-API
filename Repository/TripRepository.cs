using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Globalization;
using System.Text.Json;
using TrackR_API.Context;
using TrackR_API.Models;
using TrackR_API.Models.RequestModel;
using TrackR_API.Models.ResponseModel;
using TrackR_API.Repository.IRepository;

namespace TrackR_API.Repository
{
    public class TripRepository : ITripRepository
    {
        private readonly AppDbContext _context;

        public TripRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Trip entity)
        {
            try
            {
                await _context.Trips.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Trip> GetTripById(int id)
        {
            try
            {
                if (id > 0)
                {
                    var trip = await _context.Trips.FindAsync(id);

                    if (trip != null)
                    {
                        return trip;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<Trip>> GetTripsByUserId(int userId)
        {
            try
            {
                if (userId > 0)
                {
                    var trips = await (from TR in _context.Trips
                                       join U in _context.Users
                                       on TR.UserId equals U.Id
                                       where U.Id == userId
                                       select TR).ToListAsync();

                    if (trips != null)
                    {
                        return trips;
                    }
                }


                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<EligibleResponse> IsEligibleForTravelAllowance(int userId, double clientLat, double clientLng)
        {
            EligibleResponse response = new EligibleResponse();
            var user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                float homeToClientDistance = await GetRouteDistanceAsync(user.HomeLatitude, user.HomeLongitude, clientLat, clientLng);
                float homeToOfficeDistance = await GetRouteDistanceAsync(user.HomeLatitude, user.HomeLongitude, user.OfficeLatitude, user.OfficeLongitude);

                if (homeToClientDistance > homeToOfficeDistance)
                {
                    response.IsEligible = true;
                    response.ReimbursableDistance = (homeToClientDistance - homeToOfficeDistance) / 1000;
                    response.ReimbursableValue = response.ReimbursableDistance * 12;

                    return response;
                }

            }

            response.IsEligible = false;
            response.ReimbursableDistance = 0;
            response.ReimbursableValue = 0;

            return response;

        }

        public async Task<float> GetRouteDistanceAsync(double originLat, double originLng, double destinationLat, double destinationLng)
        {
            string apiKey = "AIzaSyAV6AGUgjeQCNH3USiGmkuk9LMkFTs9XzA";

            string origin = string.Format(CultureInfo.InvariantCulture, "{0},{1}", originLat, originLng);
            string destination = string.Format(CultureInfo.InvariantCulture, "{0},{1}", destinationLat, destinationLng);

            string requestUrl = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin}&destinations={destination}&key={apiKey}";

            using HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();

                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                var root = doc.RootElement;

               
                var distanceInMeters = root
                    .GetProperty("rows")[0]
                    .GetProperty("elements")[0]
                    .GetProperty("distance")
                    .GetProperty("value")
                    .GetDouble();

                return (float)distanceInMeters;
            }
            else
            {
                throw new HttpRequestException($"Failed to get route distance. Status code: {response.StatusCode}");
            }
        }

        public async Task<Trip> CreateTrip(TripRequest entity)
        {
            var eligible = await IsEligibleForTravelAllowance(entity.UserId, entity.EndLatitude, entity.EndLongitude);

            Trip trip = new Trip
            {
                ClientId = entity.ClientId,
                CreatedAt = DateTime.Now,
                EndAddress = entity.EndAddress,
                EndLatitude = entity.EndLatitude,
                EndLongitude = entity.EndLongitude,
                StartAddress = entity.StartAddress,
                StartLatitude = entity.StartLatitude,
                StartLongitude = entity.StartLongitude,
                TripDate = entity.TripDate,
                UserId = entity.UserId,
                IsEligibleForAllowance = eligible.IsEligible,
                Reimbursabledistance = eligible.ReimbursableDistance,
                ReimbursementValue = eligible.ReimbursableValue
            };

            return trip;
        }
    }
}
