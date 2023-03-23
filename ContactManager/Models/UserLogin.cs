using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public class UserLogin
    {
        public string UserName { get; set; }

        public string PassWord { get; set; }
    }
}
