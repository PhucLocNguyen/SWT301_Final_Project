using Repositories.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391Project.Services.ChatSystem
{
    public interface IChatService
    {
        Task<IEnumerable<Message>> GetMessagesAsync(int conversationId);
        Task SendMessageAsync(Message message);
        
    }
}
