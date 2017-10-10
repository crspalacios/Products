namespace Products.ViewModels
{
    using System.ComponentModel;
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        string _email;
        #endregion

        #region Propierties
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
        #endregion

        #region Constructor
        public LoginViewModel()
        {
        } 
        #endregion
    }
}

    
