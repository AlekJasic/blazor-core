using System.Threading.Tasks;
using NUnit.Framework;


namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        //[Fact]
        //public async Task CreateActionResult_ReturnsBadRequest_GivenInvalidModel()
        //{
        //    // Arrange & Act
        //    var mockRepo = new Moq.Mock<IBrainstormSessionRepository>();
        //    var controller = new IdeasController(mockRepo.Object);
        //    controller.ModelState.AddModelError("error", "some error");

        //    // Act
        //    var result = await controller.CreateActionResult(model: null);

        //    // Assert
        //    var actionResult = Assert.IsType<ActionResult<BrainstormSession>>(result);
        //    Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        //}
    }

    internal interface IBrainstormSessionRepository
    {
    }
}