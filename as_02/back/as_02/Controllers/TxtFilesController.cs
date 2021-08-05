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
    [Route("txtFiles/[action]")]
    public class TxtFilesController : ControllerBase
    {
        private readonly ITxtFileRepository _txtFileRepository;

        public TxtFilesController(ITxtFileRepository txtFileRepository)
        {
            _txtFileRepository = txtFileRepository;
        }
        [HttpPost]
        public IActionResult Create([FromBody]  TxtFile txtFile)
        {
            try
            {
                _txtFileRepository.CreateTxtFile(txtFile);
                return Ok(txtFile);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult Update([FromBody]  TxtFile txtFile)
        {
            try
            {
                if (txtFile.IsValid())
                {
                    _txtFileRepository.UpdateTxtFile(txtFile);
                    return Ok(txtFile);
                }
                else
                {

                    return Ok(new
                    {
                        error = "Ошибка валидации файла"
                    });
                }

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{name}/{type}")]
        public ActionResult Get(TxtFile txtFile)
        {
            try 
            {
                _txtFileRepository.GetTxtFilebyName(txtFile);
                if (txtFile == null)
                {
                    return Ok("Файл не найден");
                }else {
                    return Ok(txtFile);
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
                var txtFiles= _txtFileRepository.GetAllTxtFiles();
                return Ok(txtFiles);

            }
            catch
            {
                return BadRequest();
            }
        }
    }
    
}
