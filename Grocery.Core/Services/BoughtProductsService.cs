﻿
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class BoughtProductsService : IBoughtProductsService
    {
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroceryListRepository _groceryListRepository;
        public BoughtProductsService(IGroceryListItemsRepository groceryListItemsRepository, IGroceryListRepository groceryListRepository, IClientRepository clientRepository, IProductRepository productRepository)
        {
            _groceryListItemsRepository=groceryListItemsRepository;
            _groceryListRepository=groceryListRepository;
            _clientRepository=clientRepository;
            _productRepository=productRepository;
        }
        public List<BoughtProducts> Get(int? productId)
        {
            var result = new List<BoughtProducts>();

            if (productId == null)
                return result;

            var items = _groceryListItemsRepository.GetAll()
                          .Where(i => i.ProductId == productId.Value);

            foreach (var item in items)
            {
                var list = _groceryListRepository.Get(item.GroceryListId);
                if (list == null) continue;

                var client = _clientRepository.Get(list.ClientId);
                if (client == null) continue;

                var product = _productRepository.Get(item.ProductId);
                if (product == null) continue;

                result.Add(new BoughtProducts(client, list, product));
            }

            return result;
        }

        public IEnumerable<object> GetByProductId(int id)
        {
            return Get(id);
        }
    }
}
