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
            HttpManager.addPrefix("http://localhost:8080/");

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
