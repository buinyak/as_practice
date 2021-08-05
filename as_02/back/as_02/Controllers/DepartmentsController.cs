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
                _departmentRepository.Create(department);
                return Ok(department);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost] 
        public IActionResult Update([FromBody]  Department department)
        {
            try
            {
                _departmentRepository.Update(department);
                return Ok(department);
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
                Department department = _departmentRepository.GetById(id);
                if (department == null)
                {
                    return Ok("Файл не найден");
                }else {
                    return Ok(department);
                }
                
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
                List<Department> departments= _departmentRepository.GetAllDepartments();
                return Ok(departments);

            }
            catch
            {
                return BadRequest();
            }
        }
    }
    
}
