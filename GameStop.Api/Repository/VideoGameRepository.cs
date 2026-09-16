using GameStop.Api.Data;
using GameStop.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Api.Repository;

public class VideoGameRepository : IVideoGameRepository

{

    private readonly ApplicationDbContext _context;

    public VideoGameRepository (ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<VideoGame>> GetAll()
    {
        return await _context.VideoGames.ToListAsync();
    }

    public async Task<VideoGame?> GetById(int id)
    {
        return await _context.VideoGames.FindAsync(id);
    }

    public async Task<VideoGame> Add(VideoGame videoGame)
    {
        _context.VideoGames.Add(videoGame);
        await _context.SaveChangesAsync();
        return videoGame;
    }

    public async Task Update(VideoGame videoGame)
    {
        _context.VideoGames.Update(videoGame);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var game = await _context.VideoGames.FindAsync(id);
        if (game == null) return;
        _context.VideoGames.Remove(game);
        await _context.SaveChangesAsync();
    }
}