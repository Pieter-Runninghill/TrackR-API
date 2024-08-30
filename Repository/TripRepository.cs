using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using TrackR_API.Context;
using TrackR_API.Models;
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
            catch(Exception ex)
            { 
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Trip> GetTripById(int id)
        {
            try
            {
                if(id > 0)
                {
                    var trip = await _context.Trips.FindAsync(id);

                    if(trip != null)
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

                    if(trips != null)
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
    }
}
