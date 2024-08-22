using CommunityToolkit.Maui;
using VideoApp.Data.Interfaces;
using VideoApp.Data.Repositories;
using VideoApp.Services;
using VideoApp.Services.Interfaces;
using VideoApp.ViewModels;

namespace VideoApp.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "videos.db");
            builder.Services.AddSingleton<IVideoRepository>(new VideoRepository(dbPath));
            builder.Services.AddSingleton<IVideoService, VideoService>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<VideoPlayerViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<VideoPlayerPage>();

            return builder.Build();
        }
    }
}
