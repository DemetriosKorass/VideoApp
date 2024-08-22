using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using VideoApp.Core.Models;
using VideoApp.Services.Interfaces;

namespace VideoApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IVideoService _videoService;

        [ObservableProperty]
        private ObservableCollection<Video> videos = [];

        [ObservableProperty]
        private Video selectedVideo = default!;

        public MainViewModel(IVideoService videoService)
        {
            _videoService = videoService;
            LoadVideosCommand = new AsyncRelayCommand(LoadVideosAsync);
            UploadVideoCommand = new AsyncRelayCommand(UploadVideoAsync);
        }

        public IAsyncRelayCommand LoadVideosCommand { get; }
        public IAsyncRelayCommand UploadVideoCommand { get; }

        private async Task LoadVideosAsync()
        {
            var videoList = await _videoService.GetVideosAsync();
            Videos = new ObservableCollection<Video>(videoList);
        }

        private async Task UploadVideoAsync()
        {
            await _videoService.UploadVideoAsync();
            await LoadVideosAsync();
        }
    }
}
