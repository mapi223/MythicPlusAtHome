using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpecRandomizer.Server.Model;
using System.Threading.Tasks;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

   
}
