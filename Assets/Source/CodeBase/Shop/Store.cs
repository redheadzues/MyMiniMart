using Assets.Source.CodeBase.Production;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.CodeBase.Shop
{
    public class Store
    {
        private Dictionary<ProductType, Shelf> _availableShelfs = new Dictionary<ProductType, Shelf>();
        private List<ProductType> _availableProducts = new List<ProductType>();
        private CheckOutService _checkoutService;

        public IReadOnlyList<ProductType> AvailableProducts => _availableProducts;

        public Shelf GetShelf(ProductType type) =>
            _availableShelfs.TryGetValue(type, out Shelf shelf) ? shelf : null;

        public bool HasFreePlace()
        {
            foreach (Shelf shelf in _availableShelfs.Values)
            {
                if (shelf.Server.Queue.HasFreePlace() == false)
                    return false;
            }

            return true;
        }

        public void AddShelf(Shelf shelf)
        {
            _availableShelfs.Add(shelf.Type, shelf);
            _availableProducts.Add(shelf.Type);
        }

        public void AddCheckOutService(CheckOutService checkOutService) => 
            _checkoutService = checkOutService;

        public CheckOutService GetCheckOutService() =>
            _checkoutService;
    }
}
