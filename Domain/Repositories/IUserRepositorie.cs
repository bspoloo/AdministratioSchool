﻿using AdministratioSchool.Domain.Entities;

namespace AdministratioSchool.Domain.Repositories
{
    public interface IUserRepositorie
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(int id, User user);
        Task<User> DeleteUser(int id);
    }
}
