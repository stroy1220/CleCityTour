using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Classes
{
    public class Password
    {
        public string UnSaltedPassword { get; set; }
        public string Salt { get; set; }
    }
}