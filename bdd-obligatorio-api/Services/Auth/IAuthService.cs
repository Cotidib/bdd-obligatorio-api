using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bdd_obligatorio_api.Models;

namespace bdd_obligatorio_api.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUser(User user);
        Task<bool> GetUser(string id);
        Task<User?> LoginUser(string Username);
    }
}