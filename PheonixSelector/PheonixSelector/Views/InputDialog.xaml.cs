using PheonixSelector.ViewModels;
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
	public partial class InputDialog : ContentPage
	{
		public InputDialog ()
		{
			InitializeComponent ();
            BindingContext = new InputDialogViewModel(this);
		}
	}
}