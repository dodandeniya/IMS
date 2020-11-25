using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Constants
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Viewer = "Viewer";
        public const string AdminOrManager = Manager + "," + Admin;
        public const string AllUsers = Viewer + "," + AdminOrManager;
    }
}
