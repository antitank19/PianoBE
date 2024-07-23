﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbObject
{
    public class UserToken : IdentityUserToken<int>
    {
        public virtual User User { get; set; }
    }
}
