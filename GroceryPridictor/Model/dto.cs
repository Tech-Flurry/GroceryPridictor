using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryPridictor.Model
{
    public class dto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public string Catagory { get; set; }
        public int StoreId { get; set; }
        public int OwnerId { get; set; }
    }

    public class GetAllProductsClass {
        public bool Status { get; set; }
        public List<Product> Data { get; set; }
        public string Message { get; set; }
    }
    public class GetAllStoresClass
    {
        public bool Status { get; set; }
        public List<Store> Data { get; set; }
        public string Message { get; set; }
    }
    public class GetProductCategories
    {
        public bool Status { get; set; }
        public List<GetProductCategoryModel> Data { get; set; }
        public string Message { get; set; }
    }
    public class GetProductCategoryModel {
        public int Id { get; set; }
        public string Catagory { get; set; }
    }
    public class getNearByProductCategoryModel
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public string Link { get; set; }
        public string Catagory { get; set; }
        public int Region { get; set; }
    }
    public class getallstoredto {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; }
        public int Region { get; set; }



    }
    public class storenewModel {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int UserId { get; set; }
        public int StoreCategoryId { get; set; }
        public int Region { get; set; }
        public string Category { get; set; }

    }

}
