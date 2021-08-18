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
    [Route("skills/[action]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;

        public SkillsController(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        [HttpPost]
        public IActionResult Create([FromBody]  Skill skill)
        {
            try
            {
                return Ok(_skillRepository.Create(skill));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut] 
        public IActionResult Update([FromBody]  Skill skill)
        {
            try
            {
                
                return Ok(_skillRepository.Update(skill));
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

                return Ok(_skillRepository.Get(id));             
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
                return Ok(_skillRepository.GetAll());

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
                _skillRepository.Delete(id);
                return Ok("Deleted");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
    
}
