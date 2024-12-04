using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class Chat : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
