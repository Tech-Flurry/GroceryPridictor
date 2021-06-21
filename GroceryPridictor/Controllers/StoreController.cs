using GroceryPridictor.Infrastructure;
using GroceryPridictor.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroceryPridictor.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StoreController : CustomControllerBase
    {
        private readonly GroceryContext context;
        // GET: api/<StoreController>

        public StoreController(GroceryContext _context)
        {
            context = _context;
        }
        [HttpGet]
        [ProducesResponseType(200, StatusCode = 200, Type = typeof(Store))]

        #region Add Store
        [HttpPost]
        public IActionResult AddStore([FromQuery] Store store)
        {
            try
            {
                var store1 = context.Store.Where(s => s.StoreName == store.StoreName).FirstOrDefault();
                if (store1 == null)
                {
                    context.Store.Add(store);
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return Error("You Already have a store with Name :" + store.StoreName);
                }
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion

        #region Get Product List by User Id
        [HttpGet]
        public IActionResult GetAllProducts(int OwnerId, int storeId)
        {
            try
            {
                var person = context.User.Where(s => s.Id == OwnerId).FirstOrDefault();
                if (person != null) {
                    var product =(from dd in  context.Product.Where(s => s.UserId == OwnerId && s.StoreId == storeId) join 
                                  tt in context.productModel on dd.ProductId equals tt.Id
                                  select new { 
                                  dd.Id,
                                  dd.Link,
                                  dd.Price,
                                  dd.Stock,
                                  dd.StoreId,
                                  dd.UserId,
                                  dd.ProductId,
                                  dd.Catagory,
                                  tt.ProductName
                                  });
                           
                    return Ok(product);
                }
                else {
                    return Error("Can't get product list.");
                }
            }
            catch (Exception ex) {
                return Error(ex.Message);
            }
        }
        #endregion

        #region Delete Product

        [HttpPost]
        public IActionResult DeleteProduct([FromQuery] int productid, int UserId, int storeId)
        {
            try
            {
                CustomResponseModel<Product> model = new();
                var person = context.User.Where(s => s.Id == UserId).FirstOrDefault();
                if (person != null)
                {
                    var v = context.Product.Where(s => s.Id == productid && s.StoreId == storeId).FirstOrDefault();
                    if (v != null)
                    {
                        context.Product.Remove(v);
                        context.SaveChanges();
                        return Ok();

                    }
                    else
                    {
                        return Error("No product found with Id : " + productid);
                    }
                }
                else
                {
                    return Error("No User found with id : " + UserId);
                }
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }


        }
        #endregion

        #region Add Product  
        [HttpPost]
        public IActionResult AddProduct([FromQuery] Product product)
        {
            try
            {
                var person = context.User.Where(s => s.Id == product.UserId).FirstOrDefault();
                if (person != null)
                {
                    var v = context.Product.Where(s => s.UserId == product.UserId && s.ProductId == product.ProductId).FirstOrDefault();
                    if (v == null) {
                        context.Product.Add(product);
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return Error("Product already Exists.");
                    }
                }
                else
                {
                    return Error("No User found with id : " + product.UserId);
                }
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }


        }
        #endregion

        #region ShopList
        [HttpGet]
        public IActionResult GetAllStores(int OwnerId)
        {
            try
            {
                var person = context.User.Where(s => s.Id == OwnerId).FirstOrDefault();
                if (person != null)
                {
                    //var store = context.Store.Where(s => s.UserId == OwnerId).ToList();
                    var store2 = (from st in context.Store
                                  join
                                    stc in context.StoreCategory
                                         on st.StoreCategoryId equals stc.Id
                                  select new getallstoredto
                                  {
                                      Id = st.Id,
                                      StoreName = st.StoreName,
                                      Region = st.Region,
                                      Latitude = st.Latitude,
                                      Longitude = st.Longitude,
                                      Category = stc.Category,
                                      UserId = st.UserId
                                  }).Where(s => s.UserId == OwnerId).ToList();

                    return Ok(store2);
                }
                else
                {
                    return Error("User with this id does not exists.");
                }
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion

        #region getAllStoreCategories
        [HttpGet]
        public IActionResult GetStoreCategories()
        {
            try
            {
                var v = context.StoreCategory.ToList();
                if (v != null) {
                    return Ok(v);
                }
                else
                {
                    return Error("No Store Category found in database.");
                }
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion

        #region Product Categories List
        [HttpGet]
        public IActionResult GetProductCategories()
        {
            try
            {
                var v = context.Product.Select(x => new StoreCategory
                {
                    Id = x.Id,
                    Category = x.Catagory
                }).ToList();

                if (v != null)
                {
                    return Ok(v);
                }
                else
                {
                    return Ok("No Product Category found in database.");
                }
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion



        #region getProductsName
        [HttpGet]
        public IActionResult getproductNames()
        {
            try
            {
                var products = context.productModel.ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion
    }
}
