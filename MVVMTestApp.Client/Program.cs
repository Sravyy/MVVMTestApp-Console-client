using Microsoft.AspNet.SignalR.Client;
using MVVMTestApp.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTestApp.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new HubConnection("http://localhost:52595/");
            var myHub = connection.CreateHubProxy("MyHub");

            connection.Start().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();

            myHub.On<List<SmsContactState>>("PassContactsToClients", contacts =>
            {
                Console.WriteLine(contacts);
            });
             myHub.Invoke<string>("GetContacts", "doesnt matter");

            Console.Read();
            connection.Stop();
        }
    }
}
