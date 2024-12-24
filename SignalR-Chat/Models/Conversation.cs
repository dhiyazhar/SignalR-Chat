using System.ComponentModel.DataAnnotations;

namespace SignalR_Chat.Models
{
    public class Conversation
    {
        public Conversation()
        {
            Messages = new HashSet<Message>();
            Participants = new HashSet<Participant>();
        }
        public Guid Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        public ConversationType Type { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
    }

    public enum ConversationType
    {
        Private = 1,
        Group = 2
    }
}
