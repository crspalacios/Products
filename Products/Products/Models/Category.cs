namespace Products.Models
{
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Products.ViewModels;
    using Services;
    using System;

    public class Category
    {
        #region Services

        NavigationService navigationService;
        DialogService dialogService;

        #endregion

        #region Properties
        public int CategoryId { get; set; }
        public string Description { get; set; }

        public List<Product> Products { get; set; } 
        #endregion

        #region Commands
        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }



        public ICommand SelectCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectCategory);
            }
 

        }
        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(Edit);
            }
        }
        #endregion


        #region Constructors
        public Category()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
        }
        #endregion

        #region Methods
        async void Delete()
        {
            var response = await dialogService.ShowConfirm("Confirm", "Are you sure to delet this record?");
            if(!response)
            {
                return;
            }
            await CategoriesViewModel.GetInstance().DeleteCategory(this);
        }

        async void SelectCategory()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Products = new ProductsViewModels(Products);

            await navigationService.Navigate("ProductsView");
           
        }
        async void Edit()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditCategory = new EditCategoryVewModel(this);
            await navigationService.Navigate("EditCategoryView");


        }
        public override int GetHashCode()
        {
            return CategoryId;
        }

        #endregion

    }


}
