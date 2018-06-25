using Microsoft.EntityFrameworkCore;
using ModelEntities.Models;
using System;

namespace DALforChatBot
{
    class ChatBotContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RegistredUser> RegistredUsers { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<MessageInfo> MessageInfos { get; set; }

        private string connString;

        public ChatBotContext(string connectionString)
        {
            connString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connString);
        }
        //"Server=(localdb)\\mssqllocaldb;Database=ChatBotApp;Trusted_Connection=True;"
    }
}
