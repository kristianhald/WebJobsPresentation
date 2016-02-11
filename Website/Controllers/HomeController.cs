using Microsoft.ServiceBus.Messaging;
using Shared;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Report(ReportingFile report)
        {
            var messagingFactory = MessagingFactory.CreateFromConnectionString("Endpoint=sb://kviksag.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ru4LnxhI8qtgHtcRb9U1LROIYl86qJJRudM/cz3tgfw=");
            var queueClient = messagingFactory.CreateQueueClient(QueueNames.ProcessingQueue);
            await queueClient.SendAsync(new BrokeredMessage(report));

            return Redirect("Index");
        }
    }
}