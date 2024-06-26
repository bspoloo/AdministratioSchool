using AdministratioSchool.Domain.DTO.In;
using AdministratioSchool.Domain.Entities;
using AdministratioSchool.Infraestructure.Persistence.Contex;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AdministratioSchool.Domain.DTO.Out;
using AdministratioSchool.Infraestructure.Security;
using AdministratioSchool.Domain.Repositories;

namespace AdministratioSchool.Domain.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRepositorie _userRepositorie;
        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _userRepositorie = new UserRepositorie(_context);

        }
        public async Task<IEnumerable<UserOutDTO>> GetUsers()
        {
            var users = await _userRepositorie.GetUsers();
            return _mapper.Map<IEnumerable<UserOutDTO>>(users);
        }
        public async Task<UserOutDTO> GetUserById(int id)
        {
            var user = await _userRepositorie.GetUserById(id);
            var userFinded = _mapper.Map<UserOutDTO>(user);
            return userFinded;
        }
        public async Task<User> CreateUser([FromBody] UserInDTO userInDTO)
        {
            var user = _mapper.Map<User>(userInDTO);
            user.Password = Encrypt.EncryptPassword(user.Password);
            if(user == null)
            {
                throw new KeyNotFoundException("User don't created");
            }
            return await _userRepositorie.CreateUser(user);
        }
        public async Task<User> UpdateUser(int id, [FromBody] UserInDTO userInDTO)
        {
            
            userInDTO.Password = Encrypt.EncryptPassword(userInDTO.Password);
            var user = await _userRepositorie.UpdateUser(id, _mapper.Map<User>(userInDTO));
            return user;
        }
        public async Task<User> DeleteUser(int id)
        {
            var user = await _userRepositorie.DeleteUser(id);
            var userDelted = _mapper.Map<User>(user);
            return userDelted;
        }
    }
}
