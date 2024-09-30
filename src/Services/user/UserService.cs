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
            user.Password = hashedPassword;
            user.Salt = salt;
            user.Role = Rule.Customer;


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
            return "Unauthorized";
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
                return false;
            }
            _mapper.Map(updateDto, foundUser);
            return await _userRepo.UpdateOneAsync(foundUser);
        }
        public async Task<List<UserReadDto>> GetAllAsync()
        {
            var UserList = await _userRepo.GetAllAsync();
            return _mapper.Map<List<User>, List<UserReadDto>>(UserList);
        }
    }
}