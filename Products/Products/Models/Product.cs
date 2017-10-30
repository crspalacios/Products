namespace Products.Models
{
    using GalaSoft.MvvmLight.Command;
    using Products.Services;
    using Products.ViewModels;
    using System;
    using System.Windows.Input;

    public class Product
    {
        #region Attributes
        NavigationService navigationService;
        DialogService dialogService;
        #endregion

        #region Properties
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActivte { get; set; }

        public double Stock { get; set; }

        public DateTime LastEvent { get; set; }

        public string Remarks { get; set; }

        public string Image { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(Image))
                {
                    return "noimage";
                }
                return string.Format("http://productspalapi.azurewebsites.net/{0}",
                                      Image.Substring(1));
            }
        }
        #endregion

        #region Constructors
        public Product()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
        }
        #endregion

        #region Command
        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(Edit);

            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);

            }
        }

        async void Delete()
        {
            var response = await dialogService.ShowConfirm("Confirm", "Are you sure to delet this record?");
            if (!response)
            {
                return;
            }
            await ProductsViewModels.GetInstance().DeleteProduct(this);
        }
        #endregion

        #region Methods
        async void Edit()
        {
            MainViewModel.GetInstance().EditProduct = new EditProductViewModel(this);
            await navigationService.Navigate("EditProductView");
        }

        public override int GetHashCode()
        {
            return ProductId;
        }
        #endregion

    }
}
