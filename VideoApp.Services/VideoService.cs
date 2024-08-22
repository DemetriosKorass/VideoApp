using VideoApp.Core.Models;
using VideoApp.Data.Interfaces;
using VideoApp.Services.Interfaces;

namespace VideoApp.Services;

public class VideoService(IVideoRepository videoRepository) : IVideoService
{
    private readonly IVideoRepository _videoRepository = videoRepository;

    public async Task<List<Video>> GetVideosAsync()
    {
        return await _videoRepository.GetVideosAsync();
    }

    public async Task UploadVideoAsync()
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Please select a video file",
            FileTypes = FilePickerFileType.Videos
        });

        if (result != null)
        {
            var videoDirectory = Path.Combine(FileSystem.AppDataDirectory, "videos");

            if (!Directory.Exists(videoDirectory))
            {
                Directory.CreateDirectory(videoDirectory);
            }

            var destinationPath = Path.Combine(videoDirectory, result.FileName);

            using (var sourceStream = await result.OpenReadAsync())
            using (var destinationStream = File.Create(destinationPath))
            {
                await sourceStream.CopyToAsync(destinationStream); //TODO: Needs refactoring - saving/copy functionality needs to be either moved to another method or removed
            }

            var newVideo = new Video
            {
                Title = Path.GetFileNameWithoutExtension(result.FileName),
                Category = "Uncategorized",
                Thumbnail = "default_thumbnail.png",
                DateAdded = DateTime.Now,
                LocalPath = destinationPath,
            };

            await _videoRepository.SaveVideoAsync(newVideo);
        }
    }
    public static async Task SelectVideoAsync(Video video)
    {
        if (video == null)
            return;

        var navigationParameters = new Dictionary<string, object>
        {
            { "Video", video }
        };

        await Shell.Current.GoToAsync("VideoPlayerPage", navigationParameters);
    }
}
