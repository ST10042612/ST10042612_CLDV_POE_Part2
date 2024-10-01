using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace ABC_Retail.Controllers
{
    public class QueueController : Controller
    {
        private readonly QueueServiceClient _queueServiceClient;
        public QueueController(IConfiguration configuration)
        {
            _queueServiceClient = new QueueServiceClient(configuration.GetConnectionString("StorageConnectionString"));
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            var queueClient = _queueServiceClient.GetQueueClient("create-product");
            await queueClient.CreateIfNotExistsAsync();
            await queueClient.SendMessageAsync(message);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ReceiveMessage()
        {
            var queueClient = _queueServiceClient.GetQueueClient("create-product");
            var receivedMessage = await queueClient.ReceiveMessageAsync();
            if (receivedMessage != null)
            {
                return View(receivedMessage.Value.MessageText);
            }
            return View("No message available");
        }
    }
}