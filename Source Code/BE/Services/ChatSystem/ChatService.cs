using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391Project.Services.ChatSystem
{
    public class ChatService : IChatService
    {
        private readonly MyDbContext _context;
        public ChatService(MyDbContext context)
        {
            if (context == null)
            {
                _context = new MyDbContext();
            }
            else
            {
                _context = context;
            }

        }
        public async Task<IEnumerable<Message>> GetMessagesAsync(int conversationId)
        {
            var listMessageByConversationId = await _context.Messages.Where(x => x.ConversationId == conversationId).ToListAsync();

            return listMessageByConversationId;
        }

        public async Task SendMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

    }
}
