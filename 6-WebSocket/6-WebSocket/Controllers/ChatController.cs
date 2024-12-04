using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _6_WebSocket.Controllers
{
    public class Chat : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
