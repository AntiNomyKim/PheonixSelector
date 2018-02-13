using PheonixSelector.Models;
using PheonixSelector.Views;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace PheonixSelector.ViewModels
{
    class CategoryPageViewModel : INotifyPropertyChanged
    {
        public CategoryPageViewModel(ContentPage page)
        {
            this.page = page;

            LoadDataFromSQLite();
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
                    //UI상에서 삭제
                    var list = new List<Category>(CategoryList);
                    list.Remove(SelectedCategory);
                    CategoryList = list;

                    //SQLite 삭제
                    MainPageViewModel.Database.DeleteCategoryAsync(SelectedCategory);
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
                        var code = MainPageViewModel.Database.GetMaximumCategoryCode();
                        var name = string.Empty;

                        name = (nameDialog.BindingContext as InputDialogViewModel).Entry_Name;

                        //공백이 넘어온 경우 취소처리
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            return;
                        }

                        var newCategory = new Category()
                        {
                            CategoryCode = code,
                            CategoryName = name,
                        };

                        var list = new List<Category>(CategoryList);
                        list.Add(newCategory);
                        CategoryList = list;

                        //SQLite저장
                        MainPageViewModel.Database.SaveCategoryAsync(newCategory);
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

        #region [사용자정의 메소드]
        private void LoadDataFromSQLite()
        {
            CategoryList = MainPageViewModel.Database.GetCategoryListAsync().Result;
        }
        #endregion [사용자정의 메소드]
    }
}
