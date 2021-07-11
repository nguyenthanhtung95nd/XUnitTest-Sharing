using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PracticeUnitTest.Controllers;
using PracticeUnitTest.Repository;
using PracticeUnitTest.ViewModels;
using Xunit;

namespace PracticeUnitTest.Tests.Controllers
{
    public class PostControllerTests
    {
        private readonly PostRepository _repository;

        public PostControllerTests()
        {
            var factory = new ConnectionFactory();
            var context = factory.CreateContextForSqLite();
            var db = new DummyDataDBInitializer();
            db.Seed(context);
            _repository = new PostRepository(context);
        }

        [Fact]
        public async Task Task_GetPostById_Return_OkResult()
        {
            //Arrange
            var controller = new PostController(_repository);
            var postId = 2;

            //Act
            var data = await controller.GetPost(postId);

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async Task GetPostById_Return_NotFoundResult()
        {
            //Arrange
            var controller = new PostController(_repository);
            var postId = 3;

            //Act
            var data = await controller.GetPost(postId);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async Task GetPostById_Return_BadRequestResult()
        {
            //Arrange
            var controller = new PostController(_repository);
            int? postId = null;

            //Act
            var data = await controller.GetPost(postId);

            //Assert
            Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async Task GetPostById_MatchResult()
        {
            //Arrange
            var controller = new PostController(_repository);
            int? postId = 1;

            //Act
            var data = await controller.GetPost(postId);

            //Assert
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var post = okResult.Value.Should().BeAssignableTo<PostViewModel>().Subject;

            Assert.Equal("Test Title 1", post.Title);
            Assert.Equal("Test Description 1", post.Description);
        }

        [Fact]
        public async Task GetPosts_Return_OkResult()
        {
            //Arrange
            var controller = new PostController(_repository);

            //Act
            var data = await controller.GetPosts();

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }
    }
}