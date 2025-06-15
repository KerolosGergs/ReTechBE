using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ReTechApi.Models;
using ReTechApi.Data;

namespace ReTechApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RewardsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Reward>> GetRewards()
        {
            return Ok(RewardRepository.GetAllRewards());
        }

        [HttpGet("{id}")]
        public ActionResult<Reward> GetReward(string id)
        {
            var reward = RewardRepository.GetRewardById(id);
            if (reward == null)
            {
                return NotFound();
            }
            return Ok(reward);
        }

        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<Reward>> GetUserRewards(string userId)
        {
            return Ok(RewardRepository.GetRewardsByUserId(userId));
        }

        [HttpGet("user/{userId}/active")]
        public ActionResult<IEnumerable<Reward>> GetActiveUserRewards(string userId)
        {
            return Ok(RewardRepository.GetActiveRewards(userId));
        }

        [HttpPost]
        public ActionResult<Reward> CreateReward(Reward reward)
        {
            var newReward = RewardRepository.AddReward(reward);
            return CreatedAtAction(nameof(GetReward), new { id = newReward.Id }, newReward);
        }

        [HttpPut("{id}")]
        public ActionResult<Reward> UpdateReward(string id, Reward reward)
        {
            if (id != reward.Id)
            {
                return BadRequest();
            }

            var updatedReward = RewardRepository.UpdateReward(reward);
            if (updatedReward == null)
            {
                return NotFound();
            }

            return Ok(updatedReward);
        }

        [HttpPut("{id}/use")]
        public ActionResult<Reward> UseReward(string id)
        {
            var reward = RewardRepository.GetRewardById(id);
            if (reward == null)
            {
                return NotFound();
            }

            if (reward.IsUsed)
            {
                return BadRequest("Reward has already been used");
            }

            if (reward.ExpiryDate < System.DateTime.UtcNow)
            {
                return BadRequest("Reward has expired");
            }

            reward.IsUsed = true;
            reward.UsedDate = System.DateTime.UtcNow;
            
            var updatedReward = RewardRepository.UpdateReward(reward);
            return Ok(updatedReward);
        }
    }
} 