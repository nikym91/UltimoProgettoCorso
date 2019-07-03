using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SportsClubModel;
using SportsClubModel.Interfaces;
using SportsClubWeb.DTO;
using System;
using System.Threading.Tasks;

namespace SportsClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;

        public ChallengeController(IChallengeUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ChallengeDTO[]>> Get()
        {
            try
            {
                var results = await UnitOfWork.GetAllChallengesAsync();

                return Mapper.Map<ChallengeDTO[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        public async Task<ActionResult<ChallengeDTO>> Post(ChallengeDTO dto)
        {
            try
            {
                var existing = await UnitOfWork.GetChallengeByIdAsync(dto.ChallengeId);
                if (existing != null)
                {
                    return BadRequest("Id in Use");
                }

                //var location = linkGenerator.GetPathByAction("Get",
                //  "Camps",
                //  new { moniker = model.Moniker });

                //if (string.IsNullOrWhiteSpace(location))
                //{
                //    return BadRequest("Could not use current moniker");
                //}

                // Create a new challenge
                var challenge = Mapper.Map<Challenge>(dto);
                await UnitOfWork.AddChallengeAsync(challenge);
                return Created($"/api/challenges/{challenge.ChallengeId}", Mapper.Map<ChallengeDTO>(challenge));

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{challengeId}")]
        public async Task<ActionResult<ChallengeDTO>> Put(int challengeId, ChallengeDTO dto)
        {
            try
            {
                var oldChallenge = await UnitOfWork.GetChallengeByIdAsync(challengeId);
                if (oldChallenge == null)
                {
                    return NotFound($"Could not find challenge with moniker of {challengeId}");
                }

                Mapper.Map(dto, oldChallenge);

                if (await UnitOfWork.SaveChangesAsync())
                {
                    return Mapper.Map<ChallengeDTO>(oldChallenge);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{challengeId}")]
        public async Task<IActionResult> Delete(int challengeId)
        {
            try
            {
                var oldchallenge = await UnitOfWork.GetChallengeByIdAsync(challengeId);
                if (oldchallenge == null) return NotFound();

                await UnitOfWork.RemoveChallengeAsync(oldchallenge.ChallengeId);

                if (await UnitOfWork.SaveChangesAsync())
                {
                    return Ok();
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the camp");
        }

    }
}
