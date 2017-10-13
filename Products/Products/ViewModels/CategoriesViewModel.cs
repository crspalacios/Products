
namespace Products.ViewModels
{
    using Products.Models;
    using System;
    using System.Collections.ObjectModel;
    using Services;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoriesViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
       
        #region Attributes
        public ObservableCollection<Category> _categories;
        #endregion

        #region Services
        ApiService  apiService;
        DialogService dialogService;
        #endregion

        #region Constructors
        public CategoriesViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            LoadCategories();
        }

        #endregion

        #region Methods
        async void LoadCategories()
        {
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<Category>("http://productspalapi.azurewebsites.net", 
                                                              "/api",
                                                              "/Categories", 
                                                              mainViewModel.Token.TokenType, 
                                                              mainViewModel.Token.AccessToken);

            if(!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
            }

            var categories = (List<Category>)response.Result;
            CategoriesList = new ObservableCollection<Category>(categories.OrderBy(c => c.Description));
        }
        #endregion

        #region Properties
        public ObservableCollection<Category> CategoriesList
        {
            get
            {
                return _categories;
            }
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CategoriesList)));

                }
            }
        }

        #endregion
    }

}
