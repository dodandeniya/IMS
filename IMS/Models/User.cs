using IMS.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Models
{
    public class User
    {
        private bool isEnabled = true;
        private string password;
        private string role = Roles.Viewer;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }

        [Required]
        public string Email { get; set; }
        public ICollection<Inventory> Inventorys { get; set; }

        [Required]
        public string Password
        {
            get { return password; }
            set { SetPassword(value); }
        }


        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }

        public string Role
        {
            get { return role; }
            set { role = value; }
        }


        private void SetPassword(string pass)
        {
            password = BCrypt.Net.BCrypt.HashPassword(pass);
        }

        public bool CheckPassword(string tempPassword)
        {
            return BCrypt.Net.BCrypt.Verify(tempPassword, password);
        }

    }
}
