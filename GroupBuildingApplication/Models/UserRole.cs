using GroupBuildingService.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserRole 
{
    
    public int UserId { get; set; }
    public User User { get; set; }
    public int RoleId { get; set; }
    public GroupBuildingService.Models.Role Role { get; set; }
}