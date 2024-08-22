using VideoApp.Core.Models;

namespace VideoApp.Services.Interfaces;

public interface IVideoService
{
    Task PlayVideoAsync(Video video);
    Task UploadVideoAsync();
    Task<List<Video>> GetVideosAsync();
}
