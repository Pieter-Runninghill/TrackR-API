using Microsoft.EntityFrameworkCore;
using TrackR_API.Context;
using TrackR_API.Models;
using TrackR_API.Repository.IRepository;

namespace TrackR_API.Repository
{
    public class ClientLocationRepository : IClientLocaionRepository
    {
        private readonly AppDbContext _context;

        public ClientLocationRepository(AppDbContext context)
        {
            _context = context;   
        }

        public async Task<List<ClientLocation>> GetAll()
        {
            try
            {
                var clientLocations = await _context.ClientLocations.ToListAsync();

                if(clientLocations != null)
                {
                    return clientLocations;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<ClientLocation> GetById(int id)
        {
            try
            {
                var clientLocation = await _context.ClientLocations.FindAsync(id);

                if (clientLocation != null)
                {
                    return clientLocation;
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
