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
        public IActionResult AddStore([FromQuery] Store store )
            {
            try
            {
                var store1 = context.Store.Where(s => s.StoreName == store.StoreName).FirstOrDefault();
                if (store1 == null)
                {
                    context.Store.Add(store);
                    context.SaveChanges();
                    return Ok("Store Added Successfully");
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
        public IActionResult GetAllProducts(int OwnerId)
        {
            try
            {
                var person = context.User.Where(s => s.Id == OwnerId).FirstOrDefault();
                if (person != null) {    
                    return Ok(context.Product.Where(s => s.UserId == OwnerId).ToList());
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
                    var v = context.Product.Where(s => s.Id == productid&& s.StoreId == storeId).FirstOrDefault();
                    if (v != null)
                    {
                        context.Product.Remove(v);
                        context.SaveChanges();
                        return Ok("Product Deleted Successfully.");

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
            catch(Exception ex)
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
                    var v = context.Product.Where(s => s.UserId == product.UserId && s.Name == product.Name).FirstOrDefault();
                    if (v == null) {
                        context.Product.Add(product);
                        context.SaveChanges();
                        return Ok("Product Saved Successfully.");
                    }
                    else
                    {
                        return Error("Product already Exists.");
                    }
                }
                else
                {
                    return Error("No User found with id : "+product.UserId);
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
        public IActionResult GetAllStores([FromQuery]int OwnerId)
        {
            try
            {   
                var person = context.User.Where(s => s.Id == OwnerId).FirstOrDefault();
                if (person != null)
                {
                    return Ok(context.Store.Where(s => s.UserId == OwnerId).ToList());
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
                 var v =context.Product.Select(x => new StoreCategory
                {
                    Id = x.Id,
                    Category= x.Catagory
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
    }
}
