using API.Models;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace API.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _IUserService;
    public UserController(IUserService IUserService, ILogger<UserController> logger)
    {
        _IUserService = IUserService;
        _logger = logger;
    }
    [HttpGet("Login")]
    public IActionResult SignIn(string Number, string Password)
    {
        try
        {
            _logger.LogInformation("UserController : SignIn Action");
            var user = _IUserService.Get(Number, Password);
            return Accepted(user);
        }
        catch(Exception e)
        {
            _logger.LogError("UserController : SignIn Action", e.Message);
            return StatusCode(StatusCodes.Status406NotAcceptable);
        }
    }
    [HttpPost("Register")]
    public async Task<IActionResult> SignUp([FromBody] User user)
    {
        try
        {
            _logger.LogInformation("UserController : SignUp Action");
            await _IUserService.Post(user);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch(Exception e)
        {
            _logger.LogError("UserController : SignUp Action", e.Message);
            return StatusCode(StatusCodes.Status403Forbidden);
        }
    }
}