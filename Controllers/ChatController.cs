
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Wedding_Planner.Models;
using Newtonsoft.Json;


namespace Wedding_Planner.Controllers // Apna namespace check kar lein
{
    public class ChatController : Controller
    {
        // Database connection string (Aapke project ke mutabik)
        Wedding_PlannerEntities db = new Wedding_PlannerEntities();

        // 🟢 Step 1: Gemini API Key paste karein
        private readonly string _apiKey = "AQ.Ab8RN6IJPRFA5zZyrig-wXZOtCjO8D2k3Mxx3MhSU5Uf42VHUA";

        [HttpPost]
        public async Task<JsonResult> Ask(string message)
        {
            string reply = "Sorry, I am facing some issues. Please try again.";

            try
            {
                using (var client = new HttpClient())
                {
                    // Gemini 1.5 Flash Endpoint
                    //string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=" + _apiKey;
                    string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-flash-latest:generateContent?key=" + _apiKey;
                    var requestBody = new
                    {
                        contents = new[] {
                    new { parts = new[] { new { text = message } } }
                }
                    };

                    var json = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        dynamic data = JsonConvert.DeserializeObject(result);
                        // Safe navigation to get the text
                        if (data.candidates != null && data.candidates[0].content != null)
                        {
                            reply = data.candidates[0].content.parts[0].text;
                        }
                    }
                    else
                    {
                        reply = "API Error: " + response.StatusCode;
                    }
                }

                // 💾 Database mein save karein
                ChatMessage chat = new ChatMessage
                {
                    UserMessage = message,
                    BotReply = reply,
                    CreatedAt = DateTime.Now
                };
                db.ChatMessages.Add(chat);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                reply = "System Error: " + ex.Message;
            }

            // Sirf string return kar rahe hain JS ke liye
            return Json(reply);
        }

        // 📜 Chat History Load karne ke liye
        public JsonResult GetHistory()
        {
            var history = db.ChatMessages
                            .OrderByDescending(c => c.CreatedAt)
                            .Take(15)
                            .ToList()
                            .Select(c => new { user = c.UserMessage, bot = c.BotReply })
                            .Reverse();

            return Json(history, JsonRequestBehavior.AllowGet);
        }

      
    }
}

