using Microsoft.AspNetCore.Identity;
using SpecRandomizer.Server.Model;

public class UserRole 
{
    public User user { get; set; }
    public string role;
}