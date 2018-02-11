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
            ItemList = GetItemList(selectedCategory.CategoryCode);
            CheatedItem = null;
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

        public Command Click_BtnDel
        {
            get
            {
                return new Command(() =>
                {
                    //UI상에서 제거
                    var list = new List<Item>(ItemList);
                    list.Remove(SelectedItem);
                    ItemList = list;

                    //SQLite 제거
                    MainPageViewModel.Database.DeleteItemAsync(SelectedItem);
                });
            }
        }

        public Command Click_BtnAdd
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
                        var code = MainPageViewModel.Database.GetMaximumItemCode();
                        var name = string.Empty;

                        name = (nameDialog.BindingContext as InputDialogViewModel).Entry_Name;

                        //공백이 넘어온 경우 취소처리
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            return;
                        }

                        var item = new Item()
                        {
                            ItemCode = code,
                            CategoryCode = selectedCategory.CategoryCode,
                            ItemName = name,
                        };

                        var list = new List<Item>(ItemList);
                        list.Add(item);
                        ItemList = list;

                        //SQLite저장
                        MainPageViewModel.Database.SaveItemAsync(item);
                    };

                    await page.Navigation.PushModalAsync(nameDialog);
                });
            }
        }

        public Command Click_BtnRtn
        {
            get
            {
                return new Command(() =>
                {
                    page.Navigation.PopModalAsync();
                });
            }
        }

        public Command ActivateCheating
        {
            get
            {
                return new Command((s) =>
                {
                    CheatedItem = SelectedItem;
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

        #region [사용자 정의 메소드]

        private List<Item> GetItemList(string categoryCode)
        {
            return MainPageViewModel.Database.GetItemListNotDoneAsync(categoryCode).Result;
        }

        #endregion [사용자 정의 메소드]
    }
}
