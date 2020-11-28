﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CharlieBackend.Core.DTO;
using CharlieBackend.Core.DTO.Mentor;
using Microsoft.AspNetCore.Authorization;
using CharlieBackend.Business.Services.Interfaces;
using CharlieBackend.Core.Entities;
using CharlieBackend.Core.Models.ResultModel;
using CharlieBackend.Core;
using Swashbuckle.AspNetCore.Annotations;

namespace CharlieBackend.Api.Controllers
{
    /// <summary>
    /// Controller to manage mentors and related data
    /// </summary>
    [Route("api/mentors")]
    [ApiController]
    public class MentorsController : ControllerBase
    {
        #region
        private readonly IMentorService _mentorService;
        private readonly IAccountService _accountService;
        #endregion
        /// <summary>
        /// Mentors controller constructor
        /// </summary>
        public MentorsController(IMentorService mentorService, IAccountService accountService)
        {
            _mentorService = mentorService;
            _accountService = accountService;
        }

        /// <summary>
        /// Assign account to mentor
        /// </summary>
        /// <response code="200">Successful assigning of account to mentor</response>
        /// <response code="HTTP: 404, API: 3">Can not find account</response>
        /// <response code="HTTP: 400, API: 0">Error, account already assigned</response>
        [SwaggerResponse(200, type: typeof(MentorDto))]
        [Authorize(Roles = "Admin, Secretary")]
        [HttpPost("{accountId}")]
        public async Task<ActionResult> PostMentor(long accountId)
        {
            var createdMentorModel = await _mentorService.CreateMentorAsync(accountId);

            return createdMentorModel.ToActionResult(); ;
        }

        /// <summary>
        /// Gets list of all mentors
        /// </summary>
        /// <response code="200">Successful return of mentors list</response>
        [SwaggerResponse(200, type: typeof(IList<MentorDto>))]
        [Authorize(Roles = "Admin, Secretary")]
        [HttpGet]
        public async Task<ActionResult<List<MentorDto>>> GetAllMentors()
        {

            var mentorsModels = await _mentorService.GetAllMentorsAsync();

            return Ok(mentorsModels);
        }

        /// <summary>
        /// Update of mentor
        /// </summary>
        /// <remarks>
        /// **courseIds** and **studentGroupIds** is optional
        /// </remarks>
        /// <response code="200">Successful update of mentor</response>
        /// <response code="HTTP: 404, API: 3">Mentor not found</response>
        /// <response code="HTTP: 400, API: 5">Can not update mentor due to data conflict</response>
        [SwaggerResponse(200, type: typeof(MentorDto))]
        [Authorize(Roles = "Admin, Secretary")]
        [HttpPut("{mentorId}")]
        public async Task<ActionResult> PutMentor(long mentorId, [FromBody]UpdateMentorDto mentorModel)
        {
            var updatedMentor = await _mentorService.UpdateMentorAsync(mentorId, mentorModel);

            return updatedMentor.ToActionResult();
        }

        /// <summary>
        /// Disabling of mentor account
        /// </summary>
        /// <response code="204">Successful disabling of mentor</response>
        /// <response code="HTTP: 400, API: 3">Can not find mentor</response>
        [Authorize(Roles = "Admin, Secretary")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DisableMentor(long id)
        {

            var accountId = await _mentorService.GetAccountId(id);

            if (accountId == null)
            {
                return BadRequest("Unknown mentor id.");
            }

            var isDisabled = await _accountService.DisableAccountAsync((long)accountId);

            if (isDisabled)
            {
                return NoContent();
            }

            return StatusCode(500, "Error occurred while trying to disable mentor account.");
        }
    }
}
