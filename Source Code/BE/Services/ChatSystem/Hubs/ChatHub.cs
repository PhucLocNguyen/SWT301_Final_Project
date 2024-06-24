using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories;
using Repositories.Entity;
using SWP391Project.Services.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391Project.Services.ChatSystem.Hubs
{
    public class ChatHub :Hub
    {
        private readonly MyDbContext _context;
        public ChatHub(MyDbContext context)
        {
            if (context == null)
            {
                _context = new MyDbContext();
            }
            else { _context = context; }
        }
        public async Task SendMessage(int conversationId, int senderId, int receiverId, string content)
        {
            try
            {
                var message = new Message
                {
                    ConversationId = conversationId,
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Content = content,
                    Timestamp = DateTime.Now,
                    IsRead = false
                };

                _context.Messages.Add(message);
                await _context.SaveChangesAsync();
                var messageId = message.MessageId;
                await Clients.Group(conversationId.ToString()).SendAsync("ReceiveMessage", new
            {
                message.MessageId,
                message.ConversationId,
                message.SenderId,
                message.ReceiverId,
                message.Content,
                message.Timestamp,
                message.IsRead
            });
            }
            catch (Exception ex)
            {
            }
        }

        public async Task JoinConversation(int conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
            Console.WriteLine($"User joined conversation {conversationId}");
        }
        public async Task LeaveConversation(int conversationId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId.ToString());
        }
    }
}
