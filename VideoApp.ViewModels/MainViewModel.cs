using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using VideoApp.Core.Models;
using VideoApp.Data.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace VideoApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IVideoRepository _videoRepository;

        [ObservableProperty]
        private ObservableCollection<Video> videos = [];

        [ObservableProperty]
        private Video selectedVideo = default!;

        public MainViewModel(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
            LoadVideosCommand = new AsyncRelayCommand(LoadVideosAsync);
            UploadVideoCommand = new AsyncRelayCommand(UploadVideoAsync);

            Task.Run(async () =>
            {
                await _videoRepository.InitializeAsync();
                await _videoRepository.SeedDataAsync();
                await LoadVideosAsync();
            });
        }

        public IAsyncRelayCommand LoadVideosCommand { get; }
        public IAsyncRelayCommand UploadVideoCommand { get; }

        private async Task LoadVideosAsync()
        {
            var videoList = await _videoRepository.GetVideosAsync();
            Videos = new ObservableCollection<Video>(videoList);
        }

        private async Task UploadVideoAsync()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Please select a video file",
                    FileTypes = FilePickerFileType.Videos,
                });

                if (result != null)
                {
                    var filePath = result.FullPath;

                    // Create a new video entry (In real scenarios, consider extracting metadata)
                    var newVideo = new Video
                    {
                        Title = Path.GetFileNameWithoutExtension(filePath),
                        Category = "Uncategorized",
                        Thumbnail = "default_thumbnail.png",
                        DateAdded = DateTime.Now,
                    };

                    // Save the video metadata in the database
                    await _videoRepository.SaveVideoAsync(newVideo);

                    // Update the UI
                    Videos.Add(newVideo);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., user cancels file picker, file not supported)
                await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Error", $"Failed to upload video: {ex.Message}", "OK");
            }
        }
    }
}
