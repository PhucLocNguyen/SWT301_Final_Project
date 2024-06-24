using Repositories.Entity;
using SWP391Project.API.Model.MessageModel;

namespace API.Model.MessageModel
{
    public static class MessageMapper
    {
        public static MessageDto ToMessageDto(this Message message)
        {
            return new MessageDto()
            {
               MessageId = message.MessageId,
               ReceiverId = message.ReceiverId,
               SenderId = message.SenderId,
               Content = message.Content,
               ConversationId = message.ConversationId,
               IsRead = message.IsRead,
               Timestamp = message.Timestamp
            };
        }
       
    }
}
