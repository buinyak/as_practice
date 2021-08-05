﻿using System;
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
    [Route("Departments")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        [HttpPost]
        [Route("/create")]
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
        [Route("/update")]  
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
        [HttpGet]
        [Route("/get/{id}")]
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
        [Route("/getAll")]
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
