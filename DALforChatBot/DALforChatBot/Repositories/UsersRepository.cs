using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelEntities.Models;
using ModelEntities.Repositories;

namespace DALforChatBot.Repositories
{
    public class UsersRepository : IUserRepository<User>
    {
        private ChatBotContext _context;

        internal UsersRepository(ChatBotContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckUniquePhone(string phone)
        {
            return await _context.Users.AllAsync(u => u.Phone != phone);
        }

        public async Task Create(User item)
        {
            _context.Users.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> Find(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<User>> FindAsNoTracking(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<User> FindByPhone(string phone)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Phone == phone);
        }

        public async Task<User> Get(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsNoTracking()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }
    }
}
