using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecRandomizer.Server.Model
{
    public class Configuration
    {
        [Key]
        public int ConfigurationId { get; set; }

        public int? UserId { get; set; }

        public int? ModifiedBy { get; set; }

        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
       
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }


    public class ConfigurationDto
    {
        public int ConfigurationId { get; set; }
        public int? UserId { get; set; }
        public List<PlayerDto> Players { get; set; } = new List<PlayerDto>();

        public static List<ConfigurationDto> ConvertToDtoList(List<Configuration> configurations)
        {
            return [.. configurations.Select(ra => new ConfigurationDto
            {
                ConfigurationId = ra.ConfigurationId,
                UserId = ra.UserId,
                Players = PlayerDto.ConvertToDtoList((List<Player>)ra.Players)
            })];
        }

    }
}
