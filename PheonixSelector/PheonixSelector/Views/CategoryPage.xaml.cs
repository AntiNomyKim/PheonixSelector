using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PheonixSelector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryPage : ContentPage
	{
		public CategoryPage ()
		{
			InitializeComponent ();
            BindingContext = new ViewModels.CategoryPageViewModel(this);
		}
	}
}