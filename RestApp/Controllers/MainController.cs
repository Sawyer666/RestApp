using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApp.Data;
using RestApp.Models;
using RestApp.Repositories.Interfaces;

namespace RestApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        #region private

        private readonly IRegionRepository<Region> _regionRepository;

        #endregion

        public MainController(IRegionRepository<Region> regionService)
        {
            _regionRepository = regionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRegions()
        {
            try
            {
                return Ok(await _regionRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Region>> CreateRegion(Region region)
        {
            try
            {
                if (region == null)
                {
                    return BadRequest();
                }

                var createdRegion = await _regionRepository.CreateRegion(region);

                return CreatedAtAction(nameof(GetRegion), new {id = createdRegion.Id}, createdRegion);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Region>> GetRegion(int id)
        {
            try
            {
                var result = await _regionRepository.GetRegion(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Region>> UpdateRegion(int id, Region region)
        {
            try
            {
                if (id != region.Id)
                {
                    return BadRequest("Region ID mismatch");
                }

                var regionToUpdate = await _regionRepository.GetRegion(id);

                if (regionToUpdate == null)
                {
                    return NotFound($"Region with Id = {id} not found");
                }
                return await _regionRepository.UpdateRegion(region);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Region>> DeleteRegion(int id)
        {
            try
            {
                var regionToDelete = await _regionRepository.GetRegion(id);
                if (regionToDelete == null)
                {
                    return NotFound($"Region with Id = {id} not found");
                }

                return await _regionRepository.DeleteRegion(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}