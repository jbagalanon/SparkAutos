using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SparkAuto.Models
{

    //application user is extended identity user is a user account build by microsoft
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        //push this file to database. Go to application dbcontext
        //add all this file in register.cshtml.cs 

    }
}
