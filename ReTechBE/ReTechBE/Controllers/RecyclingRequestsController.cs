using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ReTechApi.Models;
using ReTechApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace ReTechApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecyclingRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public RecyclingRequestsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecyclingRequest>>> GetAllRequests()
        {
            return await _dbContext.RecyclingRequests.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<RecyclingRequest>> GetRequestById(string id)
        {
            var request = await _dbContext.RecyclingRequests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<RecyclingRequest>>> GetRequestsByCustomerId(string customerId)
        {
            var requests = await _dbContext.RecyclingRequests
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();

            return requests;
        }

        [HttpGet("company/{companyId}")]
        public async Task<ActionResult<IEnumerable<RecyclingRequest>>> GetRequestsByCompanyId(string companyId)
        {
            var requests = await _dbContext.RecyclingRequests
                .Where(r => r.AssignedCompanyId == companyId)
                .ToListAsync();

            return requests;
        }

        [HttpPost]
        public async Task<ActionResult<RecyclingRequest>> CreateRequest(RecyclingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            request.Id = Guid.NewGuid().ToString();
            request.CreatedAt = DateTime.UtcNow;
            request.Status = RequestStatus.Pending;

            _dbContext.RecyclingRequests.Add(request);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRequestById), new { id = request.Id }, request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(string id, RecyclingRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var existingRequest = await _dbContext.RecyclingRequests.FindAsync(id);
            if (existingRequest == null)
            {
                return NotFound();
            }

            existingRequest.Status = request.Status;
            existingRequest.AssignedCompanyId = request.AssignedCompanyId;
            existingRequest.PickupDate = request.PickupDate;
            existingRequest.PickupAddress = request.PickupAddress;
            existingRequest.Notes = request.Notes;
            existingRequest.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(string id)
        {
            var request = await _dbContext.RecyclingRequests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _dbContext.RecyclingRequests.Remove(request);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(string id)
        {
            return _dbContext.RecyclingRequests.Any(e => e.Id == id);
        }
    }
}