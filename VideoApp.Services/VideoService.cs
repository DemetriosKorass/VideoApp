using VideoApp.Core.Models;
using VideoApp.Data.Interfaces;
using VideoApp.Services.Interfaces;
using Application = Microsoft.Maui.Controls.Application;

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
            var destinationPath = Path.Combine(FileSystem.AppDataDirectory, result.FileName);

            using (var sourceStream = await result.OpenReadAsync())
            using (var destinationStream = File.Create(destinationPath))
            {
                await sourceStream.CopyToAsync(destinationStream);
            }

            var newVideo = new Video
            {
                Title = Path.GetFileNameWithoutExtension(result.FileName),
                Category = "Uncategorized",
                Thumbnail = "default_thumbnail.png",
                DateAdded = DateTime.Now
            };

            await _videoRepository.SaveVideoAsync(newVideo);
        }
    }

    public async Task PlayVideoAsync(Video video)
    {
        // Here you would implement platform-specific logic for video playback.
        // For now, we can simulate this with a display message.
        if (Application.Current is null || Application.Current.MainPage is null)
            throw new ApplicationException();
        await Application.Current.MainPage.DisplayAlert("Playing Video", $"Playing {video.Title}", "OK");
    }
}
