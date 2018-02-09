using PheonixSelector.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace PheonixSelector.ViewModels
{
    class CategoryPageViewModel : INotifyPropertyChanged
    {
        private ContentPage page;

        public Category SelectedCategory
        {
            get;
            set;
        }

        public List<Category> CategoryList
        {
            get;
            set;
        }

        #region [Commands]
        public Command Btn_DelClick
        {
            get
            {
                return new Command(() =>
                {

                });
            }
        }
        public Command Btn_AddClick
        {
            get
            {
                return new Command(() =>
                {

                });
            }
        }
        public Command Btn_RtnClick
        {
            get
            {
                return new Command(() =>
                {
                    page.Navigation.PopModalAsync();
                });
            }
        }
        #endregion [Commands]

        public CategoryPageViewModel(ContentPage page)
        {
            this.page = page;
        }

        #region [INotyfyPropertyChanged 구현]
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion [INotyfyPropertyChanged 구현]
    }
}
