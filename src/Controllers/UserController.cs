using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Entity;
namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        public List<User> users = new List<User>
        {
            new User { UserId = new Guid(), FirstName = "Razan" , LastName = "Altowairqi" , Email = "razan@gmail.com" , PhoneNumber = 0500000000 , Password = "asd123" , Role = "admin" , BirthDate = new DateTime(2005,21,12)},
            new User { UserId = new Guid(), FirstName = "Reem" , LastName = "Alatawi" , Email = "reem@gmail.com" , PhoneNumber = 0510000000 , Password = "zxc123" , Role = "customer" , BirthDate = new DateTime(1996,1,5)},
            new User { UserId = new Guid(), FirstName = "Ahmad" , LastName = "xx" , Email = "ahmad@gmail.com" , PhoneNumber = 0520000000 , Password = "mnb987" , Role = "customer" , BirthDate = new DateTime(2005,8,12)}
        };
       [HttpGet]
        public ActionResult GetAllProducts()
        {
            return Ok(users);
        }
        [HttpGet("{id}")]
        public ActionResult GetProductById(Guid id)
        {
            User? isFound = users.FirstOrDefault(x => x.UserId == id);

            if (isFound == null)
            {
                return NotFound();
            }

            return Ok(isFound);
        }
         [HttpDelete("{id}")]
        public ActionResult DeleteProductById(Guid id)
        {
            User? isFound = users.FirstOrDefault(x => x.UserId == id);

            if (isFound == null)
            {
                return NotFound();
            }
            users.Remove(isFound);
            return NoContent();
        }

    }
}