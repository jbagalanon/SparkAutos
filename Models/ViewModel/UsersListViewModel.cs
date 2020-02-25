using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

//viewmodel is something exist in the system but not in database

namespace SparkAuto.Models.ViewModel
{
    public class UsersListViewModel
    {
        //this list public List<ApplicationUser> came from index property of user. Once you copied the file, you need to change the orig value
        public List<ApplicationUser> ApplicationUserList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
