using Moq;
using API.Models;
using API.Interfaces;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using API.Exceptions;

namespace API.Tests;
// TODO: Test Coverage
[TestFixture]
public class UserControllerTests
{
    private UserController? _UserController;
    private Mock<IUserService> _MockIUserService;
    private Mock<ILogger<UserController>> _MockIlogger;
    [SetUp]
    public void Setup()
    {
        _MockIlogger = new Mock<ILogger<UserController>>();
        _MockIUserService = new Mock<IUserService>();
        _UserController = new UserController(_MockIUserService.Object, _MockIlogger.Object);
    }
    [TearDown]
    public void Tear()
    {
        _UserController = null;
        _MockIlogger.Reset();
        _MockIUserService.Reset();
    }
    [Test]
    public void SignIn406()
    {
        _MockIUserService.Setup(u => u.Get( It.IsAny<string>(),  It.IsAny<string>())).Throws(new InvalidUserException());
        var result = _UserController!.SignIn(  It.IsAny<string>(),  It.IsAny<string>()) as StatusCodeResult;
        Assert.That(result!.StatusCode, Is.EqualTo(StatusCodes.Status406NotAcceptable));
    }
    [Test]
    public void SignIn202()
    {
        _MockIUserService.Setup(u => u.Get( It.IsAny<string>(), It.IsAny<string>())).Returns(new User());
        var result = _UserController!.SignIn( It.IsAny<string>(), It.IsAny<string>()) as AcceptedResult;
        Assert.That(result!.StatusCode, Is.EqualTo(StatusCodes.Status202Accepted));
    }
    [Test]
    public async Task SignUp403()
    {
        _MockIUserService.Setup(u => u.Post(It.IsAny<User>())).ThrowsAsync(new DuplicateUserException("Duplicate User"));
        var result = await _UserController!.SignUp(It.IsAny<User>()) as StatusCodeResult;
        Assert.That(result!.StatusCode, Is.EqualTo(StatusCodes.Status403Forbidden));
    }
    [Test]
    public async Task SignUp201()
    {
        _MockIUserService.Setup(u => u.Post(It.IsAny<User>()));
        var result = await _UserController!.SignUp(It.IsAny<User>()) as StatusCodeResult;
        Assert.That(result!.StatusCode, Is.EqualTo(StatusCodes.Status201Created));
    }
}