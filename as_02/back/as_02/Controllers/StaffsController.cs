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
    [Route("staffs")]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffRepository _staffRepository;

        public StaffsController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }
        [HttpPost]
        [Route("/create")]
        public IActionResult Create([FromBody]  Staff staff)
        {
            try
            {
                _staffRepository.Create(staff);
                return Ok(staff);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("/update")]  
        public IActionResult Update([FromBody]  Staff staff)
        {
            try
            {
                _staffRepository.Update(staff);
                return Ok(staff);
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
                Staff staff = _staffRepository.GetById(id);
                if (staff == null)
                {
                    return Ok("Файл не найден");
                }else {
                    return Ok(staff);
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
                List<Staff> staffs= _staffRepository.GetAllStaffs();
                return Ok(staffs);

            }
            catch
            {
                return BadRequest();
            }
        }
    }
    
}
