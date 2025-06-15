using System;
using System.Collections.Generic;
using System.Linq;
using ReTechApi.Models;

namespace ReTechApi.Data
{
    public static class RecyclingRequestRepository
    {
        private static readonly List<RecyclingRequest> _requests = new List<RecyclingRequest>
        {
            new RecyclingRequest
            {
                Id = "1",
                CustomerId = "1", // Demo User
                DeviceType = "Smartphone",
                DeviceModel = "iPhone 12",
                DeviceCondition = "Good",
                Description = "Slightly used iPhone 12 with minor scratches",
                ImageUrl = "images/recycling/iphone12.jpg",
                EstimatedValue = 500,
                Status = RequestStatus.Pending,
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new RecyclingRequest
            {
                Id = "2",
                CustomerId = "1", // Demo User
                DeviceType = "Laptop",
                DeviceModel = "MacBook Pro 2019",
                DeviceCondition = "Fair",
                Description = "MacBook Pro with battery issues",
                ImageUrl = "images/recycling/macbook.jpg",
                EstimatedValue = 800,
                Status = RequestStatus.Assigned,
                AssignedCompanyId = "2", // Demo Recycling Company
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                PickupDate = DateTime.UtcNow.AddDays(2)
            }
        };

        public static IEnumerable<RecyclingRequest> GetAllRequests()
        {
            return _requests;
        }

        public static RecyclingRequest GetRequestById(string id)
        {
            return _requests.FirstOrDefault(r => r.Id == id);
        }

        public static IEnumerable<RecyclingRequest> GetRequestsByCustomerId(string customerId)
        {
            return _requests.Where(r => r.CustomerId == customerId);
        }

        public static IEnumerable<RecyclingRequest> GetRequestsByCompanyId(string companyId)
        {
            return _requests.Where(r => r.AssignedCompanyId == companyId);
        }

        public static RecyclingRequest AddRequest(RecyclingRequest request)
        {
            request.Id = Guid.NewGuid().ToString();
            request.CreatedAt = DateTime.UtcNow;
            request.Status = RequestStatus.Pending;
            _requests.Add(request);
            return request;
        }

        public static RecyclingRequest UpdateRequest(RecyclingRequest request)
        {
            var existingRequest = _requests.FirstOrDefault(r => r.Id == request.Id);
            if (existingRequest != null)
            {
                existingRequest.Status = request.Status;
                existingRequest.AssignedCompanyId = request.AssignedCompanyId;
                existingRequest.PickupDate = request.PickupDate;
                existingRequest.PickupAddress = request.PickupAddress;
                existingRequest.Notes = request.Notes;
                existingRequest.UpdatedAt = DateTime.UtcNow;
            }
            return existingRequest;
        }
    }
} 