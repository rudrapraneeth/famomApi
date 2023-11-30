﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
