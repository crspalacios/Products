namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using Services;

    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        string _email;
        string _password;
        bool _isToggled;
        bool _isRunning;
        bool _isEnabled;
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
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if(_email !=value )
                {
                    _email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
                   
                }
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));

                }
            }
        }
        public bool IsToggled
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsToggled)));

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

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            apiService = new ApiService();
            dialogServices = new DialogService();
            navigationService = new NavigationService();
            Email = "cpalacios@crealodigital.com";
            Password = "123456";

            IsEnabled = true;
            IsToggled = true;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }
        #endregion

        #region Methods
        async void Login()
        {
            if(string.IsNullOrEmpty(Email))
            {
                await dialogServices.ShowMessage("Error", "You  must enter an email.");
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await dialogServices.ShowMessage("Error", "You  must enter an password.");
                return;
            }
            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if(!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogServices.ShowMessage("Error", connection.Message);
                return;
            }

            var response = await apiService.GetToken("http://productspalapi.azurewebsites.net", Email, Password);
            if(response == null)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogServices.ShowMessage("Error", "The service is not available, please try latter.");
                Password = null;
                return;
            }
            if (string.IsNullOrEmpty(response.AccessToken))
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogServices.ShowMessage("Error", response.ErrorDescription);
                Password = null;
                return;

            }
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Categories = new CategoriesViewModel();
            mainViewModel.Token = response;
            await navigationService.Navigate("CategoriesView");
            Email = null;
            Password = null;
            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }




}

    
