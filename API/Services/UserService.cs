using API.Data;
using API.Models;
using API.Interfaces;
using API.Exceptions;
using Microsoft.Extensions.Logging;

namespace API.Services;
public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly OMContext _OMContext;
    public UserService(ILogger<UserService> logger, OMContext OMContext)
    {
        _logger = logger;
        _OMContext = OMContext;
    }
    private List<User> Get(User user)
    {
        try
        {
            _logger.LogInformation("UserService : Get Action");
            List<User>? _user = _OMContext.Users!.Where(u => u.Number == user.Number || u.Name == user.Name || u.Mail == user.Mail || u.GSTIN == user.GSTIN).ToList();
            return _user ?? new List<User>();
        }
        catch(Exception e)
        {
            _logger.LogError("UserServie : Get Action", e.Message);
            throw;
        }
    }
    public User Get(string Number, string Password)
    {
        try
        {
            _logger.LogInformation("UserService : Get Action");
            var user = _OMContext.Users!.SingleOrDefault(u => u.Number == Number && u.Password == Password);
            return user ?? throw new InvalidUserException("Invalid User");
        }
        catch(Exception e)
        {
            _logger.LogError("UserServie : Get Action", e.Message);
            throw;
        }
    }
    public async Task Post(User user)
    {
        try
        {
            _logger.LogInformation("UserService : Post Action");
            List<User> _user = Get(user);
            if(_user.Count > 0)
            {
                throw new DuplicateUserException("Duplicate User");
            }
            else
            {
                await _OMContext.Users!.AddAsync(user);
                await _OMContext.SaveChangesAsync();
            }
        }
        catch(Exception e)
        {
            _logger.LogError("UserService : Post Action", e.Message);
            throw;
        }
    }
}