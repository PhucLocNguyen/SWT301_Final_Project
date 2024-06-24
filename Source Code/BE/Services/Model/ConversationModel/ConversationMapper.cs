using API.Model.UserModel;
using Repositories.Entity;

namespace API.Model.ConversationModel
{
    public static class ConversationMapper
    {
        public static Conversation ToConversationEntity(this RequestCreateConversation requestCreateConversation)
        {
            return new Conversation()
            {
                User1Id = requestCreateConversation.userId1,
                User2Id = requestCreateConversation.userId2,
            };
        }

            public static ConversationDto ToConversationDto(this Conversation conversation, int userId)
            {
                var user = conversation.User1Id == userId ? conversation.User2 : conversation.User1;
                return new ConversationDto()
                {
                    ConversationId = conversation.ConversationId,
                    Messages = conversation.Messages,
                    User = UserMapper.toUserDTO(user),
                };
            }
    }
}
