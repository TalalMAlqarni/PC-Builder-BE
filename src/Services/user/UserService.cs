using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.Repository;
using src.Controllers;
using static src.DTO.UserDTO;
using src.Entity;
using src.Utils;
using static src.Entity.User;
using src.DTO;
using Microsoft.AspNetCore.Identity;


namespace src.Services.user
{
    public class UserService : IUserService
    {
        protected readonly UserRepository _userRepo;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _config;
        public UserService(UserRepository userRepo , IMapper mapper , IConfiguration config)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _config = config;
        }
        public async Task<UserReadDto> CreateOneAsync(UserCreateDto createDto)
        {
           PasswordUtils.HashPassword(createDto.Password , out string hashedPassword , out byte[] salt);
            var user = _mapper.Map<UserCreateDto, User>(createDto);
            var userTable = await _userRepo.GetAllAsync();
            if(userTable.Any(x => x.Email == user.Email) )
            {
                throw CustomException.BadRequest("Email already registed please try another one");
            }
            if(userTable.Any(x => x.PhoneNumber == user.PhoneNumber))
            {
                throw CustomException.BadRequest("Phone number already registed please try another one");
            }
            if(userTable.Any(x => x.Username == user.Username))
            {
                throw CustomException.BadRequest("Username already registed please try another one");
            }
            if(user.Email.Contains("@admin.com"))
            {
                user.Role = UserRole.Admin;
            }
            else
            {
                user.Role = UserRole.Customer;
            }
            if(user.Email == null)
            {
                throw CustomException.BadRequest("You cant leave Email empty");
            }
            if(user.PhoneNumber == null)
            {
                throw CustomException.BadRequest("You cant leave phone number empty");
            }
            if(user.Username == null)
            {
                throw CustomException.BadRequest("You cant leave Username empty");
            }
            if(user.FirstName == null)
            {
                throw CustomException.BadRequest("You cant leave First name empty");
            }
            if(user.LastName == null)
            {
                throw CustomException.BadRequest("You cant leave Last name empty");
              
            }
           
            user.CartId = Guid.NewGuid();
            user.Password = hashedPassword;
            user.Salt = salt;
            var savedUser = await _userRepo.CreateOneAsync(user);
            return _mapper.Map<User, UserReadDto>(savedUser);
        }

        //sign in
        public async Task<string> SignInAsync(UserCreateDto createDto)
        {
            // logic
            // find user by Email
            var foundUser = await _userRepo.FindByEmailAsync(createDto.Email);

            // check password
            var isMatched = PasswordUtils.VerifyPassword(createDto.Password, foundUser.Password, foundUser.Salt);

            if (isMatched)
            {
                // create token 
                var tokenUtil = new TokenUtils(_config);
                return tokenUtil.GenerateToken(foundUser);
            }

            // string
           throw CustomException.UnAuthorized($"user with {foundUser.Email} password doesnt match");
        }
        // get by id
        public async Task<UserReadDto> GetByIdAsync(Guid id)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);
            return _mapper.Map<User, UserReadDto>(foundUser);
        }
        // delete 
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);
            bool isDeleted = await _userRepo.DeleteOneAsync(foundUser);
            if(isDeleted)
            {
                return true;
            }
            return false;
        }
        // update
        public async Task<bool> UpdateOneAsync(Guid id , UserUpdateDto updateDto)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);
            if(foundUser == null)
            {
                throw CustomException.UnAuthorized($"user with {foundUser.UserId}  doesnt exist");
            }
            else
            {
                if(updateDto.Email == null)
                {
                    updateDto.Email = foundUser.Email;
                }
                if(updateDto.Username == null)
                {
                    updateDto.Username = foundUser.Username;
                }
                if(updateDto.FirstName == null)
                {
                    updateDto.FirstName = foundUser.FirstName;
                }
                if(updateDto.LastName == null)
                {
                    updateDto.LastName = foundUser.LastName;
                }
                if(updateDto.PhoneNumber == null)
                {
                    updateDto.PhoneNumber = foundUser.PhoneNumber;
                }
                if(updateDto.Password == null)
                {
                    updateDto.Password = foundUser.Password;
                }
            
                if(updateDto.CartId == null)
                {
                    updateDto.CartId = foundUser.CartId;
                }
                if(foundUser.Email.Contains("@admin.com"))
                {
                    updateDto.Role = UserRole.Admin;
                }
                else 
                {
                    updateDto.Role = UserRole.Customer;
                }
                if (updateDto.BirthDate.Equals(DateOnly.Parse("0001-01-01")))
                {
                    updateDto.BirthDate = foundUser.BirthDate;
                }
                _mapper.Map(updateDto, foundUser);
                return await _userRepo.UpdateOneAsync(foundUser);
            }
           
        }
        public async Task<List<UserReadDto>> GetAllAsync()
        {
            var UserList = await _userRepo.GetAllAsync();
            return _mapper.Map<List<User>, List<UserReadDto>>(UserList);
        }
    }
}