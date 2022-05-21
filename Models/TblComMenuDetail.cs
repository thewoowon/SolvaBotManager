using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SolvaBot.Models
{
    public partial class TblComMenuDetail
    {
        public string Code { get; set; } = string.Empty;
        public string MenuCode { get; set; } = string.Empty;
        public string CompanyCode { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public DateTime Wdate { get; set; }
        public string? SubTitle { get; set; } = string.Empty;
    }
}
