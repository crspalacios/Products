namespace Products.Models
{
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Products.ViewModels;
    using Services;

    public class Category
    {
        #region Services

        NavigationService navigationService;

        #endregion

        #region Properties
        public int CategorId { get; set; }
        public string Description { get; set; }

        public List<Product> Products { get; set; } 
        #endregion

        #region Commands
        public ICommand SelectCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectCategory);
            }
 

        }
        #endregion

        #region Constructors
        public Category()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Methods

        async void SelectCategory()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Products = new ProductsViewModels(Products);

            await navigationService.Navigate("ProductsView");
           
        }
        #endregion

    }
}
