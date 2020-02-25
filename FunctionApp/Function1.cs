using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionApp
{
    public static class Function1
    {
        [FunctionName("Calculate")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Queue("Order")] IAsyncCollector<Order> orderQueue,
           ILogger log)
        {
            log.LogInformation("ReceivedOrder");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var  data = JsonConvert.DeserializeObject<Order>(requestBody);
            await orderQueue.AddAsync(data);

            // name = name ?? data?.age;
            return (ActionResult)new OkObjectResult($"Order id  is , " +data.OrderID);
            //return name != null? (ActionResult)new OkObjectResult($"Hello, {name}"): new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }

    public class Order
        {
        public int OrderID { get; set; }
        public string Quantity { get; set; }
        public string Email { get; set; }
        

    }
}
