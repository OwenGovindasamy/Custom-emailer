using System;
using System.Threading.Tasks;
using System.Web.Http;
using Mailer.Models;
using Mailer.Logic;
using Mailer.Dto;

namespace Mailer.Controllers
{

    [RoutePrefix("api/email")]
    public class SendEmailController : ApiController
    {

        #region Dependency injection
        private readonly ApplicationDbContext _context;
        private readonly IMapCustomerData _Logic;
        public SendEmailController(ApplicationDbContext context, MapCustomerData customerData)
        {
            _context = context;
            _Logic = customerData;
        }
     
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        [HttpPost]
        [Route("AutoSendEmail")]
        public async Task<IHttpActionResult> AutoSendEmail(CustomerDto customerDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    return Ok(await _Logic.SendEmailLogic(customerDto));
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("Test")]
        public IHttpActionResult Test(CoreElements coreElements)
        {
            return Ok("Test");
        }
    }
}
