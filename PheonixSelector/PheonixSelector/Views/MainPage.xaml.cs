using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PheonixSelector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();

            BindingContext = new ViewModels.MainPageViewModel(this);
        }
	}
}