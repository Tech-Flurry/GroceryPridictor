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
                CustomResponseModel<Store> model = new();
                var store1 = context.Store.Where(s => s.StoreName == store.StoreName).FirstOrDefault();
                if (store1 == null)
                {
                    context.Store.Add(store);
                    context.SaveChanges();
                    
                    model.Message = "Store Added Successfully";
                    model.Status = true;
                    return Ok(model);
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
                GetAllProductsClass model = new GetAllProductsClass();
                var person = context.User.Where(s => s.Id == OwnerId).FirstOrDefault();
                if (person != null) {

                    model.Data = context.Product.Where(s => s.UserId == OwnerId).ToList();     
                    model.Message = "Operation Successfull.";
                    model.Status = true;
                    return Ok(model);
                }
                else {
                    model.Data = null;
                    model.Message = "Can't get product list.";
                    model.Status = false;
                    return Ok(model);
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
                CustomResponseModel<Product> model = new();
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
                GetAllStoresClass model = new GetAllStoresClass();
                var person = context.User.Where(s => s.Id == OwnerId).FirstOrDefault();
                if (person != null)
                {
                    model.Data = context.Store.Where(s => s.UserId == OwnerId).ToList();
                    model.Message = "Operation Successfull.";
                    model.Status = true;
                    return Ok(model);
                }
                else
                {
                    model.Data = null;
                    model.Message = "User with this id does not exists.";
                    model.Status = false;
                    return Ok(model);
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
                CustomResponseModel<List<StoreCategory>> model = new();
                
                    model.Data = context.StoreCategory.ToList();
                if (model.Data != null) { 
                    model.Message = "Operation Successfull.";
                    model.Status = true;
                    return Ok(model);
                }
                else
                {
                    model.Data = null;
                    model.Message = "No Store Category found in database.";
                    model.Status = false;
                    return Ok(model);
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
                CustomResponseModel<List<StoreCategory>> model = new ();
                model.Data=context.Product.Select(x => new StoreCategory
                {
                    Id = x.Id,
                    Category= x.Catagory
                }).ToList();
              
                if (model.Data != null)
                {
                    model.Message = "Operation Successfull.";
                    model.Status = true;
                    return Ok(model);
                }
                else
                {
                    model.Data = null;
                    model.Message = "No Product Category found in database.";
                    model.Status = false;
                    return Ok(model);
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
