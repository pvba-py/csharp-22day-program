using Microsoft.AspNetCore.Mvc;
using ORSchedulingManagement.Repository;

namespace ORSchedulingManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public UsersController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userRepository.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetUserById(int id)
    {
        var user = _userRepository.GetUserById(id);

        if (user is null)
        {
            return NotFound(new
            {
                Message = $"User with ID {id} was not found."
            });
        }

        return Ok(user);
    }
}