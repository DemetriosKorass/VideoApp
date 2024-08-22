namespace VideoApp.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("VideoPlayerPage", typeof(VideoPlayerPage));
        }
    }
}
