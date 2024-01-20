using Moq;
using API.Services;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
namespace API.Tests;
// TODO: Test Coverage
[TestFixture]
public class UserServicesTests
{
    private const string DBName = "Offerme";
    private UserService? _UserService;
    private Mock<ILogger<UserService>> _MockIlogger;
    private OMContext _MockOMContext;
    private DbContextOptions<OMContext> _DbContextOptionsBuilder;
    [SetUp]
    public void Setup()
    {
        _MockIlogger = new Mock<ILogger<UserService>>();
        _DbContextOptionsBuilder = new DbContextOptionsBuilder<OMContext>().UseInMemoryDatabase(databaseName: DBName).Options;
        _MockOMContext = new OMContext(_DbContextOptionsBuilder);
        _MockOMContext.Users.Add(new User
        {
            Name = "Prasad",
            Number = "8428558275",
            Mail = "pmpsrnp@outlook.com",
            Password = "sprasadr",
            GSTIN = "0A1B2C3D4F5G6H7I"
        });
        _MockOMContext.SaveChanges();
    }
    [TearDown]
    public void Tear()
    {
        _UserService = null;
        _MockIlogger.Reset();
        _DbContextOptionsBuilder.Freeze();
        _MockOMContext.Dispose();
    }
    [Test]
    public void GetExceptionTest()
    {
        _UserService = new UserService(_MockIlogger.Object, _MockOMContext);
        var result = _UserService.Get("8428558275", "sprasadr");
        Assert.That(result, Is.Not.Null);
    }
}