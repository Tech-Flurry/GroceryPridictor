using GroceryPridictor.Infrastructure;
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
                    regio = getRegion.getRegionFun(person.Latitude, person.Longitude);
                    var prod = await (from st in context.Store
                                join pr in context.Product
                               on st.Id equals pr.StoreId
                                select new Store
                                {
                                    StoreName = st.StoreName,
                                    StoreCategoryId = st.StoreCategoryId,    
                                    Latitude = st.Latitude,
                                    Longitude = st.Longitude,
                                    UserId = st.UserId,
                                    Region = st.Region
                                }).Where(x => x.Region == regio).ToListAsync(); ;
                    if (prod != null)
                    {
                        if (!String.IsNullOrEmpty(searchResult))
                        {
                            string searchMatch = searchResult.Trim().ToLower();
                            model.Data = prod.Where(e => e.StoreName.ToLower().Contains(searchMatch)).ToList();
                        }
                        
                        model.Message = "hello workd";
                        model.Status = true;

                        return Ok(model);
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
        [HttpGet]
        public ActionResult getNearByProducts(int UserId) {
            try
            {
                CustomResponseModel<List<getNearByProductCategoryModel>> model = new();
                int regio=4;
                var person = context.User.Where(u => u.Id == UserId).FirstOrDefault();
                if (person != null && person.Latitude != null && person.Longitude != null) {
                    regio =  getRegion.getRegionFun(person.Latitude,person.Longitude);
                
                var prod = (from st in context.Store
                             join pr in context.Product
                            on st.Id equals pr.StoreId
                             select new getNearByProductCategoryModel 
                             {
                                 Name  = pr.Name,
                                 Price = pr.Price,
                                 Stock = pr.Stock,
                                 Link  = pr.Link,
                                 Catagory = pr.Catagory,
                                 Region = st.Region
                             }).Where(x => x.Region == regio).ToList();
                    if (prod != null)
                    {
                        model.Data = prod;
                        model.Message = "hello workd";
                        model.Status = true;

                        return Ok(model);
                    }
                    else {
                        return Error("No Products avaliable in the area.");
                    }
                }                
                else{
                    return Error("User does not exists.");
                }
            }
            catch(Exception ex) {
                return Error(ex.Message);
            }
        }        
    }
}
