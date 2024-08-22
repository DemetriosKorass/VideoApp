using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using VideoApp.Core.Models;
using VideoApp.Services;
using VideoApp.Services.Interfaces;

namespace VideoApp.ViewModels
{
    public partial class MainViewModel(IVideoService videoService) : ObservableObject
    {
        private readonly IVideoService _videoService = videoService;

        [ObservableProperty]
        private ObservableCollection<Video> videos = [];

        [ObservableProperty]
        private Video selectedVideo = default!;

        [RelayCommand]
        private async Task LoadVideosAsync()
        {
            var videoList = await _videoService.GetVideosAsync();
            Videos = new ObservableCollection<Video>(videoList);
        }
        [RelayCommand]
        private async Task UploadVideoAsync()
        {
            await _videoService.UploadVideoAsync();
            await LoadVideosAsync();
        }
        
        [RelayCommand]
        private static async Task SelectVideoAsync(Video video)
        {
            await VideoService.SelectVideoAsync(video);
        }
    }
}
