using Repositories.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391Project.Services.ChatSystem
{
    public interface IConversationService
    {
        public Conversation CreateConversation(Conversation conversation);
        public Conversation GetById(int id);
        public bool CheckValidConversation(int userId1, int userId2);

        public IEnumerable<Conversation> GetAllByCurrentUser(int userId);
    }
}
