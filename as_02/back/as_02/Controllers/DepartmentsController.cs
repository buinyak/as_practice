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
    [Route("departments/[action]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        [HttpPost]
        public IActionResult Create([FromBody]  Department department)
        {
            try
            {
                return Ok(_departmentRepository.Create(department));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut] 
        public IActionResult Update([FromBody]  Department department)
        {
            try
            {
                return Ok(_departmentRepository.Update(department));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult GetMidSalaries()
        {
            try
            {

                return Ok(_departmentRepository.GetMidSalaries());
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
                return Ok(_departmentRepository.Get(id));
                
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
                return Ok(_departmentRepository.GetAll());

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
                _departmentRepository.Delete(id);
                return Ok("Deleted");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
    
}
