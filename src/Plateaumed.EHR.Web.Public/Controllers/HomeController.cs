using Microsoft.AspNetCore.Mvc;
using Plateaumed.EHR.Web.Controllers;

namespace Plateaumed.EHR.Web.Public.Controllers
{
    public class HomeController : EHRControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}