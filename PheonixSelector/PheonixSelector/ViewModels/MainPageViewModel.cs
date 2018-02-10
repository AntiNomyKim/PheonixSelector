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
        public MainPageViewModel(ContentPage page)
        {
            this.page = page;
            cheatedItem = null;
            selectedCategory = null;
        }

        private Category selectedCategory;
        private Item selectedItem;
        private ContentPage page;
        private Item cheatedItem;

        public string SelectedCategoryName
        {
            get
            {
                if (selectedCategory == null)
                {
                    return "Select Category";
                }
                return selectedCategory.CategoryName;
            }

            set
            {
                OnPropertyChanged(nameof(SelectedCategoryName));
            }
        }
        public string SelectedItemName
        {
            get
            {
                if (selectedCategory == null)
                {
                    return "Select Item";
                }

                return selectedItem.ItemName; 
            }
            set
            {
                OnPropertyChanged(nameof(SelectedItemName));
            }
        }

        #region [Commands]
        public Command Click_BtnRun
        {
            get
            {
                return new Command(async () =>
                {
                    selectedItem = GetRandomItem();
                    if (selectedItem != null)
                    {
                        SelectedItemName = selectedItem.ItemName;
                    }
                    else
                    {
                        if (selectedCategory == null)
                        {
                            await page.DisplayAlert("Warning", "Select Category First", "OK");
                        }
                        else
                        {
                            await page.DisplayAlert("Warning", "No Items. Check Item List", "OK");
                        }
                    }
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
                    categoryPage.Disappearing += (s, e) =>
                    {
                        selectedCategory = (categoryPage.BindingContext as CategoryPageViewModel).SelectedCategory;

                        SelectedCategoryName = (selectedCategory != null) ? selectedCategory.CategoryName : string.Empty;
                    };

                    await page.Navigation.PushModalAsync(categoryPage);
                });
            }
        }

        public Command Click_BtnItemList
        {
            get
            {
                return new Command(async () =>
                {
                    if (selectedCategory == null)
                    {
                        await page.DisplayAlert("Warning", "Select Category First", "OK");
                        return;
                    }
                    var itemPage = new ItemPage(selectedCategory);
                    itemPage.Disappearing += (s, e) =>
                    {
                        selectedCategory.ItemList = (itemPage.BindingContext as ItemPageViewModel).ItemList;
                    };

                    await page.Navigation.PushModalAsync(itemPage);
                });
            }
        }
        #endregion [Commands]


        #region [사용자정의 메소드]
        private Item GetRandomItem()
        {
            if (selectedCategory == null || selectedCategory.ItemList == null)
            {
                return null;
            }

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
