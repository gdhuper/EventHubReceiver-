using System;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System.Threading.Tasks;

namespace EventHubProcessor
{
    class Program 
    { 

    private const string EventHubConnectionString = "Endpoint=sb://serpent.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=0rpkvrmh62O2ZIwEsfDGr6Bj0ALicE62OxuYoTsYxYw=";
    private const string EventHubName = "testeventhub";
    private const string StorageContainerName = "eventhubcontainer";
    private const string StorageAccountName = "brbblob";
    private const string StorageAccountKey = "pD0dOC+BHWkYqG9CHX1wVLY3d8sim9IjqUxtwmhjKDFyR1M4BUkj8hRgIm52KJym0iJQJrZqREaDEVC7UheAZw==";

    private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);
    
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Registering EventProcessor....");

            var eventProcessorHost = new EventProcessorHost(
            EventHubName,
            PartitionReceiver.DefaultConsumerGroupName,
            EventHubConnectionString,
            StorageConnectionString,
            StorageContainerName
            );
              

            // Create an instance of IEventProcessorFactory with dependencies (connection string in this case) to inject into host 
            IEventProcessorFactory processorFactory = new ProcessorFactory("some connection string");

            // Register the Event Processor host and starts receiving messages 
            await eventProcessorHost.RegisterEventProcessorFactoryAsync(processorFactory);

            Console.WriteLine("Receiving. Press Enter to stop worker");

            Console.ReadLine();

            //Dispose of the Event Processor Host 
           await eventProcessorHost.UnregisterEventProcessorAsync();



        }
    }

   
}
