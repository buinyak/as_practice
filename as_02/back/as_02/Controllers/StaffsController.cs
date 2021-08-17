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

namespace as_02.Controllers
{
    [ApiController]
    [Route("staffs/[action]")]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffRepository _staffRepository;

        public StaffsController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
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
        [HttpGet]
        public ActionResult GetAllWithDepartmentsAndSkills()
        {
            try {
                return Ok(_staffRepository.GetAllWithDepartmentsAndSkills());
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
        [HttpGet("{id}")]
        public IActionResult GetByDepartmentId(int id)
        {
            try
            {

                return Ok(_staffRepository.GetAllByDepartmentId(id));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public ActionResult GetAllStaffsWithDepartments()
        {
            try
            {
                return Ok(_staffRepository.GetAllWithDepartments());

            }
            catch
            {
                return BadRequest();
            }
        }

    }
    
}
