// Pages/AjaxForm.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Projekt1.Pages
{
    public class AjaxFormModel : PageModel
    {
        public string ResponseMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostSendMessage([FromBody] MessageModel message)
        {
            if (message == null)
            {
                return NotFound("Invalid message.");
            }

            // Example: Setting the response message in the model
            ResponseMessage = $"Received '{message.Content}' from {message.Name}.";

            // Return response as JSON
            return new JsonResult(new { message = ResponseMessage });
        }
    }

    public class MessageModel
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
