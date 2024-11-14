using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;
using src.DTO;

namespace src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartAnalysisController : ControllerBase
    {
        private readonly string _apiKey;

        public CartAnalysisController(IConfiguration configuration)
        {
            _apiKey = configuration["OpenAi:Key"];
        }

        // api/v1/CartAnalysis
        [HttpPost]  // Use POST to send the DTO in the request body
        public async Task<IActionResult> AnalyzeCart([FromBody] CartAnalysisRequestDto request)
        {
            // Log the received products
            Console.WriteLine("Received products: " + string.Join(", ", request.Products));

            if (request.Products == null || request.Products.Count == 0)
            {
                return BadRequest("No products provided.");
            }

            ChatClient client = new ChatClient(model: "gpt-4o", apiKey: _apiKey);
            string prompt = "Here is a list of items in a customer's cart: " + string.Join(", ", request.Products) + ".\n" +
                "Provide a brief analysis focusing on product compatibility, performance, and recommendations for improvement. Avoid using any special characters or formatting. Keep the response plain text, concise, and actionable."
                + "\n don't use * in your response And keep it limited to 10 lines maximum and Talk to the customer directly";


            var response = await client.CompleteChatAsync(prompt);
            var feedback = response.Value;
            return Ok(feedback);
        }
    }

}
