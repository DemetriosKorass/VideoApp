using VideoApp.ViewModels;

namespace VideoApp.UI;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel;
    }
}