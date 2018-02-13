using PheonixSelector.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PheonixSelector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InputDialog : ContentPage
	{
		public InputDialog ()
		{
			InitializeComponent ();
            BindingContext = new InputDialogViewModel(this);
		}
	}
}