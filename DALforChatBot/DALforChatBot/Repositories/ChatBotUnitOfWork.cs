using System;
using System.Threading.Tasks;
using ModelEntities;
using ModelEntities.Models;
using ModelEntities.Repositories;

namespace DALforChatBot.Repositories
{
    public class ChatBotUnitOfWork : IUnitOfRepositories
    {
        private ChatBotContext _context;

        public TimeSpan DialogDelaySetter;
        public int DialogDelaysCountSetter;

        public IMessagesRepository MessagesRepository { get; set; }
        public IRepository<Operator> OperatorsRepository { get; set; }
        public IUserRepository<User> CommonUsersRepository { get; set; }
        public IRegistredUserRepository RegistredUsersRepository { get; set; }        

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public ChatBotUnitOfWork(string connectionString, TimeSpan span, int spansCount)
        {
            _context = new ChatBotContext(connectionString);
            CommonUsersRepository = new UsersRepository(_context);
            RegistredUsersRepository = new RegistratedUsersRepository(_context);
            MessagesRepository = new MessagesRepository(_context, spansCount, span);
            OperatorsRepository = new OperatorsRepository(_context);
        }
    }
}
