using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Compsci215ProxyServer {
    // A public static class that manages the Http instance

    static class HttpManager {
        private static TcpListener TcpInstance;
        private static IPAddress TcpIp;
        private static int TcpPort;
        private static HttpClient ClientInstance;
        private static String currentURI;

        public static TcpListener TcpListenerInstance {
            get {
                if (TcpInstance == null) {
                    TcpInstance = new TcpListener(TcpIp, TcpPort);
                    return TcpInstance;
                } else {
                    return TcpInstance;
                }
            }
        }

        //creates a new instance of an HttpClient if one does not exist
        public static HttpClient HttpClientInstance {
            get {
                if (ClientInstance == null) {
                    ClientInstance = new HttpClient();
                    return ClientInstance;
                } else {
                    return ClientInstance;
                }
            }
        }


        //starts the listener
        public static void startListener() {
            TcpListenerInstance.Start();
        }

        public static void setParams(string ip, string port) {
            TcpIp = IPAddress.Parse(ip);
            TcpPort = Int32.Parse(port);
        }


        //sends a request to the server
        public static async void sendRequest(TcpClient requestInfo) {
            NetworkStream stream = requestInfo.GetStream();
            if (currentURI == null) {
                currentURI = getURI(requestInfo, stream);
            }
            //TODO, replace with a method that gets the url from the request

            Console.WriteLine(currentURI);
            String currentRequest = currentURI;
            //if the request is not a valid ur, prefix it with the target url
            if (!currentRequest.StartsWith("http")) {
                currentRequest = currentURI + '/' + currentRequest;
            }

            HttpResponseMessage responseString = await HttpClientInstance.GetAsync(currentRequest);
            //echo the result to the console
            Console.WriteLine(responseString);

            //write the http response to the console
            byte[] buffer = await responseString.Content.ReadAsByteArrayAsync();

            stream.Write(buffer, 0, buffer.Length);
            stream.Close();
            
            

            //requestInfo.Response.OutputStream.Write(buffer, 0, buffer.Length);
            //requestInfo.Response.OutputStream.Close();

        }
        public static string getURI(TcpClient requestInfo, NetworkStream stream) {

            Byte[] bytes = new Byte[256];
            string data = null;

            // Get a stream object for reading and writing
            

            int i;

            // Loop to receive all the data sent by the client.
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0) {
                Console.WriteLine("Loop!");
                // Translate data bytes to a ASCII string.
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                
                Console.WriteLine("Received: {0}", data);
                break;
            }

            string getLine = data.Split('\n')[0];
            Console.WriteLine("GetLine: " + getLine);
            return getLine.Split(' ')[1].Trim('/');
        }
    }
}
