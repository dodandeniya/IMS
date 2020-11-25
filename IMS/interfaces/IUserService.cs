using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(string email, string password);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<bool> UpdateUser(int id, User user);
        Task<bool> CreateUser(User user);
        Task<User> DeleteUser(int id);
    }
}
