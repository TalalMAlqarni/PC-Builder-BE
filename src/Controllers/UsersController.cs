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
        public async Task<ActionResult<UserReadDto>> CreateOne(UserCreateDto createDto)
        {
            var userCreated = await _userService.CreateOneAsync(createDto);
            if(createDto.Email == null)
            {
                throw CustomException.BadRequest("You cant leave Email empty");
            }
            if(createDto.PhoneNumber == null)
            {
                throw CustomException.BadRequest("You cant leave phone number empty");
            }
            if(createDto.Username == null)
            {
                throw CustomException.BadRequest("You cant leave Username empty");
            }
            if(createDto.FirstName == null)
            {
                throw CustomException.BadRequest("You cant leave First name empty");
            }
            if(createDto.LastName == null)
            {
                throw CustomException.BadRequest("You cant leave Last name empty");
              
            }
            
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
        public async Task<ActionResult<UserReadDto>> GetOrderById([FromRoute] Guid userId)
        {
            var foundUser = await _userService.GetByIdAsync(userId);
            if (foundUser == null)
            {
                return NotFound("User not found");
            }
            return Ok(foundUser);
        }
      //  [Authorize]
        [HttpGet]
        
        public async Task<ActionResult<List<UserReadDto>>> GetAll()
        {
            var userList = await _userService.GetAllAsync();
            if(userList.Count == 0)
            {
                throw CustomException.NotFound("User table is empty");
            }
            else 
            {
                return Ok(userList);
            }
        }
         [HttpDelete("{userId}")]
        public async Task<ActionResult> CancelOrder(Guid userId)
        {
            var foundUser = await _userService.GetByIdAsync(userId);
            if (foundUser == null)
                return NotFound("User ID not found");

            var isDeleted = await _userService.DeleteOneAsync(userId);
            return isDeleted ? NoContent() : StatusCode(500);
        }
         
        [HttpPut("{userId}")]
        public async Task<ActionResult<UserReadDto>> UpdateCart(Guid userId, UserUpdateDto updateDto)
        {
            var userRead = await _userService.UpdateOneAsync(userId, updateDto);
            return Ok(userRead);
        }
        
    }

    
}