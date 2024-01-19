using API.Models;
namespace API.Interfaces;
public interface IUserService
{
    public User Get(string Number, string Password);
    public Task Post(User user);
}