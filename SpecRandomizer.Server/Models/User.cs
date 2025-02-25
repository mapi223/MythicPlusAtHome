using System.ComponentModel.DataAnnotations;

namespace SpecRandomizer.Server.Model
{
    public class User
    {
        private int userId { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9!@#$_\s]{8,50}$",
            ErrorMessage = "User Name must be between 8 and 50 characters and contain only letters, numbers, and the following special characters: !@#$_ and spaces")]
        private string userName { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9!@#$_]{8,50}$", 
            ErrorMessage = "Password must be between 8 and 50 characters and contain only letters, numbers, and the following special characters: !@#$_")]
        private string Password { get; set; }

        public virtual ICollection<Configuration> Configurations{ get; set;}

    }
}
