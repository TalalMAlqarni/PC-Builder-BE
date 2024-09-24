using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Entity;
using Microsoft.AspNetCore.Mvc;
using src.Utils;

namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController  : ControllerBase
    {
        public static List<User> users = new List<User>
        {
            new User { UserId = Guid.NewGuid() , Username = "razan12" , FirstName = "razan" , LastName = "Mansour" , Email = "razan@gmail.com" , BirthDate = new DateOnly(2001,2,1) , PhoneNumber = "0510000000" , Password = "asd123" , Role = "Admin"},
            new User { UserId = Guid.NewGuid() , Username = "ahhmmd" , FirstName = "ahmed" , LastName = "Ali" , Email = "ahmad@gmail.com" , BirthDate = new DateOnly(1998,4,10) , PhoneNumber = "0520000000" , Password = "qwe123" , Role = "customer"},
            new User { UserId = Guid.NewGuid() , Username = "leen56" , FirstName = "leen" , LastName = "Mohanned" , Email = "leen@gmail.com" , BirthDate = new DateOnly(1988,2,10) , PhoneNumber = "0530000000" , Password = "mnb123" , Role = "customer"}
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
        [HttpPost]
        public ActionResult SignUpUser(User newUser)
        {
            PasswordUtils.HashPassword(newUser.Password, out string hashedPassword , out byte[] salt);
            newUser.Password = hashedPassword;
            newUser.Salt = salt;
            var checkUsername = users.Any(x => x.Username == newUser.Username);
            var checkEmail = users.Any(x => x.Email == newUser.Email);
            var checkPhoneNumber = users.Any(x => x.PhoneNumber == newUser.PhoneNumber);
            if(checkEmail)
            {
                return NotFound();
            } 
            else if(checkPhoneNumber)
            {
                return NotFound();
            }
            else if(checkUsername)
            {
                return NotFound();
            }
            else
            {
                Guid newuserid = Guid.NewGuid();
                newUser.UserId = newuserid;
                users.Add(newUser);
                return CreatedAtAction(nameof(GetUserById) , new {id = newUser.UserId } , newUser);
            }
        }
        [HttpPost("login")]
        public ActionResult LogInUser(User user)
        {
            User? foundUser = users.FirstOrDefault(x => x.Username == user.Username);
            if(foundUser == null)
            {
                return NotFound();;
            }
            bool isMatched = PasswordUtils.VerifyPassword(user.Password, foundUser.Password , foundUser.Salt);
            if(!isMatched)
            {
                return Unauthorized();
            }
            return Ok(foundUser);
        }
        //Put Method -update information-
        //Email and PhoneNumber UNIQUE -no duplicates-
        [HttpPut("{id}")]
        public ActionResult UpdateFirstName(Guid id, User newUser)
        {
            var user = users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }
           
                user.FirstName = newUser.FirstName;
                user.LastName = newUser.LastName;
                user.Email = newUser.Email;
                user.PhoneNumber = newUser.PhoneNumber;
                user.BirthDate = newUser.BirthDate;
            
            return NoContent();
        }

        //Delete user by id
        [HttpDelete("{id}")]
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