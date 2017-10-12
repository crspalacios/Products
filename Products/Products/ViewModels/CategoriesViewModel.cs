using Products.Models;
using System;
using System.Collections.ObjectModel;

namespace Products.ViewModels
{
    public class CategoriesViewModel
    {

        #region Constructors
        public CategoriesViewModel()
        {
            LoadCategories();
        }
        #endregion

        #region Methods
        private void LoadCategories()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Properties
        public ObservableCollection<Category> Categories
        {
            get;
            set;
        }

        #endregion
    }

}
