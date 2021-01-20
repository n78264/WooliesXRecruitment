using API.Contracts;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class ShoppingTrolleyService : IShoppingTrolleyService
    {
        public decimal GetLowestTrolleyTotal(ShoppingTrolley request)
        {
            var trolleyTotalList = new List<decimal>();
            decimal total = 0;
            total = CalculateTotalPrice(request.Products, request.Quantities);
            trolleyTotalList.Add(total);

            foreach (var specialItem in request.Specials)
            {
                if (IsSepcialApplicable(specialItem, request.Quantities))
                {
                    var specialItemTotal = specialItem.Total;
                    var remainingItemsList = GetRemainingProductQuantity(specialItem, request.Quantities);
                    remainingItemsList = ApplySpecials(request.Specials, remainingItemsList, ref specialItemTotal);
                    specialItemTotal = CalculateTotalPrice(request.Products, remainingItemsList);
                    trolleyTotalList.Add(specialItemTotal);
                }
            }

            return trolleyTotalList.OrderBy(c => c).FirstOrDefault();
        }

        private IEnumerable<ProductQuantity> ApplySpecials(IEnumerable<Special> specials,
                    IEnumerable<ProductQuantity> remainingItemsList, ref decimal total)
        {
            foreach (var specialItem in specials)
            {
                if (IsSepcialApplicable(specialItem, remainingItemsList))
                {
                    total = total + specialItem.Total;
                    remainingItemsList = GetRemainingProductQuantity(specialItem, remainingItemsList);
                    while (IsSepcialApplicable(specialItem, remainingItemsList))
                    {
                        remainingItemsList = GetRemainingProductQuantity(specialItem, remainingItemsList);
                        total = total + specialItem.Total;
                    }
                }
            }
            return remainingItemsList;
        }

        private decimal CalculateTotalPrice(IEnumerable<TrolleyProduct> products,
            IEnumerable<ProductQuantity> quantities)
        {
            decimal total = 0;
            foreach (var itemsQuantity in quantities)
            {
                var purchasedItem = products.FirstOrDefault(p => p.Name == itemsQuantity.Name);
                if (purchasedItem == null)
                    continue;
                total = total + purchasedItem.Price * itemsQuantity.Quantity;
            }
            return total;
        }

        private bool IsSepcialApplicable(Special special, IEnumerable<ProductQuantity> purchaseItemQuantityList)
        {
            foreach (var purchaseItemQuantity in purchaseItemQuantityList)
            {
                var isThisSpecialValid = special.Quantities.Any(c =>
                    c.Name == purchaseItemQuantity.Name && c.Quantity > purchaseItemQuantity.Quantity);
                if (isThisSpecialValid)
                {
                    return false;
                }
            }
            return true;
        }

        private IEnumerable<ProductQuantity> GetRemainingProductQuantity(Special special,
                                             IEnumerable<ProductQuantity> productQuantity)
        {
            return productQuantity.Select(x => new ProductQuantity
            {
                Name = x.Name,
                Quantity = x.Quantity - special.Quantities.FirstOrDefault(q => q.Name == x.Name)?.Quantity ?? 0
            });
        }
    }
}
