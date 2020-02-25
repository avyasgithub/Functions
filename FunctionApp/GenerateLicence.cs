
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using FunctionApp;
using System.IO;

namespace FunctionApp
{
    public static class GenerateLicence
    {
        [FunctionName("GenerateLicence")]
        public static void Run([QueueTrigger("order", Connection = "AzureWebJobsStorage")]Order orders,
            [Blob("licence/{rand-guid}.json")] TextWriter outPutBlob,
            ILogger log)
        {
            outPutBlob.WriteLine($"Order Id: {orders.OrderID}" );
            outPutBlob.WriteLine($"Quantity: {orders.Quantity}");
            outPutBlob.WriteLine($"EmailId: {orders.Email}");
            
            log.LogInformation($"Order has been send to Blobl {orders}");
        }
    }
}
