using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Entity;
using Microsoft.AspNetCore.Mvc;
namespace src.Controllers
{
    public class UserController  : ControllerBase
    {
        public List<User> users = new List<User>(){
            new User {UserId = new Guid(), FirstName = "razan" , LastName = "kfj" , Email = "djhfkf" , BirthDate = new DateTime(2001,2,1) , PhoneNumber = 0000 , Password = "djhf" , Role = "Admin"}
        };
    }
}