

namespace Products.Services
{
    using Products.View;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class NavigationService
    {
        public async Task Navigate(string pageName)
        {
            switch (pageName)
            {
                case "CategoriesView":
                    await Application.Current.MainPage.Navigation.PushAsync(new CategoriesView());
                    break;

                case "ProductsView":
                    await Application.Current.MainPage.Navigation.PushAsync(new ProductsView());
                    break;

                case "NewCategoryView":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewCategoryView());
                    break;

            }
            
        }
    }
}
