using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VideoApp.Core.Models;

namespace VideoApp.ViewModels;

public partial class VideoPlayerViewModel() : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private Video videoRecord = default!;
    [ObservableProperty]
    private MediaSource source = default!;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Video", out var video))
        {
            VideoRecord = video as Video ?? throw new ArgumentNullException(video.ToString());
            Source = MediaSource.FromFile(VideoRecord.LocalPath);
        } 
    }

    [RelayCommand]
    private static async Task BackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
