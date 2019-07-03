using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsClubModel;
using SportsClubModel.Interfaces;
using SportsClubWeb.DTO;
using Microsoft.AspNetCore.Routing;

namespace SportsClubWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;

        public ReservationController(IReservationUnitOfWork unitOfWork, IMapper mapper, LinkGenerator linkGenerator)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<ReservationsDTO[]>> Get()
        {
            try
            {
                var results = await UnitOfWork.GetAllReservationsAsync();

                return Mapper.Map<ReservationsDTO[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("fieldId")]
        public async Task<ActionResult<ReservationsDTO[]>> GetByField(int fieldId)
        {
            try
            {
                var results = await UnitOfWork.GetReservationsByFieldAsync(fieldId);

                return Mapper.Map<ReservationsDTO[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("userId")]
        public async Task<ActionResult<ReservationsDTO[]>> GetByUser(int userId)
        {
            try
            {
                var results = await UnitOfWork.GetReservationsByUserIdAsync(userId);

                return Mapper.Map<ReservationsDTO[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("date")]
        public async Task<ActionResult<ReservationsDTO[]>> GetInADate(DateTime start, DateTime end)
        {
            try
            {
                var results = await UnitOfWork.GetReservationsByDateAsync(start, end);

                return Mapper.Map<ReservationsDTO[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> Delete(int reservationId)
        {
            try
            {
                var result = await UnitOfWork.GetReservationByReservationIdAsync(reservationId);
                if (result == null) return NotFound();

                await UnitOfWork.RemoveReservationAsync(reservationId);

                
                return Ok();
                
            }
            catch(Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed to delete the reservation");
        }

        [HttpPut("{reservationId}")]
        public async Task<ActionResult<ReservationsDTO>> Put(int reservationId,[FromBody]ReservationsDTO reservationsDTO)
        {
            try
            {
                var oldReservation = await UnitOfWork.GetReservationByReservationIdAsync(reservationId);
                if (oldReservation == null) return NotFound($"Could not found resevation with this id: {reservationId}");

                Mapper.Map(reservationsDTO, oldReservation);

                if(await UnitOfWork.SaveChangesAsync())
                {
                    return Mapper.Map<ReservationsDTO>(oldReservation);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<ReservationsDTO>> Post([FromBody]ReservationsDTO reservationsDTO)
        {
            try
            {
                var location = LinkGenerator.GetPathByAction("Get", "Reservation", new { id = reservationsDTO.ReservationId });
                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current Id");
                }

                var newres = Mapper.Map<Reservation>(reservationsDTO);

                await UnitOfWork.AddReservationAsync(newres);
                
                return Created($"/api/reservation/{reservationsDTO.ReservationId}", Mapper.Map<ReservationsDTO>(newres));

            }
            catch(Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        }

    }
}