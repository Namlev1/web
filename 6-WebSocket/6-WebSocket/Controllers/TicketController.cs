using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _6_WebSocket.Models;
using _6_WebSocket.Service;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Sockets;

namespace _6_WebSocket.Controllers
{
    public class TicketController : Controller
    {
       
        TicketService _ticketService = new TicketService();

        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var result = await _ticketService.GetTickets();
            List<Ticket> tickets = new(result);
            return View(tickets);
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Ticket ticket = await _ticketService.GetTicketsById(id);
            return View(ticket);
        }

        // GET: UserController/Create
        public ActionResult CreateTicket()
        {
            return View();
        }

        // POST: UserController/CreateTicket
        [HttpPost]
        public async Task<ActionResult> CreateTicket(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                await _ticketService.PostTicket(ticket);

            }
            return RedirectToAction(nameof(Index));
		}
        public ActionResult CreateComment(int id)
        {
            ViewData["id"] = id;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> CreateComment(Comment comment)
        {
            if (ModelState.IsValid)
            {   
                //int a = (int)ViewData["id"];
                comment.Id = -1;
                await _ticketService.PostComment(comment);

            }
            return RedirectToAction(nameof(Details), new { id = comment.TicketId });
        }

        [HttpPost]
        public ActionResult CloseTicket(int id)
        {
            _ticketService.PatchTicket(id);
			return RedirectToAction(nameof(Index));
		}


	}
}
