using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Entity;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController  : ControllerBase
    {
        public static List<User> users = new List<User>
        {
            new User { UserId = Guid.NewGuid() , FirstName = "razan" , LastName = "Mansour" , Email = "razan@gmail.com" , BirthDate = new DateOnly(2001,2,1) , PhoneNumber = "0510000000" , Password = "asd123" , Role = "Admin"},
            new User { UserId = Guid.NewGuid() , FirstName = "ahmed" , LastName = "Ali" , Email = "ahmad@gmail.com" , BirthDate = new DateOnly(1998,4,10) , PhoneNumber = "0520000000" , Password = "qwe123" , Role = "customer"},
            new User { UserId = Guid.NewGuid() , FirstName = "leen" , LastName = "Mohanned" , Email = "leen@gmail.com" , BirthDate = new DateOnly(1988,2,10) , PhoneNumber = "0530000000" , Password = "mnb123" , Role = "customer"}
        };
        //get Method 
        //show all users
        [HttpGet]
        public ActionResult GetUsers()
        {
            int count = users.Count;
            //if the users list is empty show 404 Not found BC list is empty
            var isther = users.Any(x => count > 0);
            if (isther)
            {
                return Ok(users);
            }
            return NotFound();
        }
        //orderby and pagenation
        [HttpGet("{search}/{pagesize}/{pagenumber}")]
        public ActionResult GetOrderedUsers(int pagenumber , int pagesize , string search)
        {

            List<User> pagedUsers = users.Skip((pagenumber-1) * pagesize).Take(pagesize).ToList();
            if(search.ToLower() == "firstname")
            {
                return Ok(pagedUsers.OrderBy(x => x.FirstName).ToList());
            }
            else if(search.ToLower() == "lastname")
            {
                return Ok(pagedUsers.OrderBy(x => x.LastName).ToList());
            }
            else if(search.ToLower() == "email")
            {
                return Ok(pagedUsers.OrderBy(x => x.Email).ToList());
            }
            else if(search.ToLower() == "phonenumber")
            {
                return Ok(pagedUsers.OrderBy(x => x.PhoneNumber).ToList());
            }
            else 
            {
                return Ok(pagedUsers.OrderBy(x => x.UserId).ToList());
            }
        }
        [HttpGet("{id}")]
        public ActionResult GetUserById(Guid id)
        {
            User? isFound = users.FirstOrDefault(x => x.UserId == id);
            if (isFound == null)
            {
                return NotFound();
            }
            return Ok(isFound);
        }
        //POST MMETHOD -Create new user-
        //id - Auto generated
        //Email and PhoneNumber UNIQUE -no duplicates-
        // [HttpPost]
        // public ActionResult CreateUser()
        // {
            
        // }
        //Put Method -update information-
        //Email and PhoneNumber UNIQUE -no duplicates-
        // [HttpPut]
        // public ActionResult CreateUser()
        // {
        //   
        // }

        //Delete user by id
        [HttpDelete("id")]
        public ActionResult DeleteUser(Guid id)
        {
            User? isFound =users.FirstOrDefault(x => x.UserId == id);
            if(isFound == null)
            {
                return NotFound();
            }
            users.Remove(isFound);
            return NoContent();
        }
    }
}