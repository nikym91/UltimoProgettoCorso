using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SportsClubModel;
using SportsClubModel.Interfaces;
using SportsClubWeb.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SportsClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly IFieldUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;
        private readonly LinkGenerator LinkGenerator;

        public FieldController(IFieldUnitOfWork unitOfWork, IMapper mapper, LinkGenerator linkGenerator)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            LinkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<FieldDTO[]>> Get(bool includeTalks = false)
        {
            try
            {
                var results = await UnitOfWork.GetAllFieldsAsync();
                if (!results.Any()) return NotFound();

                return Mapper.Map<FieldDTO[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<FieldDTO>> Post([FromBody]FieldDTO dto)
        {
            try
            {
                var location = LinkGenerator.GetPathByAction("Get", "Field", new { id = dto.FieldId });
                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current Id");
                }

                Field field;

                switch(dto.Sport)
                {
                    case Sports.Tennis:
                        field = Mapper.Map<TennisCourt>(dto);
                        break;
                    case Sports.Paddle:
                        field = Mapper.Map<PaddleCourt>(dto);
                        break;
                    case Sports.Soccer:
                        field = Mapper.Map<SoccerField>(dto);
                        break;
                    default: field = null;
                        break;

                }

                await UnitOfWork.AddFieldAsync(field);
                return Created($"/api/field/{field.FieldId}", Mapper.Map<FieldDTO>(field));

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{fieldId}")]
        public async Task<ActionResult<FieldDTO>> Put(int fieldId, [FromBody]FieldDTO dto)
        {
            try
            {
                var oldField = await UnitOfWork.GetFieldByIdAsync(fieldId);
                if (oldField == null)
                {
                    return NotFound($"Could not find field with moniker of {fieldId}");
                }
                        

                Mapper.Map(dto, oldField);

                if (await UnitOfWork.SaveChangesAsync())
                {
                    return Mapper.Map<FieldDTO>(oldField);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{FieldId}")]
        public async Task<IActionResult> Delete(int fieldId)
        {
            try
            {
                var oldField = await UnitOfWork.GetFieldByIdAsync(fieldId);
                if (oldField == null) return NotFound();

                await UnitOfWork.RemoveFieldAsync(oldField.FieldId);

               
                return Ok();


            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the camp");
        }

    }
}
