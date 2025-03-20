using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserApplication.Models
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
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
        public User? ModifiedBy { get; set; }
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
        public static ConfigurationDto ConvertToDto(Configuration configurations)
        {
            return new ConfigurationDto
            {
                ConfigurationId = configurations.ConfigurationId,
                UserId = configurations.UserId,
                Players = PlayerDto.ConvertToDtoList((List<Player>)configurations.Players)
            };
        }
    }
}
