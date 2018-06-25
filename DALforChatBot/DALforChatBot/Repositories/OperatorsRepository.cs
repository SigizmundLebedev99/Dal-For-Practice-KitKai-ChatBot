using Microsoft.EntityFrameworkCore;
using ModelEntities.Models;
using ModelEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DALforChatBot.Repositories
{
    public class OperatorsRepository : IRepository<Operator>
    {
        ChatBotContext _context;

        internal OperatorsRepository(ChatBotContext context)
        {
            _context = context;
        }

        public async Task Create(Operator item)
        {
            if(await _context.Operators.AllAsync(o => o.Login != item.Login))
            {
                _context.Operators.Add(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Operator>> Find(Expression<Func<Operator, bool>> predicate)
        {
            return await _context.Operators.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Operator>> FindAsNoTracking(Expression<Func<Operator, bool>> predicate)
        {
            return await _context.Operators.Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<Operator> Get(int id)
        {
            return await _context.Operators.FindAsync(id);
        }

        public async Task<IEnumerable<Operator>> GetAll()
        {
            return await _context.Operators.ToListAsync();
        }

        public async Task<IEnumerable<Operator>> GetAllAsNoTracking()
        {
            return await _context.Operators.AsNoTracking().ToListAsync();
        }
    }
}
