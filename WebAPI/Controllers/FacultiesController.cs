using Business.Services.Obs.Abstract;
using Business.Services.Obs.Concrete;
using Entities.ObsEntities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("ObsApi/[controller]")]
    [ApiController]
    public class FacultiesController(IFacultyService facultyService) : ControllerBase
    {
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var result = facultyService.Get(p => p.Id == id);
            var response = Ok(result);

            return await Task.FromResult<IActionResult>(response);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var result = facultyService.GetList();

            var response = Ok(result);

            return await Task.FromResult<IActionResult>(response) ;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Faculty entity)
        {
            var result = facultyService.Add(entity);
            var response = Ok(result);

            return await Task.FromResult<IActionResult>(response);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Faculty entity)
        {
            var result = facultyService.Update(entity);
            var response = Ok(result);

            return await Task.FromResult<IActionResult>(response);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(Faculty entity)
        {
            var result = facultyService.Remove(entity);
            var response = Ok(result);

            return await Task.FromResult<IActionResult>(response);
        }


    }
}
