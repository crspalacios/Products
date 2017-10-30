

namespace Products.ViewModels
{
    using System.Collections.Generic;
    using Products.Models;
    using System.ComponentModel;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Threading.Tasks;
    using Products.Services;

    public class ProductsViewModels : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        #region Attributes
        List<Product> products;
        bool _isRefreshing;
        public ObservableCollection<Product> _products;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Constructors
        public ProductsViewModels(List<Product> product)
        {
             instance = this;
;            this.products = product;

            Products = new ObservableCollection<Product>(products.OrderBy(p => p.Description));

            apiService = new ApiService();
            dialogService = new DialogService();
        }
        #endregion

        #region Properties
        public ObservableCollection<Product> Products
        {
            get
            {
                return _products;
            }
            set
            {
                if(_products != value)
                {
                    _products = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));

                }
            }
        }

        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));

                }
            }
        }
        #endregion


        #region Sigleton
        static ProductsViewModels instance;

        public static ProductsViewModels GetInstance()
        {
            return instance;
        }

        #endregion


        #region Methods
       
        public void Add(Product product)
        {
            IsRefreshing = true;
            Products.Add(product);
            IsRefreshing = false;

        }

        public void Update(Product product)
        {
            IsRefreshing = true;

            var oldProduct = products.Where(c => c.ProductId == product.ProductId).FirstOrDefault();
            oldProduct = product;
            Products = new ObservableCollection<Product>(products.OrderBy(c => c.Description));

            IsRefreshing = false;

        }

        public async Task DeleteProduct(Product product)
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }


            var mainViewModel = MainViewModel.GetInstance();
            var response = await apiService.Delete("http://productspalapi.azurewebsites.net",
                                                  "/api",
                                                  "/Categories",
                                                  mainViewModel.Token.TokenType,
                                                  mainViewModel.Token.AccessToken,
                                                  product);
            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Errors", response.Message);
                return;
            }

            products.Remove(product);
            Products = new ObservableCollection<Product>(products.OrderBy(c => c.Description));
            IsRefreshing = false;
        }
        #endregion

        #region Commands
        /* public ICommand RefreshCommand
         {
             get
             {

                 //return new RelayCommand();
             }

         }*/

        #endregion
    }
}
