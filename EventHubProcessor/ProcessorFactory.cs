using System;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System.Threading.Tasks;


namespace EventHubProcessor
{
    public class ProcessorFactory : IEventProcessorFactory
    {
        private string connectionStr;
        private int numCreated = 0;

        public ProcessorFactory(string connectionStr)
        {
            this.connectionStr = connectionStr;
            
        }
        public IEventProcessor CreateEventProcessor(PartitionContext context)
        {
            return new SampleEventProcessor(connectionStr);
        }


    }
}
