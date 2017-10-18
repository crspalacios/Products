


namespace Products.ViewModels
{
    using System.ComponentModel;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Services;
    using Models;
    using System;

    public class EditCategoryVewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogServices;
        NavigationService navigationService;
        #endregion

        #region Attributes
        bool _isRunning;
        bool _isEnabled;
        Category category;
        #endregion

        #region Propierties
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));

                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));

                }
            }
        }
        public string Description
        {

            set;
            get;

        }
        #endregion

        #region Constructor

        public EditCategoryVewModel(Category category)
        {
            this.category = category;


            apiService = new ApiService();
            dialogServices = new DialogService();
            navigationService = new NavigationService();

            Description = category.Description;
            IsEnabled = true;
            }
        #endregion

        #region Commands
        public ICommand SaveCategoryCommand
        {
            get
            {
                return new RelayCommand(SaveCategory);
            }
        }


        #endregion

        #region Methods
        async void SaveCategory()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await dialogServices.ShowMessage("Error", "Yeou must enter a category Description");
                return;
            }
            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogServices.ShowMessage("Error", connection.Message);
                return;
            }

            category.Description = Description;
            var mainViewModel = MainViewModel.GetInstance();
            var response = await apiService.Put("http://productspalapi.azurewebsites.net",
                                                  "/api",
                                                  "/Categories",
                                                  mainViewModel.Token.TokenType,
                                                  mainViewModel.Token.AccessToken,
                                                  category);
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogServices.ShowMessage("Errors", response.Message);
                return;
            }

            
            var categoriesViewModel = CategoriesViewModel.GetInstance();
            categoriesViewModel.UpdateCategory(category);

            await navigationService.Back();

            IsRunning = true;
            IsEnabled = false;
        }
        #endregion
    }





}
