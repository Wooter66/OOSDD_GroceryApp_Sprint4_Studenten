
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Grocery.App.ViewModels
{
    public partial class GroceryListViewModel : BaseViewModel
    {
        public ObservableCollection<GroceryList> GroceryLists { get; set; }
        private readonly IAuthService authService;
        private readonly IGroceryListService _groceryListService;
        private readonly IClientRepository _clientRepository;
        public Client Client { get; set; }
        public ICommand ShowBoughtProductsCommand { get; }

        public GroceryListViewModel(IGroceryListService groceryListService, IClientRepository clientRepository, IAuthService authService)
        {
            Title = "Boodschappenlijst";
            _groceryListService = groceryListService;
            authService = authService;
            GroceryLists = new(_groceryListService.GetAll());

            Client = authService.CurrentClient;

            ShowBoughtProductsCommand = new Command(async () => await ShowBoughtProducts());
        }

        [RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            Dictionary<string, object> paramater = new() { { nameof(GroceryList), groceryList } };
            await Shell.Current.GoToAsync($"{nameof(Views.GroceryListItemsView)}?Titel={groceryList.Name}", true, paramater);
        }
        public override void OnAppearing()
        {
            base.OnAppearing();
            GroceryLists = new(_groceryListService.GetAll());
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            GroceryLists.Clear();
        }

        private async Task ShowBoughtProducts()
        {
            if (Client?.Role == Roles.Admin)
            {
                await Shell.Current.GoToAsync(nameof(Views.BoughtProductsView));
            }
        }

    }
}
