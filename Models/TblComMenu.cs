using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SolvaBot.Models
{
    public partial class TblComMenu
    {
        public string Code { get; set; } = string.Empty;
        public string CategoryCode { get; set; } = string.Empty;
        public string CompanyCode { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime? Wdate { get; set; }
    }
}
