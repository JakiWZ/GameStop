using GameStop.Api.Data;
using GameStop.Api.Models;
using GameStop.Api.Repository;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Tests;

public class VideoGameRepositoryTests
{
    private static ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task Add_PersistsVideoGame()
    {
        var repository = new VideoGameRepository(CreateContext());

        var created = await repository.Add(new VideoGame { Name = "Halo", Publisher = "Xbox Game Studios", Developer = "343 Industries", CopiesCount = 5, Price = 59.99m });

        Assert.NotEqual(0, created.Id);
        Assert.Equal("Halo", created.Name);
    }

    [Fact]
    public async Task GetAll_ReturnsAllPersistedGames()
    {
        var context = CreateContext();
        var repository = new VideoGameRepository(context);
        await repository.Add(new VideoGame { Name = "Halo", CopiesCount = 5, Price = 59.99m });
        await repository.Add(new VideoGame { Name = "Forza", CopiesCount = 3, Price = 49.99m });

        var games = await repository.GetAll();

        Assert.Equal(2, games.Count);
    }

    [Fact]
    public async Task GetById_WithExistingId_ReturnsGame()
    {
        var repository = new VideoGameRepository(CreateContext());
        var created = await repository.Add(new VideoGame { Name = "Halo", CopiesCount = 5, Price = 59.99m });

        var found = await repository.GetById(created.Id);

        Assert.NotNull(found);
        Assert.Equal("Halo", found!.Name);
    }

    [Fact]
    public async Task GetById_WithNonExistentId_ReturnsNull()
    {
        var repository = new VideoGameRepository(CreateContext());

        var found = await repository.GetById(999);

        Assert.Null(found);
    }

    [Fact]
    public async Task Update_ChangesPersistedFields()
    {
        var repository = new VideoGameRepository(CreateContext());
        var created = await repository.Add(new VideoGame { Name = "Halo", CopiesCount = 5, Price = 59.99m });

        created.Price = 39.99m;
        created.CopiesCount = 2;
        await repository.Update(created);
        var updated = await repository.GetById(created.Id);

        Assert.Equal(39.99m, updated!.Price);
        Assert.Equal(2, updated.CopiesCount);
    }

    [Fact]
    public async Task Delete_RemovesGame()
    {
        var repository = new VideoGameRepository(CreateContext());
        var created = await repository.Add(new VideoGame { Name = "Halo", CopiesCount = 5, Price = 59.99m });

        await repository.Delete(created.Id);
        var found = await repository.GetById(created.Id);

        Assert.Null(found);
    }

    [Fact]
    public async Task Delete_WithNonExistentId_DoesNotThrow()
    {
        var repository = new VideoGameRepository(CreateContext());

        var exception = await Record.ExceptionAsync(() => repository.Delete(999));

        Assert.Null(exception);
    }
}
