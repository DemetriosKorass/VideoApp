using VideoApp.Data.Interfaces;
using VideoApp.Data.Repositories;
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

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "videos.db");
            builder.Services.AddSingleton<IVideoRepository>(new VideoRepository(dbPath));
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}
