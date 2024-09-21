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
        //show specific user by name
        [HttpGet("{search}")]
        public ActionResult GetUserById(string search)
        {
           
            var searchByFirstName = users.FirstOrDefault(x => x.FirstName == search);
            var searchByLastName = users.FirstOrDefault(x => x.LastName == search);
            var searchByEmail = users.FirstOrDefault(x => x.Email == search);
            var searchByPhoneNumber = users.FirstOrDefault(x => x.PhoneNumber == search);
            if (searchByFirstName == null)
            {
                if(searchByLastName == null)
                {
                    
                    if(searchByEmail == null)
                    {
                        if(searchByPhoneNumber == null)
                        {
                               return NotFound();
                        }
                        else
                        {
                            return Ok(searchByPhoneNumber);
                        }
                    }
                    else
                    {
                        return Ok(searchByEmail);
                    }
                }
                else 
                {
                    return Ok(searchByLastName);
                }
            }
            else 
            {
                return Ok(searchByFirstName);
            }
        }
        [HttpGet("{id:guid}")]
        public ActionResult GetUserById2(Guid id)
        {
            User? isFound = users.FirstOrDefault(x => x.UserId == id);

            if (isFound == null)
            {
                return NotFound("not found");
            }

            return Ok(isFound);
        }
        //POST MMETHOD -Create new user-
        //id - Auto generated
        //Email and PhoneNumber UNIQUE -no duplicates-

    }
}