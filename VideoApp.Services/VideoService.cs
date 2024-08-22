using SQLite;
using VideoApp.Core.Interfaces;
using VideoApp.Core.Models;

namespace VideoApp.Services
{
    public class VideoService : IVideoService
    {
        private readonly SQLiteAsyncConnection _database;

        public VideoService()
        {
            _database = new SQLiteAsyncConnection("videos.db");
            _database.CreateTableAsync<Video>().Wait();
        }

        public async Task<IEnumerable<Video>> GetAllVideosAsync()
        {
            return await _database.Table<Video>().ToListAsync();
        }

        public async Task AddVideoAsync(Video video)
        {
            await _database.InsertAsync(video);
        }

        public async Task<Video> GetVideoByIdAsync(int id)
        {
            return await _database.FindAsync<Video>(id);
        }
    }
}