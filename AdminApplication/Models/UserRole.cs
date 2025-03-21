using AdminApplication.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserRole
{
    
    public int UserId { get; set; }
    public User User { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
}