using ModelEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DALforChatBot.MessageLogic
{
    class TimeAwaiter
    {
        private TimeAwaitersControl _control;
        public User User { get; }
        public int Parent { get; }
        public int Timeout { get; set; }

        public TimeAwaiter(TimeAwaitersControl control, User user, int parent)
        {
            User = user;
            _control = control;
            Timeout = _control.CommonDelay;
            Parent = parent;
            _control.Tick += Tick;
        }

        private void Tick(object obj)
        {
            if (Timeout == 0)
            {
                _control.Tick -= Tick;
                _control.RemoveSession(this);
                return;
            }
            Timeout--;
        }
    }   
}
