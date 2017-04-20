using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Compsci215ProxyServer {
    class Program {
        static void Main(string[] args) {
            //create an HttpListenerInstance and start it
            //HttpManager.addPrefix("http://localhost:8080/");
            //get all local ip and display them, allow the user to choose

            IPAddress[] IP_Addresses = Dns.GetHostEntry(Environment.MachineName).AddressList;

            Console.WriteLine("Please select which ip address you would like to use: \n");
            int counter = 1;
            foreach(IPAddress e in IP_Addresses) {
                Console.WriteLine((counter++).ToString() + ": " + e.ToString());

            }
            Console.WriteLine("Which one? : ");
            string input = Console.ReadLine();

            string IP_address = IP_Addresses[int.Parse(input) - 1].ToString();

            HttpManager.addPrefix("http://" + IP_address + ":8080/");

            HttpManager.startListener();
            
            while (true) {
                Console.WriteLine("Listening....");
                HttpListenerContext response = HttpManager.HttpListenerInstance.GetContext();
                //System.Console.WriteLine(response.Request);
                Console.WriteLine("You have been heard!");
                Console.WriteLine("Sending Request...");
                HttpManager.sendRequest(response);
                

            }
        }
    }
}
