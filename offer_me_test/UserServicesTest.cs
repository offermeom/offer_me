using Moq;
using API.Data;
using API.Models;
using API.Services;
using API.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
namespace API.Tests;
[Author("Prasad", "narendra.prasadsr@nbfc.com")]
[TestFixture, Description("Test class for user service")]
static class UserServicesTests
{
    private const string DBName = "Offerme";
    static UserService? _UserService;
    static Mock<ILogger<UserService>> _MockIlogger;
    static OMContext _OMContext;
    static DbContextOptions<OMContext> _DbContextOptionsBuilder;
    [SetUp]
    public static void Setup()
    {
        _MockIlogger = new Mock<ILogger<UserService>>();
        _DbContextOptionsBuilder = new DbContextOptionsBuilder<OMContext>().UseInMemoryDatabase(databaseName: DBName).Options;
        _OMContext = new OMContext(_DbContextOptionsBuilder);
        _OMContext.Users.Add(new User
        {
            Name = "Prasad",
            Number = "8428558275",
            Mail = "pmpsrnp@outlook.com",
            Password = "sprasadr",
            GSTIN = "0A1B2C3D4F5G6H7I"
        });
        _OMContext.SaveChanges();
        _UserService = new UserService(_MockIlogger.Object, _OMContext);
    }
    [TearDown]
    public static void Tear()
    {
        _UserService = null;
        _MockIlogger.Reset();
        _DbContextOptionsBuilder.Freeze();
        _OMContext.Dispose();
    }
    [Test, Description("Test method to throw Invalid User Exception")]
    public static void GetExceptionTest()
    {
        Assert.Throws<InvalidUserException>(() => _UserService!.Get("9790687606", "prasad"));
    }
    [Test, Description("Test method to get an user")]
    public static void GetUserTest()
    {
        var result = _UserService!.Get("8428558275", "sprasadr");
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<User>());
            Assert.That(result.ID, Is.EqualTo(1));
        });
    }
    [Test, Description("Test method to throw Duplicate User Exception")]
    public static void PostExceptionTest()
    {
        User user = new()
        {
            Name = "Prasad",
            Number = "8428558275",
            Mail = "pmpsrnp@outlook.com",
            Password = "sprasadr",
            GSTIN = "0A1B2C3D4F5G6H7I"
        };
        var result = _UserService!.Post(user);
        Assert.Multiple(() =>
        {
            Assert.That(result.Exception!.InnerException!.Source, Is.EqualTo("API"));
            Assert.That(result.Exception.InnerException.Message, Is.EqualTo("Duplicate User"));
            Assert.That(result.Exception.Message, Is.EqualTo("One or more errors occurred. (Duplicate User)"));
            Assert.ThrowsAsync<DuplicateUserException>(() => result);
        });
    }
    [Test, Description("Test method to add an user to In Memory DB")]
    public static void PostUserTest()
    {
        User user = new()
        {
            Name = "Naren",
            Number = "9790687606",
            Mail = "naren000000000@gmail.com",
            Password = "sprasadr",
            GSTIN = "3S4T5U6V7W8X9Y0Z"
        };
        var result = _UserService!.Post(user);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Exception,Is.Null);
            Assert.DoesNotThrowAsync(() => result);
        });
    }
}