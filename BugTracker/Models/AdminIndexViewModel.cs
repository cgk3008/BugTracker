﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class AdminIndexViewModel
    {

        public User User { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}