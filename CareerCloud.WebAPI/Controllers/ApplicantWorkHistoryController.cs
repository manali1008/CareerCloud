﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantWorkHistoryController : ControllerBase
    {
        private ApplicantWorkHistoryLogic _logic;
        public ApplicantWorkHistoryController()
        {
            EFGenericRepository<ApplicantWorkHistoryPoco> repo = new EFGenericRepository<ApplicantWorkHistoryPoco>();
            _logic = new ApplicantWorkHistoryLogic(repo);
        }

        [HttpGet("workHistory/{applicantWorkHistoryId}")]
        [ResponseType(typeof(ApplicantWorkHistoryPoco))]
        public ActionResult GetApplicantWorkHistory(Guid applicantWorkHistoryId)
        {
            ApplicantWorkHistoryPoco poco = _logic.Get(applicantWorkHistoryId);

            if (poco == null)
            {
                return NotFound();
            }

            return Ok(poco);
        }

        [HttpGet("workHistory")]
        [ResponseType(typeof(List<ApplicantWorkHistoryPoco>))]
        public ActionResult GetAllApplicantWorkHistory()
        {
            List<ApplicantWorkHistoryPoco> pocos = _logic.GetAll();

            if (pocos == null)
            {
                return NotFound();
            }

            return Ok(pocos);
        }

        [HttpPost("workHistory")]
        public ActionResult PostApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] pocos)
        {
            try
            {
                _logic.Add(pocos);

                return Ok();
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("workHistory")]
        public ActionResult PutApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] pocos)
        {
            try
            {
                _logic.Update(pocos);
                return Ok();
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("workHistory")]
        public ActionResult DeleteApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] pocos)
        {
            try
            {
                _logic.Delete(pocos);
                return Ok();
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
