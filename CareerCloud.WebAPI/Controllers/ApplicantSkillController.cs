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
    public class ApplicantSkillController : ControllerBase
    {
        private ApplicantSkillLogic _logic;
        public ApplicantSkillController()
        {
            EFGenericRepository<ApplicantSkillPoco> repo = new EFGenericRepository<ApplicantSkillPoco>();
            _logic = new ApplicantSkillLogic(repo);
        }

        [HttpGet("skill/{applicantSkillId}")]
        [ResponseType(typeof(ApplicantSkillPoco))]
        public ActionResult GetApplicantSkill(Guid applicantSkillId)
        {
            ApplicantSkillPoco poco = _logic.Get(applicantSkillId);

            if (poco == null)
            {
                return NotFound();
            }

            return Ok(poco);
        }

        [HttpGet("skill")]
        [ResponseType(typeof(List<ApplicantSkillPoco>))]
        public ActionResult GetAllApplicantSkill()
        {
            List<ApplicantSkillPoco> pocos = _logic.GetAll();

            if (pocos == null)
            {
                return NotFound();
            }

            return Ok(pocos);
        }

        [HttpPost("skill")]
        public ActionResult PostApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
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

        [HttpPut("skill")]
        public ActionResult PutApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
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

        [HttpDelete("skill")]
        public ActionResult DeleteApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
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
