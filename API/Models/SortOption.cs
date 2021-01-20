using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortOption
    {
        Undefined = 0,
        [Description("High to Low Price")]
        High,
        [Description("Low to High Price")]
        Low,
        [Description("A - Z sort on the Name")]
        Ascending,
        [Description("Z - A sort on the Name")]
        Descending,
        [Description("ShopperHistory based on Popularity")]
        Recommended
    }
}
