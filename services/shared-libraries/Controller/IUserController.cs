using Microsoft.AspNetCore.Mvc;
using shared_libraries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared_libraries.Controller
{
    public interface IUserController
    {
        public Task<User?> GetUser(string publicId);
        Task<IActionResult> GetUser(int userId);
    }
}
