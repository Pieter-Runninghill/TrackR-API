using Microsoft.EntityFrameworkCore;
using TrackR_API.Context;
using TrackR_API.Models;
using TrackR_API.Repository.IRepository;

namespace TrackR_API.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAll()
        {
            try
            {
                var clientList = await _context.Clients.ToListAsync();

                if (clientList != null)
                {
                    return clientList;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public async Task<Client> GetById(int id)
        {
            try
            {
                var client = await _context.Clients.FindAsync(id);

                if (client != null)
                {
                    return client;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }
    }
}
