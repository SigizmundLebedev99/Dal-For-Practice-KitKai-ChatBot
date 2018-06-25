using ModelEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DALforChatBot.MessageLogic
{
    class TimeAwaitersControl
    {
        private List<TimeAwaiter> Sessions { get; set; }
        public readonly Timer timer;
        public event TimerCallback Tick;
        public readonly int CommonDelay;

        public TimeAwaitersControl(TimeSpan span, int delay)
        {
            CommonDelay = delay;
            Sessions = new List<TimeAwaiter>();
            timer = new Timer(OnTick, null, 0, (int)span.TotalMilliseconds);
        }

        private void OnTick(object state)
        {
            Tick?.Invoke(state);
        }

        public async void SetParent(MessageInfo info, int id)
        {
            await Task.Run(() =>
            {
                var record = Sessions.FirstOrDefault(t => t.User.Id == info.User.Id);
                if (record == null)
                {
                    info.Parent = 0;
                    AddSession(info.User, id);
                }
                else
                {
                    record.Timeout = CommonDelay;
                    info.Parent = record.Parent;
                }
            }
            );
        }

        private void AddSession(User user, int parent)
        {
            var awaiter = new TimeAwaiter(this, user, parent);
            Sessions.Add(awaiter);
        }

        public void RemoveSession(TimeAwaiter awaiter)
        {
            Sessions.Remove(awaiter);
        }
    }
}
