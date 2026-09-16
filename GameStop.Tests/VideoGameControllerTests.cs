using GameStop.Api.Controller;
using GameStop.Api.Data;
using GameStop.Api.DTO;
using GameStop.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Tests;

public class VideoGameControllerTests
{
    private static VideoGameController CreateController()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var repository = new VideoGameRepository(new ApplicationDbContext(options));
        return new VideoGameController(repository);
    }

    [Fact]
    public async Task Post_WithEmptyName_ReturnsBadRequest()
    {
        var controller = CreateController();

        var result = await controller.Post(new VideoGameDto { Name = "", CopiesCount = 1, Price = 10m });

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task Post_WithNegativePrice_ReturnsBadRequest()
    {
        var controller = CreateController();

        var result = await controller.Post(new VideoGameDto { Name = "Halo", CopiesCount = 1, Price = -5m });

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task Post_WithValidData_ReturnsCreatedWithGeneratedId()
    {
        var controller = CreateController();

        var result = await controller.Post(new VideoGameDto { Name = "Halo", CopiesCount = 3, Price = 59.99m });

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        var game = Assert.IsType<Api.Models.VideoGame>(created.Value);
        Assert.NotEqual(0, game.Id);
    }

    [Fact]
    public async Task Get_WithNonExistentId_ReturnsNotFound()
    {
        var controller = CreateController();

        var result = await controller.Get(999);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Put_WithNonExistentId_ReturnsNotFound()
    {
        var controller = CreateController();

        var result = await controller.Put(999, new VideoGameDto { Name = "Halo", CopiesCount = 1, Price = 10m });

        Assert.IsType<NotFoundResult>(result);
    }
}
