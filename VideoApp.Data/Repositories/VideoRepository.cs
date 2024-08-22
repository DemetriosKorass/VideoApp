using SQLite;
using VideoApp.Core.Models;
using VideoApp.Data.Interfaces;

namespace VideoApp.Data.Repositories;

public class VideoRepository : IVideoRepository
{
    private readonly SQLiteAsyncConnection _database;

    public VideoRepository(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Video>().Wait();
    }

    public Task<List<Video>> GetVideosAsync()
    {
        return _database.Table<Video>().ToListAsync();
    }

    public Task<int> SaveVideoAsync(Video video)
    {
        if (video.Id != 0)
        {
            return _database.UpdateAsync(video);
        }
        else
        {
            return _database.InsertAsync(video);
        }
    }

    public Task<int> DeleteVideoAsync(Video video)
    {
        return _database.DeleteAsync(video);
    }

    public async Task SeedDataAsync()
    {
        if (await _database.Table<Video>().CountAsync() == 0)
        {
            var sampleVideos = LoadSampleVideos();
            foreach (var video in sampleVideos)
            {
                await SaveVideoAsync(video);
            }
        }
    }

    private static List<Video> LoadSampleVideos()
    {
        return
        [
            new() { Title = "Sample Video 1", Category = "Category 1", Thumbnail = "sample1.png", DateAdded = DateTime.Now },
            new() { Title = "Sample Video 2", Category = "Category 2", Thumbnail = "sample2.png", DateAdded = DateTime.Now.AddDays(-1) },
            new() { Title = "Sample Video 3", Category = "Category 3", Thumbnail = "sample3.png", DateAdded = DateTime.Now.AddDays(-2) }
        ];
    }
}
