using Microsoft.EntityFrameworkCore;
using Repositories.Entity;
namespace SWP391Project.Services.ChatSystem.Hubs
{
    public class ConversationService : IConversationService
    {
        private readonly MyDbContext _context;
        public ConversationService(MyDbContext context)
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

        public Conversation CreateConversation(Conversation conversation)
        {
            if (!CheckValidConversation(conversation.User1Id, conversation.User2Id))
            {
                var getConversationFromOne = _context.Conversations.FirstOrDefault(x => (x.User1Id == conversation.User1Id && x.User2Id == conversation.User2Id));
                var getConversationFromTwo = _context.Conversations.FirstOrDefault(x => (x.User1Id == conversation.User2Id && x.User2Id == conversation.User1Id));
                if (getConversationFromOne != null)
                {
                    return getConversationFromOne;
                }
                if(getConversationFromTwo != null)
                {
                    return getConversationFromTwo;
                }
            }
                try
                {
                    _context.Conversations.Add(conversation);
                    _context.SaveChanges();
                    return conversation;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            
        }

        public Conversation GetById(int id)
        {
            var conversation = _context.Conversations.Include(x => x.User1).Include(x => x.User2).FirstOrDefault(x=> x.ConversationId == id);
            return conversation;
        }
       
        public bool CheckValidConversation(int userId1, int userId2)
        {
            var output = true;
            if (userId1 == userId2) {
                output = false;
            }
            var check1 = _context.Conversations.FirstOrDefault(x => (x.User1Id == userId1 && x.User2Id == userId2));
            var check2 = _context.Conversations.FirstOrDefault(x => (x.User1Id == userId2 && x.User2Id == userId1));
            if(check1 != null|| check2!=null)
            {
                output = false;
            }
            return output;
        }

        public IEnumerable<Conversation> GetAllByCurrentUser(int userId)
        {
            var conversations = 
                _context.Conversations
        .Where(x => x.User1Id == userId || x.User2Id == userId)
        .Include(x => x.User1)
        .Include(x => x.User2)
                .ToList();

            return conversations;
        }
    }
}