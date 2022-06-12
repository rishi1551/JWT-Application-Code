using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTApplication.Models;
namespace JWTApplication.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authonticate(Users user);

    }
}
