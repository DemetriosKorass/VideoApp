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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IVideoRepository, VideoRepository>();
            builder.Services.AddSingleton<IVideoService, VideoService>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}
