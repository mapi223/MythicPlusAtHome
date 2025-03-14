using Microsoft.AspNetCore.Identity;
using SpecRandomizer.Server.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserRole 
{
    
    public int UserId { get; set; }
    public int RoleId { get; set; }
}