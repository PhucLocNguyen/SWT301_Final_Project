using System;
using System.Collections.Generic;

namespace Repositories.Entity;

public partial class Conversation
{
    public int ConversationId { get; set; }

    public int User1Id { get; set; }

    public int User2Id { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual Users User1 { get; set; } = null!;

    public virtual Users User2 { get; set; } = null!;
}
