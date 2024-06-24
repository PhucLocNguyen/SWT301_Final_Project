using API.Model.UserModel;
using Repositories.Entity;

namespace API.Model.ConversationModel
{

    public class ConversationDto
    {
        public int ConversationId { get; set; }

        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

        public virtual UserDTO User { get; set; } = null!;

    }
}
