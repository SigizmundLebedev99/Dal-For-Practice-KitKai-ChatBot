using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DALforChatBot.MessageLogic;
using Microsoft.EntityFrameworkCore;
using ModelEntities.Models;
using ModelEntities.Repositories;

namespace DALforChatBot.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private ChatBotContext _context;
        private TimeAwaitersControl timeControl;

        internal MessagesRepository(ChatBotContext context, int delayCount, TimeSpan span)
        {
            timeControl = new TimeAwaitersControl(span, delayCount);
            _context = context;
        }

        public async Task Create(MessageInfo item)
        {
            var lastMess = await _context.MessageInfos.LastOrDefaultAsync();
            timeControl.SetParent(item, lastMess?.Id + 1 ?? 1);
            _context.MessageInfos.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MessageInfo>> Find(Expression<Func<MessageInfo, bool>> predicate)
        {
            return await _context.MessageInfos.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<MessageInfo>> FindAsNoTracking(Expression<Func<MessageInfo, bool>> predicate)
        {
            return await _context.MessageInfos.Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<MessageInfo> Get(int id)
        {
            return await _context.MessageInfos.FindAsync(id);
        }

        public async Task<IEnumerable<MessageInfo>> GetAll()
        {
            return await _context.MessageInfos.ToListAsync();
        }

        public async Task<IEnumerable<MessageInfo>> GetAllAsNoTracking()
        {
            return await _context.MessageInfos.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MessageInfo>> GetMessagesByOperator(Operator _operator)
        {
            return await _context.MessageInfos.Where(m => m.Operator.Id == _operator.Id).ToListAsync();
        }

        public async Task<IEnumerable<MessageInfo>> GetMessagesByUser(User user)
        {
            return await _context.MessageInfos.Where(m => m.User.Id == user.Id).ToListAsync();
        }
    }
}
