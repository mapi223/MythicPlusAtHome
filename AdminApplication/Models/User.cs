using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminApplication.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9!@#$_\s]{8,50}$",
            ErrorMessage = "User Name must be between 8 and 50 characters and contain only letters, numbers, and the following special characters: !@#$_ and spaces")]
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }

        public  ICollection<Configuration>? Configurations { get; set; }
        public ICollection<UserRole>? UserRoles { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
        public User? ModifiedBy { get; set; }

    }

    public class UserDTO
    {
        [RegularExpression(@"^[a-zA-Z0-9!@#$_\s]{8,50}$",
            ErrorMessage = "User Name must be between 8 and 50 characters and contain only letters, numbers, and the following special characters: !@#$_ and spaces")]
        [Required]
        public string userName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9!@#$_]{8,50}$",
            ErrorMessage = "Password must be between 8 and 50 characters and contain only letters, numbers, and the following special characters: !@#$_")]
        [Required]
        public string password { get; set; }
        public int? uId { get; set; }

        public static UserDTO convertToDTO(User user)
        {
            return new UserDTO
            {
                password = user.UserName,
                uId = user.UserId,
                userName = user.UserName
            }; 
        }
    }
}
