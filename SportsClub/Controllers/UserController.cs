using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsClubModel;
using SportsClubWeb.DTO;
using Microsoft.AspNetCore.Routing;
using SportsClubModel.Interfaces;

namespace SportsClubWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;

        public UserController(IUserUnitOfWork unit, IMapper mapper, LinkGenerator linkGenerator)
        {
            UnitOfWork = unit;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<UserDTO[]>> Get()
        {
            try
            {
                var results = await UnitOfWork.GetAllUsersAsync();
                if (!results.Any()) return NotFound();

                return Mapper.Map<UserDTO[]>(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message + e.StackTrace);
            }
        }

        [HttpGet("{token}")]
        public async Task<ActionResult<UserDTO[]>> GetString(string token)
        {
            try
            {
                var results = await UnitOfWork.GetAllUsersByLastNameAsync(token);
                if (!results.Any()) return NotFound();

                return Mapper.Map<UserDTO[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("Date")]
        public async Task<ActionResult<UserDTO[]>> GetInADate(DateTime start, DateTime end)
        {
            try
            {
                var results = await UnitOfWork.GetUsersByDateOfBirthRangeAsync(start, end);
                if (!results.Any()) return NotFound();

                return Mapper.Map<UserDTO[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
                var oldUser = await UnitOfWork.GetUserByIdAsync(userId);
                if (oldUser == null)
                {
                    return NotFound();
                }

                await UnitOfWork.RemoveUserAsync(userId);
                
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the user");
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Post([FromBody]UserDTO dto)
        {
            try
            {
                //Link Generator
                var location = LinkGenerator.GetPathByAction("Get", "User", new {id = dto.UserId});
                if(string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current Id");
                }

                dto.DateOfRegistration = DateTime.Now.Date;

                var user = Mapper.Map<User>(dto);

                //Da risolvere
                await UnitOfWork.AddUserAsync(user);
                
                return Created($"/api/users/{user.UserId}", Mapper.Map<UserDTO>(user));
                
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<UserDTO>> Put(int userId, [FromBody]UserDTO dto)
        {
            try
            {
                var oldUser = await UnitOfWork.GetUserByIdAsync(userId);
                if (oldUser == null)
                {
                    return NotFound($"Could not find user with id: {userId}");
                }
                Mapper.Map(dto, oldUser);

                if (await UnitOfWork.SaveChangesAsync())
                {
                    return Mapper.Map<UserDTO>(oldUser);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        }
    }
}
