using System;
using System.Collections.Generic;

namespace AdminFinal.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; } = null!;
        public string AdminEmail { get; set; } = null!;
        public string AdminPhoneNum { get; set; } = null!;
        public string Adminpassword { get; set; } = null!;
    }
}
