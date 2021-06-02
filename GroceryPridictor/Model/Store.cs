using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryPridictor.Model
{
    public class Store
    {

        public int Id { get; set; }
        public string StoreName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int UserId { get; set; }
        public int StoreCategoryId { get; set; }
        public int Region { get; set; }

    }
}
