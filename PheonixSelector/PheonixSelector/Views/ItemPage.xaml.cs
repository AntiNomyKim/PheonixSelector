using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PheonixSelector.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemPage : ContentPage
	{
		public ItemPage ()
		{
			InitializeComponent ();
            BindingContext = new ViewModels.ItemPageViewModel(this);
		}
	}
}