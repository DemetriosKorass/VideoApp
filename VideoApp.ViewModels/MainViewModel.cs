using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using VideoApp.Core.Models;
using VideoApp.Data.Interfaces;

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

            Task.Run(async () =>
            {
                await _videoRepository.InitializeAsync();
                await _videoRepository.SeedDataAsync();
                await LoadVideosAsync();
            });
        }

        public IAsyncRelayCommand LoadVideosCommand { get; }

        private async Task LoadVideosAsync()
        {
            var videoList = await _videoRepository.GetVideosAsync();
            Videos = new ObservableCollection<Video>(videoList);
        }
    }
}
