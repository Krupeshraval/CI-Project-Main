using System;
using System.Collections.Generic;

namespace CI_Entities.Models
{
    public partial class ContactU
    {
        public string? Email { get; set; }
        public string? Username { get; set; }
        public int ContactUsId { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
