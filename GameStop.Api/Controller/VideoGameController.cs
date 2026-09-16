using GameStop.Api.DTO;
using GameStop.Api.Models;
using GameStop.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GameStop.Api.Controller;

// api / VideoGame
[ApiController]
[Route("api/[controller]")]
public class VideoGameController : ControllerBase
{
    private readonly IVideoGameRepository _repository;

    public VideoGameController(IVideoGameRepository repository)
    {
        _repository = repository;
    }

    //GET "api/VideoGame"
    [HttpGet]
    public async Task<List<VideoGame>> Get()
    {
        return await _repository.GetAll();
    }

    // GET per ID api/VideoGame/1
    [HttpGet("{id}")]
    public async Task<ActionResult<VideoGame>> Get(int id)
    {
        var game = await _repository.GetById(id);
        if (game == null)
            return NotFound();
        return game;
    }

    //POST api/VideoGame
    [HttpPost]
    public async Task<ActionResult<VideoGame>> Post(VideoGameDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest(new { error = "Name is required." });
        if (dto.CopiesCount < 0)
            return BadRequest(new { error = "CopiesCount cannot be negative." });
        if (dto.Price < 0)
            return BadRequest(new { error = "Price cannot be negative." });

        var videoGame = new VideoGame
        {
            Name = dto.Name,
            Publisher = dto.Publisher,
            Developer = dto.Developer,
            CopiesCount = dto.CopiesCount,
            Price = dto.Price
        };

        var created = await _repository.Add(videoGame);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    //PUT api/VideoGame/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, VideoGameDto dto)
    {
        var existing = await _repository.GetById(id);
        if (existing == null)
            return NotFound();

        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest(new { error = "Name is required." });
        if (dto.CopiesCount < 0)
            return BadRequest(new { error = "CopiesCount cannot be negative." });
        if (dto.Price < 0)
            return BadRequest(new { error = "Price cannot be negative." });

        existing.Name = dto.Name;
        existing.Publisher = dto.Publisher;
        existing.Developer = dto.Developer;
        existing.CopiesCount = dto.CopiesCount;
        existing.Price = dto.Price;

        await _repository.Update(existing);
        return NoContent();
    }

    //DELETE api/VideoGame/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _repository.GetById(id);
        if (existing == null)
            return NotFound();

        await _repository.Delete(id);
        return NoContent();
    }
}
