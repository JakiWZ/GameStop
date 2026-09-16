using GameStop.Api.Models;

namespace GameStop.Api.Repository;

public interface IVideoGameRepository
{

    Task<List<VideoGame>> GetAll();

    Task<VideoGame?> GetById(int id);

    Task<VideoGame> Add(VideoGame videoGame);

    Task Update(VideoGame videoGame);

    Task Delete(int id);

}