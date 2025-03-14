using Microsoft.AspNetCore.Identity;
using SpecRandomizer.Server.Model;
using SpecRandomizer.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserRole 
{
    
    public int UserId { get; set; }
    public User User { get; set; }
    public int RoleId { get; set; }
    public SpecRandomizer.Server.Models.Role Role { get; set; }
}