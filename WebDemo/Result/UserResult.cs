﻿using Microsoft.VisualBasic;
using WebDemo.Models;

namespace WebDemo.Result
{
    public class UserResult
    {
        public int Id { get; set; } = 0;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; } = 0;
        public string? Name { get; set; }
        public int? SexId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<InformationResult>? Informations{ get; set; }
        public List<ProductResult>? Products { get; set; }

    }
}
