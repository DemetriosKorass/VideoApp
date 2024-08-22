using VideoApp.Core.Models;

namespace VideoApp.Data.Interfaces
{
    public interface IVideoRepository
    {
        Task<List<Video>> GetVideosAsync();
        Task<int> SaveVideoAsync(Video video);
        Task<int> DeleteVideoAsync(Video video);
        Task InitializeAsync();
        Task SeedDataAsync(); // Optional: For seeding sample data
    }
}
