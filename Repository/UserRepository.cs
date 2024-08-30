﻿using Microsoft.EntityFrameworkCore;
using TrackR_API.Context;
using TrackR_API.Models;
using TrackR_API.Repository.IRepository;
using TrackR_API.Utils;

namespace TrackR_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly EmailValidator _emailValidator;

        public UserRepository(AppDbContext context, EmailValidator emailValidator)
        {
            _context = context;
            _emailValidator = emailValidator;
        }

        public async Task<User> GetUser(string emailAddress)
        {
            try
            {

                if (_emailValidator.ValidateEmail(emailAddress))
                {
                    User user = await _context.Users.Where(x => x.Email.ToLower().Equals(emailAddress.ToLower())).FirstOrDefaultAsync();

                    if(user != null)
                    {
                        return user;
                    }
                }

                return null;
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<User>> GetUsers()
        {
            try
            {
                List<User> users = await _context.Users.AsNoTracking().ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task Update(User entity)
        {
           _context.Users.Update(entity);
           await _context.SaveChangesAsync();
        }
    }
}
