using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;
using Mailer.Dto;
using Mailer.Logic;
using Mailer.Models;

namespace Mailer.Controllers
{
    public class HomeController : Controller
    {
       
        private ApplicationDbContext _context;
        private readonly IMapCustomerData _mapCustomerData;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _mapCustomerData = new MapCustomerData();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            return View("SendEmailManually");
        }

        public ActionResult SendEmailManually()
        {
            return View();
        }

        public async Task<bool> ManualSend(CustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _mapCustomerData.SendEmailLogic(customerDto);
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<ActionResult> Test(CustomerDto customerDto)
        {
            //await _mapCustomerData.SaveCustomerDetails(coreElements);
            Debug.WriteLine("Ok");
            return View(customerDto);
        }
    }
}
