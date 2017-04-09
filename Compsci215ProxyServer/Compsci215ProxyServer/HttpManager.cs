using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Compsci215ProxyServer {
    // A public static class that manages the Http instance
    
    static class HttpManager {

        private static HttpListener HttpInstance;
        private static HttpClient ClientInstance;
       
        //Creates a new instance of the listener if there isnt one already, controls it so there can be only one at a time
        public static HttpListener HttpListenerInstance {
            get {
                if (HttpInstance == null) {
                    HttpInstance = new HttpListener();
                    return HttpInstance;
                } else {
                    return HttpInstance;
                }
            }
        }

        //creates a new instance of an HttpClient if one does not exist
        public static HttpClient HttpClientInstance {
            get {
                if(ClientInstance == null) {
                    ClientInstance = new HttpClient();
                    return ClientInstance;
                }else {
                    return ClientInstance;
                }
            }

        }

        //adds the set prefix to the set of URI prefixes
        public static void addPrefix(string prefix) {
            HttpListenerInstance.Prefixes.Add(prefix);
        }

        //starts the listener
        public static void startListener() {
            HttpListenerInstance.Start();
        }

        //sends a request to the server
        public static async void sendRequest(HttpListenerContext requestInfo) {
            Console.WriteLine(requestInfo.Request.RawUrl.Trim('/'));
            HttpResponseMessage responseString = await HttpClientInstance.GetAsync(requestInfo.Request.RawUrl.Trim('/'));
            Console.WriteLine(responseString);

            byte[] buffer = await responseString.Content.ReadAsByteArrayAsync();

            requestInfo.Response.OutputStream.Write(buffer, 0, buffer.Length);

        }


    }
}
