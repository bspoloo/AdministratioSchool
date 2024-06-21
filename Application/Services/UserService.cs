using AdministratioSchool.Domain.DTO.In;
using AdministratioSchool.Domain.Entities;
using AdministratioSchool.Infraestructure.Persistence.Contex;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AdministratioSchool.Domain.DTO.Out;
using AdministratioSchool.Infraestructure.Security;

namespace AdministratioSchool.Application.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserOutDTO>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return _mapper.Map<IEnumerable<UserOutDTO>>(users); ;
        }
        public async Task<UserOutDTO> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not founded with Id:" + id);
            }
            var userFinded = _mapper.Map<UserOutDTO>(user);

            return userFinded;
        }
        public async Task<User> CreateUser([FromBody] UserInDTO userInDTO)
        {
            var user = _mapper.Map<User>(userInDTO);
            user.Password = Encrypt.EncryptPassword(user.Password);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<User> UpdateUser(int id, [FromBody] UserInDTO userInDTO)
        {
            var user = await _context.Users.FindAsync(id);
            userInDTO.Password = Encrypt.EncryptPassword(userInDTO.Password);

            if (user == null)
            {
                throw new KeyNotFoundException("User not founded with Id:" + id);
            }
            _mapper.Map(userInDTO, user);

            await _context.SaveChangesAsync();

            return user;
        }   
        public async Task<User> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not founded with Id:" + id);
            }
            var userDelted = _mapper.Map<User>(user);
            _context.Users.Remove(userDelted);
            await _context.SaveChangesAsync();
            return userDelted;
        }
    }
}
