using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_Chat.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        [Required]
        public Guid ConversationId { get; set; }
        [Required]
        public Guid SenderId { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public MessageStatus Status { get; set; }
        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }
        [ForeignKey("ConversationId")]
        public virtual Conversation Conversation { get; set; }
    }

    public enum MessageStatus
    {
        Sent = 1,
        Delivered = 2, 
        Read = 3
    }
}
