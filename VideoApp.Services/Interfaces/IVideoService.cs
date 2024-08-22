using VideoApp.Core.Models;

namespace VideoApp.Services.Interfaces;

public interface IVideoService
{
    Task UploadVideoAsync();
    Task<List<Video>> GetVideosAsync();
}
