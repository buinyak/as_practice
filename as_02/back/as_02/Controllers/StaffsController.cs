using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using as_02.Models;
using as_02.Interfaces;
using as_02.Services;
using as_02.Services.Interfaces;

namespace as_02.Controllers
{
    [ApiController]
    [Route("staffs/[action]")]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IStaffService _staffService;

        public StaffsController(IStaffRepository staffRepository,IStaffService staffService)
        {
            _staffRepository = staffRepository;
            _staffService = staffService;
        }
        [HttpPost]
        public IActionResult Create([FromBody]  Staff staff)
        {
            try
            {
                ;
                return Ok(_staffRepository.Create(staff));
            }
            catch
            {
                return BadRequest();
            }
        }

        
        [HttpPost]
        public IActionResult CreateWithSkills([FromBody]  Staff staff)
        {
            try
            {
                
                return Ok(_staffService.CreateWithSkills(staff));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult UpdateWithSkills([FromBody]  Staff staff)
        {
            try
            {

                return Ok(_staffService.UpdateWithSkills(staff));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody]  Staff staff)
        {
            try
            {
                
                return Ok(_staffRepository.Update(staff));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try 
            {
                return Ok(_staffRepository.Get(id));
                
            } 
            catch 
            {
                return BadRequest();
            } 
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_staffRepository.GetAll());

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public ActionResult GetAllWithDepNames()
        {
            try
            {
                return Ok(_staffRepository.GetAll());

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _staffRepository.Delete(id);
                return Ok("Deleted");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public ActionResult GetAllByDepartmentsWithSkills()
        {
            try
            {
                return Ok(_staffRepository.GetAllByDepartmentsWithSkills());

            }
            catch
            {
                return BadRequest();
            }
        }

    }
    
}
