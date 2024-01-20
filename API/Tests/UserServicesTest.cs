/* using Moq;
using API.Services;
using API.Data;
using API.Exceptions;
namespace API.Tests;
// TODO: Test Coverage
[TestFixture]
public class UserControllerTests
{
    private UserService _UserService;
    private Mock<ILogger<UserService>> _MockIlogger;
    private readonly Mock<OMContext> _MockOMContext;
    [SetUp]
    public void Setup()
    {
        _MockIlogger = new Mock<ILogger<UserController>>();
        _MockOMContext = new Mock<OMContext>();
        _UserService = new UserService>(_MockIlogger.Object, _MockOMContext.Object);
    }
    [TearDown]
    public void Tear()
    {
        _UserService = null;
        _MockIlogger.Reset();
        _MockOMContext.Reset();
    }
    [Test]
    public void GetExceptionTest()
    {
        _MockOMContext.Setup(c => c.)
    } */
/*    public void SignIn406()
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
    } */
}