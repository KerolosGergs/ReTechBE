using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReTechApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public UserType UserType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }
        public DateTime? LastLogin { get; set; }
        public int Points { get; set; } = 100;
    }
    public enum UserType
    {
        Customer,
        RecyclingCompany,
        Admin
    }
} 