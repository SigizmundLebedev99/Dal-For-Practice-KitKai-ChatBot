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
    public class RegistratedUsersRepository : IRegistredUserRepository
    {
        private ChatBotContext _context;

        internal RegistratedUsersRepository(ChatBotContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckUniqueEmail(string Email)
        {
            return await _context.RegistredUsers.AllAsync(u => u.EmailAddress != Email);
        }

        public async Task<bool> CheckUniquePhone(string phone)
        {
            return await _context.RegistredUsers.AllAsync(u => u.Phone != phone);
        }

        public async Task Create(RegistredUser item)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Phone == item.Phone);
            if(user == null)
            {
                _context.RegistredUsers.Add(item);
            }
            else
            {
                _context.Users.Remove(user);
                _context.RegistredUsers.Add(item);
                _context.MessageInfos.Where(m => m.User.Id == user.Id).AsParallel().ForAll(m => m.User = item);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RegistredUser>> Find(Expression<Func<RegistredUser, bool>> predicate)
        {
            return await _context.RegistredUsers.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<RegistredUser>> FindAsNoTracking(Expression<Func<RegistredUser, bool>> predicate)
        {
            return await _context.RegistredUsers.Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<RegistredUser> FindByPhone(string phone)
        {
            return await _context.RegistredUsers.SingleOrDefaultAsync(u => u.Phone == phone);
        }

        public async Task<RegistredUser> Get(int id)
        {
            return await _context.RegistredUsers.FindAsync(id);
        }

        public async Task<IEnumerable<RegistredUser>> GetAll()
        {
            return await _context.RegistredUsers.ToListAsync();
        }

        public async Task<IEnumerable<RegistredUser>> GetAllAsNoTracking()
        {
            return await _context.RegistredUsers.AsNoTracking().ToListAsync();
        }
    }
}
