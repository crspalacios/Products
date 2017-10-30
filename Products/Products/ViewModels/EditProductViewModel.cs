namespace Products.ViewModels
{
    using System.ComponentModel;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Services;
    using Models;
    using System;

    public class EditProductViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        bool _isRunning;
        bool _isEnabled;
        bool _isToggled;
        string _price;
        string _stock;
        Product product;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogServices;
        NavigationService navigationService;
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
        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Price)));

                }
            }
        }

        public bool IsActive
        {
            get
            {
                return _isToggled;
            }
            set
            {
                if (_isToggled != value)
                {
                    _isToggled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsActive)));

                }
            }
        }
        public string Stock
        {
            get
            {
                return _stock;
            }
            set
            {
                if (_stock != value)
                {
                    _stock = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Price)));

                }
            }
        }
        public DateTime LastEvent
        {
            get;
            set;
        }

        public string Remarks
        {
            get;
            set;
        }

        public string Image
        {
            get;
            set;
        }

        #endregion

        #region Constructor
        public EditProductViewModel(Product product)
        {
            this.product = product;

            Description = product.Description;
            LastEvent = product.LastEvent;
            Price = product.Price.ToString();
            Remarks = product.Remarks;
            Stock = product.Stock.ToString();
            Image = "noimage";

            apiService = new ApiService();
            dialogServices = new DialogService();
            navigationService = new NavigationService();

            Image = "noimage";
            IsActive = true;
            LastEvent = DateTime.Today;
            IsEnabled = true;
        }
        #endregion
        #region Command
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }


        #endregion

        #region Methods
        async void Save()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await dialogServices.ShowMessage("Error", "you must a Description");
                return;
            }


            if (string.IsNullOrEmpty(Price))
            {
                await dialogServices.ShowMessage("Error", "you must a price");
                return;
            }


            var price = decimal.Parse(Price);
            if (price < 0)
            {
                await dialogServices.ShowMessage("Error", "The price must be a value grathher or equals than zero.");
                return;
            }


            if (string.IsNullOrEmpty(Stock))
            {
                await dialogServices.ShowMessage("Error", "You must enter a product stock.");
                return;
            }


            var stock = double.Parse(Stock);
            if (stock < 0)
            {
                await dialogServices.ShowMessage("Error", "The stock must be a value greather or equals than zero.");
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

            var mainViewModel = MainViewModel.GetInstance();
            var product = new Product
            {
                CategoryId = mainViewModel.Category.CategoryId,
                Description = Description,
                IsActivte = IsActive,
                LastEvent = LastEvent,
                Price = price,
                Remarks = Remarks,
                Stock = stock,
                
            };

            var response = await apiService.Put("http://productspalapi.azurewebsites.net",
                                      "/api",
                                      "/Products",
                                      mainViewModel.Token.TokenType,
                                      mainViewModel.Token.AccessToken,
                                      product);
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogServices.ShowMessage("Error", response.Message);
                return;
            }

            var productsViewModels = ProductsViewModels.GetInstance();
            productsViewModels.Update(product);

            await navigationService.Back();

            IsRunning = true;
            IsEnabled = false;

        }
        #endregion
    }
}
