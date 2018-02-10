using PheonixSelector.Models;
using PheonixSelector.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace PheonixSelector.ViewModels
{
    class CategoryPageViewModel : INotifyPropertyChanged
    {
        public CategoryPageViewModel(ContentPage page)
        {
            this.page = page;

            //TODO Local 저장 되는대로 삭제할것
            var test = new List<Category>()
            {
                new Category()
                {
                    CategoryCode = "0",
                    CategoryName = "test1",
                    ItemList = null
                },
                new Category()
                {
                    CategoryCode = "1",
                    CategoryName = "test2",
                    ItemList = null
                }
            };
            CategoryList = test;
        }

        private ContentPage page;
        private List<Category> categoryList;
        private Category selectedCategory;

        public List<Category> CategoryList
        {
            get
            {
                return categoryList;
            }
            set
            {
                categoryList = value;
                OnPropertyChanged(nameof(CategoryList));
            }
        }
        public Category SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        #region [Commands]
        public Command Btn_DelClick
        {
            get
            {
                return new Command(() =>
                {
                    var list = new List<Category>(CategoryList);
                    list.Remove(SelectedCategory);
                    CategoryList = list;
                });
            }
        }
        public Command Btn_AddClick
        {
            get
            {
                return new Command(async () =>
                {
                    if (CategoryList == null)
                    {
                        CategoryList = new List<Category>();
                    }

                    var nameDialog = new InputDialog();
                    //팝업이 닫히고 난뒤 입력값의 처리
                    nameDialog.Disappearing += (s, e) =>
                    {
                        var code = "0";
                        var name = string.Empty;

                        if (CategoryList.Count != 0)
                        {
                            code = CategoryList.Count.ToString();
                        }

                        name = (nameDialog.BindingContext as InputDialogViewModel).Entry_Name;

                        //공백이 넘어온 경우 취소처리
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            return;
                        }

                        var list = new List<Category>(CategoryList);

                        list.Add(new Category()
                        {
                            CategoryCode = code,
                            CategoryName = name,
                            ItemList = null
                        });

                        CategoryList = list;
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

        #region [INotyfyPropertyChanged]
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion [INotyfyPropertyChanged]
    }
}
