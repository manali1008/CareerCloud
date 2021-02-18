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
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemCountryCodeController : ControllerBase
    {
        private SystemCountryCodeLogic _logic;
        public SystemCountryCodeController()
        {
            EFGenericRepository<SystemCountryCodePoco> repo = new EFGenericRepository<SystemCountryCodePoco>();
            _logic = new SystemCountryCodeLogic(repo);
        }

        [HttpGet("countrycode/{systemCountryCode}")]
        [ResponseType(typeof(SystemCountryCodePoco))]
        public ActionResult GetSystemCountryCode(string systemCountryCode)
        {
            SystemCountryCodePoco poco = _logic.Get(systemCountryCode);

            if (poco == null)
            {
                return NotFound();
            }

            return Ok(poco);
        }

        [HttpGet("countrycode")]
        [ResponseType(typeof(List<SystemCountryCodePoco>))]
        public ActionResult GetAllSystemCountryCode()
        {
            List<SystemCountryCodePoco> pocos = _logic.GetAll();

            if (pocos == null)
            {
                return NotFound();
            }

            return Ok(pocos);
        }

        [HttpPost("countrycode")]
        public ActionResult PostSystemCountryCode([FromBody] SystemCountryCodePoco[] pocos)
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
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("countrycode")]
        public ActionResult PutSystemCountryCode([FromBody] SystemCountryCodePoco[] pocos)
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

        [HttpDelete("countrycode")]
        public ActionResult DeleteSystemCountryCode([FromBody] SystemCountryCodePoco[] pocos)
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
