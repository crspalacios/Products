

namespace Products.ViewModels
{
    using System.Collections.Generic;
    using Products.Models;
    using System.ComponentModel;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ProductsViewModels : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        #region Attributes
        private List<Product> products;
        #endregion

        #region Attributes
        public ObservableCollection<Product> _products;
        #endregion

        #region Constructors
        public ProductsViewModels(List<Product> products)
        {
            this.products = products;
            Products = new ObservableCollection<Product>(products.OrderBy(p => p.Description));
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

        #endregion


    }
}
