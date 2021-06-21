using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryPridictor.Model
{
    public class Product
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public string Catagory { get; set; }
        public int StoreId { get; set; }
        public int UserId { get; set; }
        public string Link { get; set; }


    }
}
