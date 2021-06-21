﻿using GroceryPridictor.Infrastructure;
using GroceryPridictor.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryPridictor.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : CustomControllerBase
    {
        private readonly GroceryContext context;
        public CustomerController(GroceryContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<ActionResult> GetShops(int UserId, string searchResult) {
            try
            {
                CustomResponseModel<List<Store>> model = new();
                int regio = 4;
                var person = context.User.Where(u => u.Id == UserId).FirstOrDefault();
                if (person != null && person.Latitude != null && person.Longitude != null)
                {
                    regio =  getRegion.getRegionFun(person.Latitude, person.Longitude);
                    var prod = await (from st in context.Store
                                join pr in context.Product
                               on st.Id equals pr.StoreId
                                select new Store
                                {   Id =st.Id,
                                    StoreName = st.StoreName,
                                    StoreCategoryId = st.StoreCategoryId,    
                                    Latitude = st.Latitude,
                                    Longitude = st.Longitude,
                                    UserId = st.UserId,
                                    Region = st.Region
                                }).Where(x => x.Region == regio).Distinct().ToListAsync();

             var prod2 = (from st in prod
                     join pp in context.StoreCategory
            on st.StoreCategoryId equals pp.Id
                     select new storenewModel
                     {
                         Id = st.Id,
                         StoreName = st.StoreName,
                         StoreCategoryId = st.StoreCategoryId,
                         Latitude = st.Latitude,
                         Longitude = st.Longitude,
                         UserId = st.UserId,
                         Region = st.Region,
                         Category = pp.Category
                     }).ToList();
                            

                    if (prod2 != null)
                    {
                        if (!String.IsNullOrEmpty(searchResult))
                        {
                            string searchMatch = searchResult.Trim().ToLower();
                            object v = prod2.Where(e => e.StoreName.ToLower().Contains(searchMatch)).ToList();
                           
                            return Ok(v);
                        }
                        else {
                            return Ok(prod2);                
                        }
                        
                    }
                    else
                    {
                        return Error("No Products avaliable in the area.");
                    }
                }
                else
                {
                    return Error("User does not exists.");
                }
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }

        } 
        //[HttpGet]
        //public ActionResult getNearByProducts(int UserId) {
        //    try
        //    {
        //        int regio=4;
        //        var person = context.User.Where(u => u.Id == UserId).FirstOrDefault();
        //        if (person != null && person.Latitude != null && person.Longitude != null) {
        //            regio =  getRegion.getRegionFun(person.Latitude,person.Longitude);
                
        //        var prod = (from st in context.Store
        //                     join pr in context.Product
        //                    on st.Id equals pr.StoreId
        //                     select new getNearByProductCategoryModel 
        //                     {              
        //                         Name  = pr.Name,
        //                         Price = pr.Price,
        //                         Stock = pr.Stock,
        //                         Link  = pr.Link,
        //                         Catagory = pr.Catagory,
        //                         Region = st.Region
        //                     }).Where(x => x.Region == regio).ToList();
        //            if (prod != null)
        //            {
        //                return Ok(prod);
        //            }
        //            else {
        //                return Error("No Products avaliable in the area.");
        //            }
        //        }                
        //        else{
        //            return Error("User does not exists.");
        //        }
        //    }
        //    catch(Exception ex) {
        //        return Error(ex.Message);
        //    }
        //}


        [HttpGet]
        public ActionResult getProductByStoreId(int StoreID)
        {
            try
            {
                var store = context.Store.Where(u => u.Id == StoreID).FirstOrDefault();
                if (store != null)
                {
                   List<Product> products = context.Product.Where(x => x.StoreId == StoreID).ToList();
                    if (products != null)
                    {
                        return Ok(products);
                    }
                    else
                    {
                        return Error("No Products avaliable in the area.");
                    }
                }
                else
                {
                    return Error("User does not exists.");
                }
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }



        [HttpPost]
        public ActionResult getStoresListbyProductList([FromBody] int[] productIds)
        {
            try
            {
                var stores = (from st in context.Store
                              join
     pp in context.StoreCategory on
     st.StoreCategoryId equals pp.Id
                              select new
                              {
                                  Id = st.Id,
                                  StoreName = st.StoreName,
                                  StoreCategoryId = st.StoreCategoryId,
                                  Latitude = st.Latitude,
                                  Longitude = st.Longitude,
                                  UserId = st.UserId,
                                  Region = st.Region,
                                  Category = pp.Category
                              }).ToList();
                    
                    
                if (stores != null) {
                    return Ok(stores);
                }
                
                   return Error("No stores found.");
                

            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}
