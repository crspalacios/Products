namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Products.Models;
    using System.Windows.Input;
    using System;
    using Services;

    public class MainViewModel
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Properties
        public LoginViewModel Login
        {
            get; set;
        }

        public CategoriesViewModel Categories
        {
            get;
            set;
        }
        public TokenResponse Token
        {
            get;
            set;
        }
        public ProductsViewModels Products
        {
            get;
            set;
        }

        public NewCategoryViewModels NewCategory
        {
            get;
            set;
        }

        public EditCategoryVewModel EditCategory
        {
            get;
            set;
        }

        public NewProductViewModel NewProduct
        {
            get;
            set;
        }
        public Category Category
        {
            get;
            set;
        }
        public Product Product
        {
            get;
            set;
        }
        public EditProductViewModel EditProduct
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();
            navigationService = new NavigationService();
        }
        #endregion

        #region Sigleton
        static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if(instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Commands
        public ICommand NewCategoryCommand
        {
            get
            {
                return new RelayCommand(GoNewCategory);
            }
          
        }

        public ICommand NewProductCommand
        {
            get
            {
                return new RelayCommand(GoNewProduct);
            }
        }

         #endregion

        #region Events
        async void GoNewCategory()
        {
            NewCategory = new NewCategoryViewModels();
            await navigationService.Navigate("NewCategoryView");
        }

        async void GoNewProduct()
        {
            NewProduct = new NewProductViewModel();
            await navigationService.Navigate("NewProductView");
        }
        #endregion
    }
}
