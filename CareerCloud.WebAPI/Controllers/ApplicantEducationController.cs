using System;
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
    public class ApplicantEducationController : ControllerBase
    {
        private ApplicantEducationLogic _logic;
        public ApplicantEducationController()
        {
            EFGenericRepository<ApplicantEducationPoco> repo = new EFGenericRepository<ApplicantEducationPoco>();
            _logic = new ApplicantEducationLogic(repo);
        }

        [HttpGet("education/{applicantEducationId}")]
        [ResponseType(typeof(ApplicantEducationPoco))]
        public ActionResult GetApplicantEducation(Guid applicantEducationId)
        {
            ApplicantEducationPoco poco = _logic.Get(applicantEducationId);

            if(poco == null)
            {
                return NotFound();
            }

            return Ok(poco);
        }

        [HttpGet("education")]
        [ResponseType(typeof(List<ApplicantEducationPoco>))]
        public ActionResult GetAllApplicantEducation()
        {
            List<ApplicantEducationPoco> pocos = _logic.GetAll();

            if(pocos == null)
            {
                return NotFound();
            }

            return Ok(pocos);
        }

        [HttpPost("education")]
        public ActionResult PostApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
        {
            try
            {
                _logic.Add(pocos);

                return Ok();
            }
            catch(AggregateException ex)
            {
                return BadRequest(ex);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("education")]
        public ActionResult PutApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
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

        [HttpDelete("education")]
        public ActionResult DeleteApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
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
