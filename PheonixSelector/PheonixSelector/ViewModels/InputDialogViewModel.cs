using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace PheonixSelector.ViewModels
{
    class InputDialogViewModel : INotifyPropertyChanged
    {
        private string entry_Name;
        private ContentPage page;

        public string Entry_Name
        {
            get
            {
                return entry_Name;
            }
            set
            {
                entry_Name = value;
                OnPropertyChanged(nameof(Entry_Name));
            }
        }

        #region [Commands]
        public Command Click_BtnCancel
        {
            get
            {
                return new Command(() =>
                {
                    //취소의 경우 공백 처리
                    Entry_Name = string.Empty;
                    page.Navigation.PopModalAsync();
                });
            }
        }

        public Command Click_BtnAdd
        {
            get
            {
                return new Command(() =>
                {
                    //공백 입력 불허
                    if (string.IsNullOrWhiteSpace(Entry_Name))
                    {
                        page.DisplayAlert("Warning", "Insert a Name", "Ok");
                        return;
                    }

                    page.Navigation.PopModalAsync();
                });
            }
        }
        #endregion [Commands]

        public InputDialogViewModel(ContentPage page)
        {
            this.page = page;
        }

        #region [INotifyPropertyChanged]
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion [INotifyPropertyChanged]
    }
}
