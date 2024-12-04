using _6_WebSocket.Models;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Xml.Linq;

namespace Client.Service
{
    public class TicketService
    {
        private const string base_url = "http://localhost:5170";

        public async Task<List<Ticket>> GetTickets()
        {
            string url = base_url + "/api/tickets";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Ticket>>(json);
                return result;
            }
        }

        public async Task<Ticket> GetTicketsById(int id)
        {
            string url = base_url + "/api/tickets";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Ticket>>(json);
               return result.Find(x => x.Id == id);
            }
        }

        public async Task<Ticket> PostTicket(Ticket ticket)
        {
            string url = base_url + "/api/tickets";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(url, ticket);
                string json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Ticket>(json);
                return result;
            }
        }

        public async void PatchTicket(int id)
        {
			string url = base_url + "/api/tickets/" + id + "/status";
			using (HttpClient client = new HttpClient())
			{
				var response = await client.PatchAsJsonAsync(url, "Closed");
			}
		}

        public async Task<Comment> PostComment(Comment comment)
        {
            string url = base_url + "/api/tickets/" + comment.TicketId + "/comments";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(url, comment);
                string json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Comment>(json);
                return result;
            }
        }

    }
}
