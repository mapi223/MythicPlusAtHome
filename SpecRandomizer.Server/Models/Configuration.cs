using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecRandomizer.Server.Model
{
    public class Configuration
    {
        [Key]
        public int ConfigurationId { get; set; }

        public int? UserId { get; set; }

        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
       
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
