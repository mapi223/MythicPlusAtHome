using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecRandomizer.Server.Model
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }

        public int? ConfigurationId { get; set; }

        public List<ClassList> SpecList { get; set; } = new List<ClassList>();

        [RegularExpression(@"^[a-zA-Z0-9!@#$_\s]{5,50}$",
            ErrorMessage = "Player Name must be between 5 and 50 characters and contain only letters, numbers, and the following special characters: !@#$_ and spaces")]
        [Required]
        public string PlayerName { get; set; }

        [ForeignKey("ConfigurationId")]
        public virtual Configuration? Configuration { get; set; }

    }

    public class PlayerDto
    {
        public int PlayerId { get; set; }
        public List<ClassList> SpecList { get; set; } = new List<ClassList>();
        public string PlayerName { get; set; }

        public static List<PlayerDto> ConvertToDtoList(List<Player> players)
        {
            return [.. players.Select(ra => new PlayerDto
            {
                PlayerId = ra.PlayerId,
                PlayerName = ra.PlayerName,
                SpecList = ra.SpecList
            })];
        }
    }
}
