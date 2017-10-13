namespace Products.Models
{
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Windows.Input;
    using System;
    using Xamarin.Forms;
    using Products.View;
    using Products.ViewModels;

    public class Category
    {
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
        #region Methods

        async void SelectCategory()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Products = new ProductsViewModels(Products);
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsView());
        } 
        #endregion

    }
}
