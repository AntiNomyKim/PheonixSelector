using PheonixSelector.Models;
using PheonixSelector.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PheonixSelector.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private Category selectedCategory;
        private Item selectedItem;
        private ContentPage page;
        private Item cheatedItem;

        public MainPageViewModel(ContentPage page)
        {
            this.page = page;
            cheatedItem = null;
        }

        public string SelectedCategoryName
        {
            get
            {
                if (selectedCategory == null)
                {
                    return "Selected Category Name";
                }
                return selectedCategory.CategoryName;
            }

            set
            {
                SelectedCategoryName = value;
                OnPropertyChanged(nameof(SelectedCategoryName));
            }
        }
        public string SelectedItemName
        {
            get
            {
                if (selectedCategory == null)
                {
                    return "Selected Item Name";
                }

                return selectedItem.ItemName; 
            }
            set
            {
                SelectedItemName = value;
                OnPropertyChanged(nameof(SelectedItemName));
            }
        }

        #region [Commands]
        public Command Click_BtnRun
        {
            get
            {
                return new Command(() =>
                {
                    selectedItem = GetRandomItem();
                    SelectedItemName = selectedItem.ItemName;
                    cheatedItem = null;
                });
            }
        }

        public Command Click_BtnCategoryList
        {
            get
            {
                return new Command(async () =>
                {
                    var categoryPage = new CategoryPage();
                    await page.Navigation.PushModalAsync(categoryPage);
                });
            }
        }

        public Command Click_BtnItemList
        {
            get
            {
                return new Command(() =>
                {
                    page.DisplayAlert("", "BTN_Item CLICKED", "Cancel");
                });
            }
        }
        #endregion [Commands]


        #region [사용자정의 메소드]
        //미사용
        private Item GetSelectedItem(string itemName)
        {
            Item item = null;
            if (selectedCategory != null)
            {
                IEnumerable<Item> items = selectedCategory.ItemList.Where((x) => x.ItemName.Equals(itemName));
                if (items.Count() > 0)
                {
                    var i = 0;
                    if (items.Count() > 1)
                    {
                        //조회한 아이템 이름이 중복되는 경우의 처리
                    }
                    item = items.ToList()[i];
                }
            }

            return item;
        }
        //미사용
        private Category GetCategorybyName(string categoryName)
        {
            Category category = null;
            
            return category;
        }
         
        private Item GetRandomItem()
        {
            var r = new Random(DateTime.Now.TimeOfDay.Seconds);
            List<Item> Items = selectedCategory.ItemList;

            //확률상승의 경우 50% 확률로 선택된 아이템을 출현
            if (cheatedItem != null)
            {
                if(Convert.ToInt16(r.Next() % 2) == 0)
                {
                    return cheatedItem;
                }
            }
            
            int idx = Convert.ToInt16(r.Next() % Items.Count);

            return Items[idx];
        }

        #endregion [사용자정의 메소드]


        #region [INotifyPropertyChanged 구현]
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion [INotifyPropertyChanged 구현]
    }
}
