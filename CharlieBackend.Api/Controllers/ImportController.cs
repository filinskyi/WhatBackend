﻿using CharlieBackend.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CharlieBackend.Core.DTO.File;
using Microsoft.AspNetCore.Authorization;
using CharlieBackend.Business.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using CharlieBackend.Core.FileModels;
using System.Collections.Generic;

namespace CharlieBackend.Api.Controllers
{
    [Route("api/imports")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IFileImportService _importService;

        public ImportController(IFileImportService importService)
        {
            _importService = importService;
        }

        /// <summary>
        /// Imports data from file to application
        /// </summary>
        /// <response code="200">Successful import of data from file</response>
        /// <response code="400">File validation error</response>
        [SwaggerResponse(200, type: typeof(List<StudentGroupFileModel>))]
        [Authorize(Roles = "Mentor, Secretary, Admin")]
        [HttpPost]
        public async Task<ActionResult> ImportDataFromFile(ImportFileDto file)
        {
            var listOfImportedGroups = await _importService.ImportFileAsync(file);

            return listOfImportedGroups.ToActionResult();
        }
    }
}
