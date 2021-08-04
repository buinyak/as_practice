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
    public class TxtFilesController : ControllerBase
    {
        private readonly ITxtFileService _txtFileService;

        public TxtFilesController(ITxtFileService txtFileService)
        {
            _txtFileService = txtFileService;
        }
        [HttpPost]
        [Route("/create")]
        public IActionResult Create([FromBody]  TxtFile txtFile)
        {
            try
            {
                _txtFileService.CreateTxtFile(txtFile);
                return Ok("Ok");
            }
            catch
            {
                return Ok("Error");
            }
        }
        [HttpPost]
        [Route("/update")]  
        public IActionResult Update([FromBody]  TxtFile txtFile)
        {
            try
            {
                _txtFileService.UpdateTxtFile(txtFile);
                return Ok("Ok");
            }
            catch
            {
                return Ok("Error");
            }
        }
        [HttpGet]
        [Route("/get/{name}")]
        public ActionResult Get(string name)
        {
            try 
            {
                TxtFile txtFile = _txtFileService.GetTxtFilebyName(name);
                if (txtFile == null)
                {
                    return Ok("Файл не найден");
                }else {
                    return Ok(txtFile);
                }
                
            } 
            catch 
            {
                return Ok("Error");
            } 
        }
        [HttpGet]
        [Route("/getAll")]
        public ActionResult GetAll()
        {
            try
            {
                List<TxtFile> txtFiles= _txtFileService.GetAllTxtFiles();
                if (txtFiles.Count == 0)
                {
                    return Ok("Файлов нет");
                }
                else
                {
                    return Ok(txtFiles);
                }

            }
            catch
            {
                return Ok("Error");
            }
        }
    }
    
}