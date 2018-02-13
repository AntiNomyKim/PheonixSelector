using PheonixSelector.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PheonixSelector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemPage : ContentPage
	{
		public ItemPage (Category category)
		{
			InitializeComponent ();
            BindingContext = new ViewModels.ItemPageViewModel(this, category);
		}
	}
}