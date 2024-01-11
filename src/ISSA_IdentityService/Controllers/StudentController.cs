using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Contract.Service.Interface;
using ISSA_IdentityService.Controllers;
using ISSA_IdentityService.Core.Models;
using ISSA_IdentityService.Core.Models.Common;
using ISSA_IdentityService.Core.QueryObject;
using Microsoft.AspNetCore.Mvc;

namespace ISSA_IdentityService.Controllers
{
    [ApiController]
    public class StudentController(IStudentService service) : ApiControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] StudentQuery query )
        {
            try
            {
                var result = await service.GetPaginatedAsync(query);
                return Ok(new BaseResponseModel<PaginatedList<Student>?>(StatusCodes.Status200OK, data: result));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel<string>(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await service.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new BaseResponseModel<string>(StatusCodes.Status404NotFound, "Student not found"));

                }
                return Ok(new BaseResponseModel<Student?>(StatusCodes.Status200OK, data: result));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel<string>(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] StudentModel model)
        {
            try
            {
                var result = await service.CreateAsync(model);
                return Created("" , new BaseResponseModel<string>(StatusCodes.Status201Created, data: result));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel<string>(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(string id, [FromBody] StudentModel model)
        {
            try
            {
                var result = await service.UpdateAsync(id, model);
                if (result == 0)
                {
                    return NotFound(new BaseResponseModel<string>(StatusCodes.Status404NotFound, "Student not found"));
                }
                return Accepted(new BaseResponseModel<int>(StatusCodes.Status200OK, data: result));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel<string>(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await service.DeleteAsync(id);
                if (result == 0)
                {
                    return NotFound(new BaseResponseModel<string>(StatusCodes.Status404NotFound, "Student not found"));
                }
                return Accepted(new BaseResponseModel<int>(StatusCodes.Status200OK, data: result));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel<string>(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }
    }
}