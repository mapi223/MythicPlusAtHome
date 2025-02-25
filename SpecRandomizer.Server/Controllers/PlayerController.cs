using Microsoft.AspNetCore.Mvc;
using SpecRandomizer.Server.Models;

namespace SpecRandomizer.Server.Controllers
{
    public class PlayerController : Controller
    {

        private readonly IPlayerRepository _playerRepository;

        public PlayerController(IPlayerRepository playerRepository)
        {
        _playerRepository = playerRepository;
        }
        
       // public IActionResult List()
       // {
       //     return(_playerRepository.GetSpecList())
       // }
    }
}
