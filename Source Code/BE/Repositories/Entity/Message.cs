using System;
using System.Collections.Generic;

namespace Repositories.Entity;

public partial class Message
{
    public int MessageId { get; set; }

    public int ConversationId { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public bool IsRead { get; set; }

    public virtual Conversation Conversation { get; set; } = null!;

    public virtual Users Receiver { get; set; } = null!;

    public virtual Users Sender { get; set; } = null!;
}
