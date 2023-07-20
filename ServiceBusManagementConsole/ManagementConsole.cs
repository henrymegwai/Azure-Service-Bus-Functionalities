using System;
using System.Threading.Tasks;

namespace ServiceBusManagementConsole
{
    internal class ManagementConsole
    {
        // Enter a valid Service Bus connection string
        private static string ServiceBusConnectionString = "copy and paste the connection string of your service bus. This bad practise though, however just for test purpose";


        static async Task Main(string[] args)
        {
            ManagementHelper helper = new ManagementHelper(ServiceBusConnectionString);




            bool done = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(">");
                string commandLine = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Magenta;
                string[] commands = commandLine.Split(' ');

                try
                {
                    if (commands.Length > 0)
                    {
                        switch (commands[0])
                        {
                            case "createqueue":
                            case "cq":
                                if (commands.Length > 1)
                                {
                                    await helper.CreateQueueAsync(commands[1]);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Queue name not specified.");
                                }
                                break;
                            case "listqueues":
                            case "lq":
                                helper.ListQueuesAsync().Wait();
                                break;
                            case "getqueue":
                            case "gq":
                                if (commands.Length > 1)
                                {
                                    await helper.GetQueueAsync(commands[1]);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Queue name not specified.");
                                }
                                break;
                            case "deletequeue":
                            case "dq":
                                if (commands.Length > 1)
                                {
                                    await helper.DeleteQueueAsync(commands[1]);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Queue name not specified.");
                                }
                                break;
                            case "createtopic":
                            case "ct":
                                if (commands.Length > 1)
                                {
                                    await helper.CreateTopicAsync(commands[1]);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Topic name not specified.");
                                }
                                break;
                            case "createsubscription":
                            case "cs":
                                if (commands.Length > 2)
                                {
                                    await helper.CreateSubscriptionAsync(commands[1], commands[2]);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Topic name not specified.");
                                }
                                break;
                            case "listtopics":
                            case "lt":
                                await helper.ListTopicsAndSubscriptionsAsync();
                                break;
                            case "help":
                                Console.WriteLine("cq createqueue [queueName]");
                                Console.WriteLine("lq listqueues");
                                Console.WriteLine("gq getqueue [queueName]");
                                Console.WriteLine("dq deletequeue");
                                Console.WriteLine("ct createtopic [topicName]");
                                Console.WriteLine("cs createsubscription [topicName] [subscriptionName]");
                                Console.WriteLine("lt listtopics");
      
                                break;
                            case "exit":
                                done = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }

            } while (!done);
        }
    }
}
