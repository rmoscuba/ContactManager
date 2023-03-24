using ContactManager.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Interfaces
{
    public interface IJwtRepository
    {
        TokenInfo GenerateSecurityToken(User user);
    }
}
