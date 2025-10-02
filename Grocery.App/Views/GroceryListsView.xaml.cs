using Grocery.App.ViewModels;
using Grocery.Core.Data.Repositories;
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Microsoft.Maui.Controls;

namespace Grocery.App.Views;

public partial class GroceryListsView : ContentPage
{
    public GroceryListsView(GroceryListViewModel viewModel, IClientRepository clientRepository, IAuthService authService)
    {
        InitializeComponent();;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is GroceryListViewModel bindingContext)
        {
            bindingContext.OnAppearing();

        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is GroceryListViewModel bindingContext)
        {
            bindingContext.OnDisappearing();
        }
    }
}