//using System;
//using System.Collections.Generic;
//using System.Linq;
//using ReTechApi.Models;

//namespace ReTechApi.Data
//{
//    public static class UserRepository
//    {
//        //private static readonly List<ApplicationUser> _users = new List<ApplicationUser>
//        //{
//        //    new ApplicationUser
//        //    {
//        //        Id = "1",
//        //        Email = "user@example.com",
//        //        Password = "user123", // Note: In production, this should be hashed
//        //        Name = "Demo User",
//        //        Phone = "1234567890",
//        //        Address = "123 Main St",
//        //        Type = UserType.Customer,
//        //        CreatedAt = DateTime.UtcNow.AddDays(-30),
//        //        IsActive = true
//        //    },
//        //    new ApplicationUser
//        //    {
//        //        Id = "2",
//        //        Email = "company@recycling.com",
//        //        Password = "company123",
//        //        Name = "Demo Recycling Company",
//        //        Phone = "0987654321",
//        //        Address = "456 Business Ave",
//        //        Type = UserType.RecyclingCompany,
//        //        CreatedAt = DateTime.UtcNow.AddDays(-60),
//        //        IsActive = true
//        //    },
//        //    new ApplicationUser
//        //    {
//        //        Id = "3",
//        //        Email = "admin@retech.com",
//        //        Password = "admin123",
//        //        Name = "System Admin",
//        //        Phone = "5555555555",
//        //        Address = "789 Admin Blvd",
//        //        Type = UserType.Admin,
//        //        CreatedAt = DateTime.UtcNow.AddDays(-90),
//        //        IsActive = true
//        //    }
//        //};

//        public static IEnumerable<ApplicationUser> GetAllUsers()
//        {
//            return _users;
//        }

//        public static ApplicationUser GetUserById(string id)
//        {
//            return _users.FirstOrDefault(u => u.Id == id);
//        }

//        public static ApplicationUser GetUserByEmail(string email)
//        {
//            return _users.FirstOrDefault(u => u.Email == email);
//        }

//        public static ApplicationUser AddUser(ApplicationUser user)
//        {
//            user.Id = Guid.NewGuid().ToString();
//            user.CreatedAt = DateTime.UtcNow;
//            user.IsActive = true;
//            _users.Add(user);
//            return user;
//        }

//        public static ApplicationUser UpdateUser(ApplicationUser user)
//        {
//            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
//            if (existingUser != null)
//            {
//                existingUser.Name = user.Name;
//                existingUser.Phone = user.Phone;
//                existingUser.Address = user.Address;
//                existingUser.IsActive = user.IsActive;
//                existingUser.LastLogin = DateTime.UtcNow;
//            }
//            return existingUser;
//        }
//    }
//} 