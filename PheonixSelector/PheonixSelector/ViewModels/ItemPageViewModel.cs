using PheonixSelector.Models;
using PheonixSelector.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace PheonixSelector.ViewModels
{
    class ItemPageViewModel : INotifyPropertyChanged
    {
        public ItemPageViewModel(ContentPage page, Category selectedCategory)
        {
            this.page = page;
            this.selectedCategory = selectedCategory;
            SelectedCategoryName = this.selectedCategory == null ? string.Empty : this.selectedCategory.CategoryName;
            ItemList = selectedCategory.ItemList;
        }

        private ContentPage page;
        private Category selectedCategory;
        private Item selectedItem;
        private List<Item> itemList;
        
        public string SelectedCategoryName
        {
            get
            {
                return selectedCategory.CategoryName;
            }
            set
            {
                OnPropertyChanged(nameof(SelectedCategoryName));
            }
        }
        public Item SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public List<Item> ItemList
        {
            get
            {
                return itemList;
            }
            set
            {
                itemList = value;
                OnPropertyChanged(nameof(ItemList));
            }
        }
        public Item CheatedItem { get; set; }

        #region [Commands]
        public Command Btn_DelClick
        {
            get
            {
                return new Command(() =>
                {
                    var list = new List<Item>(ItemList);
                    list.Remove(SelectedItem);
                    ItemList = list;
                });
            }
        }
        public Command Btn_AddClick
        {
            get
            {
                return new Command(async () =>
                {
                    if (ItemList == null)
                    {
                        ItemList = new List<Item>();
                    }

                    var nameDialog = new InputDialog();
                    //팝업이 닫히고 난뒤 입력값의 처리
                    nameDialog.Disappearing += (s, e) =>
                    {
                        var code = "0";
                        var name = string.Empty;

                        if (ItemList.Count != 0)
                        {
                            code = ItemList.Count.ToString();
                        }

                        name = (nameDialog.BindingContext as InputDialogViewModel).Entry_Name;

                        //공백이 넘어온 경우 취소처리
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            return;
                        }

                        var list = new List<Item>(ItemList);

                        list.Add(new Item()
                        {
                            ItemCode = code,
                            CategoryCode = selectedCategory.CategoryCode,
                            ItemName = name,
                        });

                        ItemList = list;
                    };

                    await page.Navigation.PushModalAsync(nameDialog);
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

        #region [INotifyPropertyChanged]
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
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
