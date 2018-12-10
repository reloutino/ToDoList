using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ToDoList.WebApp.MVC.Models
{
    public class UserDataAccessLayer
    {
        private List<dynamic> _presetUsers;

        public UserDataAccessLayer()
        {
            _presetUsers = new List<dynamic>
            {
                new {Name = "test", Password = "pwd123", UserIdClaim = "00000000-0000-0000-0000-000000000001"},
                new {Name = "test2", Password = "pwd456", UserIdClaim = "00000000-0000-0000-0000-000000000002"},
                new {Name = "test3", Password = "pwd789", UserIdClaim = "00000000-0000-0000-0000-000000000003"},
            };
        }

        //To Validate the login  
        public string ValidateLogin(UserDetails user)
        {
            //the call to the identiyserver will be here.
            //the claim UserId will be returned with the token


            if (_presetUsers.Any(x => x.Name == user.UserID && x.Password == user.Password))
            {
                return "Success";
            }
            else
            {
                return String.Empty;
            }
        }

        public string GetClaims(UserDetails user)
        {
            //the call to the identiyserver will be here.
            //the claim UserId will be returned with the token
            return _presetUsers.FirstOrDefault(x => x.Name == user.UserID && x.Password == user.Password)?
                .UserIdClaim;
        }
    }
}
