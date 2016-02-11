using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SendGrid;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Shared;
using System;
using System.Net.Mail;
using Microsoft.Azure.WebJobs.Host;

namespace ProcessingJob
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var jobHostConfiguration = new JobHostConfiguration();
            jobHostConfiguration.UseServiceBus(new ServiceBusConfiguration
            {
                ConnectionString = "Endpoint=sb://kviksag.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ru4LnxhI8qtgHtcRb9U1LROIYl86qJJRudM/cz3tgfw="
            });

            jobHostConfiguration.UseCore();

            jobHostConfiguration.DashboardConnectionString = "DefaultEndpointsProtocol=https;AccountName=kviksag;AccountKey=3qbeYTgN13XDOVA6sqGn7OyZXs2TcHt3Z+TomEs0GU7GCK7VC8A+hUaMvawbkUshhFLKCCDXrpdisOq6nZ1VNQ==;BlobEndpoint=https://kviksag.blob.core.windows.net/;TableEndpoint=https://kviksag.table.core.windows.net/;QueueEndpoint=https://kviksag.queue.core.windows.net/;FileEndpoint=https://kviksag.file.core.windows.net/";
            jobHostConfiguration.StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=kviksag;AccountKey=3qbeYTgN13XDOVA6sqGn7OyZXs2TcHt3Z+TomEs0GU7GCK7VC8A+hUaMvawbkUshhFLKCCDXrpdisOq6nZ1VNQ==;BlobEndpoint=https://kviksag.blob.core.windows.net/;TableEndpoint=https://kviksag.table.core.windows.net/;QueueEndpoint=https://kviksag.queue.core.windows.net/;FileEndpoint=https://kviksag.file.core.windows.net/";

            var jobHost = new JobHost(jobHostConfiguration);
            jobHost.RunAndBlock();
        }

        public static void Processor(
            [ServiceBusTrigger(QueueNames.ProcessingQueue)] ReportingFile report,
            string name,
            [Blob("casedata/{name} data.txt")] out string caseData)
        {
            Console.WriteLine("Report received. Name: {0}, Data: {1}", report.Name, report.CaseData);

            caseData = report.CaseData;

            throw new Exception(name);
        }

        public static void ErrorMonitor(
            [ErrorTrigger()] TraceEvent error)
        {
            Console.WriteLine("Error: {0}", error.Exception.Message);
        }
    }
}
