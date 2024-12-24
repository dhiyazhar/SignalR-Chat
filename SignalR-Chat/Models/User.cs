using System.ComponentModel.DataAnnotations;

namespace SignalR_Chat.Models
{
    public class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
            Conversations = new HashSet<Participant>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Participant> Conversations { get; set; }

        
    }
}
