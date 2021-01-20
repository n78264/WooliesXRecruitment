using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class IEnumerableExt
    {
        public static IEnumerable<Product> Sort(this IEnumerable<Product> sequenceToBeSorted,
                            SortOption parsedSortOption)
        {
            IEnumerable<Product> result;
            switch (parsedSortOption)
            {
                case SortOption.High:
                    result = sequenceToBeSorted.OrderByDescending(p => p.Price).ToList();
                    break;
                case SortOption.Low:
                    result = sequenceToBeSorted.OrderBy(p => p.Price).ToList();
                    break;
                case SortOption.Ascending:
                    result = sequenceToBeSorted.OrderBy(n => n.Name).ToList();
                    break;
                case SortOption.Descending:
                    result = sequenceToBeSorted.OrderByDescending(n => n.Name).ToList();
                    break;
                case SortOption.Recommended:
                    result = sequenceToBeSorted.OrderByDescending(n => n.PopularityRank).ToList();
                    break;
                default:
                    result = sequenceToBeSorted;
                    break;
            }

            return result;
        }
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return true;
            return !enumerable.Any();
        }
    }
}
