using Moq;
using API.Models;
using API.Exceptions;
using API.Interfaces;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace API.Tests;
[TestFixture]
static class UserControllerTests
{
    static UserController? _UserController;
    static Mock<IUserService> _MockIUserService;
    static Mock<ILogger<UserController>> _MockIlogger;
    [SetUp]
    public static void Setup()
    {
        _MockIlogger = new Mock<ILogger<UserController>>();
        _MockIUserService = new Mock<IUserService>();
        _UserController = new UserController(_MockIUserService.Object, _MockIlogger.Object);
    }
    [TearDown]
    public static void Tear()
    {
        _UserController = null;
        _MockIlogger.Reset();
        _MockIUserService.Reset();
    }
    [Test]
    public static void SignIn406()
    {
        _MockIUserService.Setup(u => u.Get(It.IsAny<string>(), It.IsAny<string>())).Throws(new InvalidUserException());
        var result = _UserController!.SignIn(It.IsAny<string>(), It.IsAny<string>()) as StatusCodeResult;
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(StatusCodes.Status406NotAcceptable));
        });
    }
    [Test]
    public static void SignIn202()
    {
        _MockIUserService.Setup(u => u.Get(It.IsAny<string>(), It.IsAny<string>())).Returns(new User());
        var result = _UserController!.SignIn(It.IsAny<string>(), It.IsAny<string>()) as AcceptedResult;
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(StatusCodes.Status202Accepted));
        });
    }
    [Test]
    public static async Task SignUp403()
    {
        _MockIUserService.Setup(u => u.Post(It.IsAny<User>())).ThrowsAsync(new DuplicateUserException("Duplicate User"));
        var result = await _UserController!.SignUp(It.IsAny<User>()) as StatusCodeResult;
        Assert.Multiple(() => 
        { 
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(StatusCodes.Status403Forbidden));
        });
    }
    [Test]
    public static async Task SignUp201()
    {
        _MockIUserService.Setup(u => u.Post(It.IsAny<User>()));
        var result = await _UserController!.SignUp(It.IsAny<User>()) as StatusCodeResult;
        Assert.Multiple(() => 
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StatusCode, Is.EqualTo(StatusCodes.Status201Created));
        });
    }
}