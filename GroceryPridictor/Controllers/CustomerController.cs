using GroceryPridictor.Infrastructure;
using GroceryPridictor.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        public ActionResult getStoresListbyProductList([FromQuery]int UserId, [FromQuery] int[] productIds)
        {
            try
            {
                //find user
                var user = context.User.Where(x => x.Id == UserId).FirstOrDefault();
                int userRegion= getRegion.getRegionFun(user.Latitude,user.Longitude);
                //get his/her region


                //get those products from database contains one of productIds
                var product1 = context.Product.Where(x=> productIds.Contains(x.Id));
                var product = product1.GroupBy(p => p.StoreId)
                                .Select(g => new { StoreId= g.Key,
                                                   Count = g.Count() }).OrderByDescending(x=>x.Count).ToList();

                //
                //var stores = (from st in context.Store
                //              join
                //                   pp in context.StoreCategory on
                //                       st.StoreCategoryId equals pp.Id
                //              select new
                //              {
                //                  Id = st.Id,
                //                  StoreName = st.StoreName,
                //                  StoreCategoryId = st.StoreCategoryId,
                //                  Latitude = st.Latitude,
                //                  Longitude = st.Longitude,
                //                  UserId = st.UserId,
                //                  Region = st.Region,
                //                  Category = pp.Category
                //              }).Where(c=> product.Select(b=>b.StoreId).Contains(c.Id)).ToList();


                // to select stores in descending order 
                //var orderedstore = (from st in context.Store 
                //                    join pr in product on st.Id equals pr.StoreId
                //                    into OdStore
                //                    select new
                //                    {
                //                        Id = st.Id,
                //                        StoreName = st.StoreName,
                //                        StoreCategoryId = st.StoreCategoryId,
                //                        Latitude = st.Latitude,
                //                        Longitude = st.Longitude,
                //                        UserId = st.UserId,
                //                        Region = st.Region,
                //                        Count = st.Count
                //                    }).Where(x => x.Region.Equals(userRegion)).OrderByDescending(x => x.Count).ToList();
                var parameters = new string[productIds.Length];
                var sqlParameters = new List<SqlParameter>();
                for (var i = 0; i < productIds.Length; i++)
                {
                    parameters[i] = string.Format("@p{0}", i);
                    sqlParameters.Add(new SqlParameter(parameters[i], productIds[i]));
                }

                //var order = context.Database.ExecuteSqlRaw(@"
                //                             select st.Id,st.Latitude,st.Longitude,st.Region,st.StoreCategoryId,st.StoreName,st.UserId from
                //                             (
                //                             select StoreId , count(StoreId) as 'CountValue'  from  Product where ProductId IN ({0})
                //                             group by StoreId
                //                             ) as abc  join Store as st on abc.StoreId = st.Id  order by CountValue desc", productIds);

             
                  // get the product whose ids are present in the given paramerter array then from that find the stores in the database 
                  // and then find the stores by the counnt of their product occurance in decending order
                var rawCommand = string.Format(@"
                                            select st.Id,st.Latitude,st.Longitude,st.Region,st.StoreCategoryId,st.StoreName,st.UserId from
                                            (
                                            select StoreId , count(StoreId) as 'CountValue'  from  Product where ProductId IN ({0})
                                            group by StoreId
                                            ) as abc  join Store as st on abc.StoreId = st.Id  order by CountValue desc", string.Join(", ", parameters));

                // execute the query
               var f = context.Store.FromSqlRaw(rawCommand, sqlParameters.ToArray()).ToList();  /*(rawCommand, sqlParameters.ToArray())*/;
              




                //var orderedStore = (from st in context.Store
                //                    join pr in product
                //                    on st.Id equals pr.StoreId
                //                    select new
                //                    {
                //                        Id = st.Id,
                //                        StoreName = st.StoreName,
                //                        StoreCategoryId = st.StoreCategoryId,
                //                        Latitude = st.Latitude,
                //                        Longitude = st.Longitude,
                //                        UserId = st.UserId,
                //                        Region = st.Region,
                //                        Count = pr.Count

                //                    }).ToList();



                // add Catagory column and filter the region record 
                var  tt = (from st in f
                          join
                         sc in context.StoreCategory on
                        st.StoreCategoryId equals sc.Id
                         select new
                  {
                     st.Id,
                     st.StoreName,
                     st.StoreCategoryId,
                     st.Latitude,
                    st.Longitude,
                      st.UserId,
                     st.Region,
                     sc.Category
                  }).Where(x=>x.Region == userRegion).ToList();


                if (tt != null) {
                    return Ok(tt);
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
