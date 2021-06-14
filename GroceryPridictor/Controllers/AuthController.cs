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
    public class AuthController : CustomControllerBase
    {
        private readonly GroceryContext context;
        // GET: api/<AuthController>

        public AuthController(GroceryContext _context)
        {
            context = _context;
        }
      
        [HttpPost]
        public IActionResult SignIn(string UserName, string Password) {
            try
            {
                CustomResponseModel model = new CustomResponseModel();
               
                var person = context.User.Where(s => s.UserName == UserName && s.Password == Password).FirstOrDefault();
                if (person != null)
                {//
                //    model.Message = "Loged in Successfully.";
                //    model.Data = person.Id;
                //    model.Status = true;
                    return Ok(person.Id);
                }
                else {
                    return Error("Wrong User Name or Password.");
                }
               
            }
            catch(Exception ex ){
                return Error(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Register([FromQuery]User user)
        {
            try
            {
                var person = context.User.Where(s => s.UserName == user.UserName && s.Password == user.Password).FirstOrDefault();
                if (person == null)
                {
                    context.User.Add(user);
                    context.SaveChanges();
                    return Ok("Registed Successfully.");
                }
                else {
                    return Error("User Already Exists.");
                }
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }



    }
}
