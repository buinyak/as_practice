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
    public class TxtFilesController : ControllerBase
    {
        private readonly ITxtFileRepository _txtFileRepository;
        private readonly IStaffRepository _staffRepository;

        public TxtFilesController(ITxtFileRepository txtFileService)
        {
            _txtFileRepository = txtFileService;
        }
        [HttpPost]
        [Route("/create")]
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
        [Route("/update")]  
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
        [HttpGet]
        [Route("/get/{name}")]
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
        [Route("/getAll")]
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
