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
        public List<User> users = new List<User>
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
        //show specific user by name
        // [HttpGet("{search}")]
        // public ActionResult FindUsers(string search)
        // {
        //     var searchById = users.Where(x => x.UserId.ToString().Contains(search ,StringComparison.OrdinalIgnoreCase));
        //     var searchByFirstName = users.Where(x => x.FirstName.Contains(search , StringComparison.OrdinalIgnoreCase));
        //     var searchByLastName = users.Where(x => x.LastName.Contains(search , StringComparison.OrdinalIgnoreCase));
        //     var searchByEmail = users.Where(x => x.Email.Contains(search , StringComparison.OrdinalIgnoreCase));
        //     var searchByPhoneNumber = users.Where(x => x.PhoneNumber.Contains(search , StringComparison.OrdinalIgnoreCase));
        //     if (searchByFirstName == null)
        //     {
        //         if(searchByLastName == null)
        //         {
                    
        //             if(searchByEmail == null)
        //             {
        //                 if(searchByPhoneNumber == null)
        //                 {
        //                     if(searchById == null)
        //                     {
        //                         return NotFound();
        //                     }
        //                     else
        //                     {
        //                         return Ok(searchById);
        //                     }
        //                 }
        //                 else
        //                 {
        //                     return Ok(searchByPhoneNumber);
        //                 }
        //             }
        //             else
        //             {
        //                 return Ok(searchByEmail);
        //             }
        //         }
        //         else 
        //         {
        //             return Ok(searchByLastName);
        //         }
        //     }
        //     else 
        //     {
        //         return Ok(searchByFirstName);
        //     }
        // }
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
    }
}