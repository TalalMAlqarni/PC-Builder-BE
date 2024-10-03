using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Entity;
using Microsoft.AspNetCore.Mvc;
using src.Utils;
using src.Services.user;
using static src.DTO.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using static src.Entity.User;

namespace src.Controllers
{   [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController  : ControllerBase
    {
        protected readonly IUserService _userService;

        public UsersController(IUserService service)
        {
            _userService = service;
        }
        // create user
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserReadDto>> CreateOne(UserCreateDto createDto)
        {
            var userCreated = await _userService.CreateOneAsync(createDto);
            return Ok(userCreated);
        }
        
        // log in
        [HttpPost("signIn")]
        
        public async Task<ActionResult<string>> SignInUser([FromBody] UserCreateDto createDto)
        {
            var token = await _userService.SignInAsync(createDto);
            //return Created($"api/v1/users/{UserCreated.Id}", UserCreated);
            // if(token )
            // {
            //     throw CustomException.UnAuthorized("Unauthorized access");
            // }
            return Ok(token);
        }
        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserReadDto>> GetOrderById([FromRoute] Guid userId)
        {
            var foundUser = await _userService.GetByIdAsync(userId);
            if (foundUser == null)
            {
                throw CustomException.BadRequest("User not found");
            }
            return Ok(foundUser);
        }
      //  [Authorize]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserReadDto>>> GetAllUsers()
        {
            var userList = await _userService.GetAllAsync();
           
                return Ok(userList);
            
        }
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser(Guid userId)
        {
            var foundUser = await _userService.GetByIdAsync(userId);
            if (foundUser == null)
                throw CustomException.UnAuthorized($"user with {userId}  doesnt exist");

            var isDeleted = await _userService.DeleteOneAsync(userId);
            return isDeleted ? Ok("user deleted seccsufully") : StatusCode(500);
        }
         
        [HttpPut("{userId}")]
        public async Task<ActionResult<UserReadDto>> UpdateUser(Guid userId, UserUpdateDto updateDto)
        {
            var userRead = await _userService.UpdateOneAsync(userId, updateDto);
            return Ok("User Updated seccussfuly");
        }
        
    }

    
}