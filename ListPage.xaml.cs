using Aron_Andreea_Lab7.Models;
namespace Aron_Andreea_Lab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        if(listView.SelectedItem != null)
        {
            var selected_Product = listView.SelectedItem as Product;
            var all_products = await App.Database.GetAllProducts();
            var toDelete = all_products.Find(x => x.ProductID == selected_Product.ID && x.ShopListID == slist.ID);
        
            if(toDelete != null)
            {
                await App.Database.DeleteListProductAsync(toDelete);
                await Navigation.PopAsync();
            }
        }
        
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)
       this.BindingContext)
        {
            BindingContext = new Product()
        });

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }


}