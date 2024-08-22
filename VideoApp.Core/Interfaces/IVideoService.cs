using VideoApp.Core.Models;

namespace VideoApp.Core.Interfaces
{
    public interface IVideoService
    {
        Task<IEnumerable<Video>> GetAllVideosAsync();
        Task AddVideoAsync(Video video);
        Task<Video> GetVideoByIdAsync(int id);
    }
}