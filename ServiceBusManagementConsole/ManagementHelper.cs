using Azure.Messaging.ServiceBus.Administration;
using System;
using System.Threading.Tasks;



namespace ServiceBusManagementConsole
{
    internal class ManagementHelper
    {


        private ServiceBusAdministrationClient m_AdministrationClient;

        public ManagementHelper(string connectionString)
        {
            // Create a ServiceBusAdministrationClientu using the specified credentials.
            m_AdministrationClient = new ServiceBusAdministrationClient(connectionString);
        }



        public async Task CreateQueueAsync(string queueName)
        {
            Console.Write("Creating queue {0}...", queueName);
            var createQueueOptions = GetCreateQueueOptions(queueName);
            var response = await m_AdministrationClient.CreateQueueAsync(createQueueOptions);
            var queueProperties = response.Value;

            Console.WriteLine("Done!");
        }

        public async Task DeleteQueueAsync(string queueName)
        {
            Console.Write("Deleting queue {0}...", queueName);
            await m_AdministrationClient.DeleteQueueAsync(queueName);

            Console.WriteLine("Done!");
        }

        public async Task ListQueuesAsync()
        {
            var queuePropertiesList = m_AdministrationClient.GetQueuesAsync();
            Console.WriteLine("Listing queues...");
            await foreach (var queueProperties in queuePropertiesList)
            {
                Console.WriteLine("\t{0}", queueProperties.Name);
            }
            Console.WriteLine("Done!");
        }

        public async Task GetQueueAsync(string queueName)
        {
            var response = await m_AdministrationClient.GetQueueAsync(queueName);
            var queueProperties = response.Value;
            Console.WriteLine($"Queue description for { queueName }");
            Console.WriteLine($"    Name:                                { queueProperties.Name }");
            Console.WriteLine($"    MaxSizeInMegabytes:                  { queueProperties.MaxSizeInMegabytes }");
            Console.WriteLine($"    RequiresSession:                     { queueProperties.RequiresSession }");
            Console.WriteLine($"    RequiresDuplicateDetection:          { queueProperties.RequiresDuplicateDetection }");
            Console.WriteLine($"    DuplicateDetectionHistoryTimeWindow: { queueProperties.DuplicateDetectionHistoryTimeWindow }");
            Console.WriteLine($"    LockDuration:                        { queueProperties.LockDuration }");
            Console.WriteLine($"    DefaultMessageTimeToLive:            { queueProperties.DefaultMessageTimeToLive }");
            Console.WriteLine($"    DeadLetteringOnMessageExpiration:    { queueProperties.DeadLetteringOnMessageExpiration }");
            Console.WriteLine($"    EnableBatchedOperations:             { queueProperties.EnableBatchedOperations }");
            Console.WriteLine($"    MaxDeliveryCount:                    { queueProperties.MaxDeliveryCount }");
            Console.WriteLine($"    Status:                              { queueProperties.Status }");
        }



        public async Task CreateTopicAsync(string topicName)
        {
            Console.Write("Creating topic {0}...", topicName);
            var response = await m_AdministrationClient.CreateTopicAsync(topicName);
            var topicProperties = response.Value;

            Console.WriteLine("Done!");
        }


        public async Task CreateSubscriptionAsync(string topicName, string subscriptionName)
        {
            Console.Write("Creating subscription {0}/subscriptions/{1}...", topicName, subscriptionName);
            var response = await m_AdministrationClient.CreateSubscriptionAsync(topicName, subscriptionName);
            var subscriptionProperties = response.Value;

            Console.WriteLine("Done!");
        }


        public async Task ListTopicsAndSubscriptionsAsync()
        {
            var topicPropertiesList = m_AdministrationClient.GetTopicsAsync();
            Console.WriteLine("Listing topics and subscriptions...");
            await foreach (var topicProperties in topicPropertiesList)
            {
                Console.WriteLine("\t{0}", topicProperties.Name);
                var subscriptionPropertiesList = m_AdministrationClient.GetSubscriptionsAsync(topicProperties.Name);
                await foreach (var subscriptionProperties in subscriptionPropertiesList)
                {
                    Console.WriteLine("\t\t{0}", subscriptionProperties.SubscriptionName);
                }
            }
            Console.WriteLine("Done!");
        }




        public CreateQueueOptions GetCreateQueueOptions(string queueName)
        {

            

            return new CreateQueueOptions(queueName)
            {
                RequiresDuplicateDetection = true,
                DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(10),
                RequiresSession = true,
                MaxDeliveryCount = 20,
                DefaultMessageTimeToLive = TimeSpan.FromHours(1),
                DeadLetteringOnMessageExpiration = true
            };
        }


    }
}
