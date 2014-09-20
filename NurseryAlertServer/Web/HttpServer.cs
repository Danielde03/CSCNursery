using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Web;
using NurseryAlertServer.Properties;

namespace NurseryAlertServer.Web
{

    public class HttpProcessor {
        public TcpClient socket;        
        public HttpServer srv;

        private Stream inputStream;
        public StreamWriter outputStream;

        public String http_method;
        public String http_url;
        public String http_protocol_versionstring;
        public Hashtable httpHeaders = new Hashtable();


        private static int MAX_POST_SIZE = 10 * 1024 * 1024; // 10MB

        public HttpProcessor(TcpClient s, HttpServer srv) {
            this.socket = s;
            this.srv = srv;                   
        }
        

        private string streamReadLine(Stream inputStream) {
            int next_char;
            string data = "";
            int timeout = 0;
            while (true) {
                next_char = inputStream.ReadByte();
                if (next_char == '\n') { break; }
                if (next_char == '\r') { continue; }
                if (next_char == -1) {
                    timeout++;
                    if (timeout > 3)
                        break;
                    Thread.Sleep(1); 
                    continue;
                };
                data += Convert.ToChar(next_char);
            }
            return data;
        }
        public void process() {                        
            // we can't use a StreamReader for input, because it buffers up extra data on us inside it's
            // "processed" view of the world, and we want the data raw after the headers
            inputStream = new BufferedStream(socket.GetStream());

            // we probably shouldn't be using a streamwriter for all output from handlers either
            outputStream = new StreamWriter(new BufferedStream(socket.GetStream()));
            try {
                parseRequest();
                readHeaders();
                if (http_method.Equals("GET")) {
                    handleGETRequest();
                } else if (http_method.Equals("POST")) {
                    handlePOSTRequest();
                }
            } catch (Exception e) {
                Console.WriteLine("Exception: " + e.ToString());
                writeFailure();
            }
            outputStream.Flush();
            // bs.Flush(); // flush any remaining output
            inputStream = null; outputStream = null; // bs = null;            
            socket.Close();
        }

        public void parseRequest() {
            String request = streamReadLine(inputStream);
            string[] tokens = request.Split(' ');
            if (tokens.Length != 3) {
                throw new Exception("invalid http request line");
            }
            http_method = tokens[0].ToUpper();
            http_url = tokens[1];
            http_protocol_versionstring = tokens[2];

            Console.WriteLine("starting: " + request);
        }

        public void readHeaders() {
            Console.WriteLine("readHeaders()");
            String line;
            while ((line = streamReadLine(inputStream)) != null) {
                if (line.Equals("")) {
                    Console.WriteLine("got headers");
                    return;
                }
                
                int separator = line.IndexOf(':');
                if (separator == -1) {
                    throw new Exception("invalid http header line: " + line);
                }
                String name = line.Substring(0, separator);
                int pos = separator + 1;
                while ((pos < line.Length) && (line[pos] == ' ')) {
                    pos++; // strip any spaces
                }
                    
                string value = line.Substring(pos, line.Length - pos);
                Console.WriteLine("header: {0}:{1}",name,value);
                httpHeaders[name] = value;
            }
        }

        public void handleGETRequest() {
            srv.handleGETRequest(this);
        }

        private const int BUF_SIZE = 4096;
        public void handlePOSTRequest() {
            // this post data processing just reads everything into a memory stream.
            // this is fine for smallish things, but for large stuff we should really
            // hand an input stream to the request processor. However, the input stream 
            // we hand him needs to let him see the "end of the stream" at this content 
            // length, because otherwise he won't know when he's seen it all! 

            Console.WriteLine("get post data start");
            int content_len = 0;
            MemoryStream ms = new MemoryStream();
            if (this.httpHeaders.ContainsKey("Content-Length")) {
                 content_len = Convert.ToInt32(this.httpHeaders["Content-Length"]);
                 if (content_len > MAX_POST_SIZE) {
                     throw new Exception(
                         String.Format("POST Content-Length({0}) too big for this simple server",
                           content_len));
                 }
                 byte[] buf = new byte[BUF_SIZE];              
                 int to_read = content_len;
                 while (to_read > 0) {  
                     Console.WriteLine("starting Read, to_read={0}",to_read);

                     int numread = this.inputStream.Read(buf, 0, Math.Min(BUF_SIZE, to_read));
                     Console.WriteLine("read finished, numread={0}", numread);
                     if (numread == 0) {
                         if (to_read == 0) {
                             break;
                         } else {
                             throw new Exception("client disconnected during post");
                         }
                     }
                     to_read -= numread;
                     ms.Write(buf, 0, numread);
                 }
                 ms.Seek(0, SeekOrigin.Begin);
            }
            Console.WriteLine("get post data end");
            srv.handlePOSTRequest(this, new StreamReader(ms));

        }

        public void writeSuccess(string content_type="text/html") {
            outputStream.WriteLine("HTTP/1.0 200 OK");            
            outputStream.WriteLine("Content-Type: " + content_type);
            outputStream.WriteLine("Connection: close");
            outputStream.WriteLine("");
        }

        public void writeFailure() {
            outputStream.WriteLine("HTTP/1.0 404 File not found");
            outputStream.WriteLine("Connection: close");
            outputStream.WriteLine("");
        }
    }

    public abstract class HttpServer {

        protected int port;
        TcpListener listener;
        bool is_active = true;
       
        public HttpServer(int port) {
            this.port = port;
        }

        public void listen() {
            listener = new TcpListener(port);
            listener.Start();
            while (is_active) {
                TcpClient s = null;
                try
                {
                    s = listener.AcceptTcpClient();
                }
                catch (SocketException)
                {
                    //Connection has been interrupted
                    continue;
                }
                HttpProcessor processor = new HttpProcessor(s, this);
                Thread thread = new Thread(new ThreadStart(processor.process));
                thread.Start();
                Thread.Sleep(1);
            }
        }

        public void Stop() {
            is_active = false;
            listener.Stop();
        }

        public abstract void handleGETRequest(HttpProcessor p);
        public abstract void handlePOSTRequest(HttpProcessor p, StreamReader inputData);
    }

    public class NurseryAlertHttpServer : HttpServer
    {
        public NurseryAlertHttpServer(int port)
            : base(port)
        {
        }

        public override void handleGETRequest (HttpProcessor p)
		{
            Console.WriteLine("request: {0}", p.http_url);
            if (String.Equals(p.http_url, "/favicon.ico"))
            {
                Console.WriteLine("fav");
                p.writeFailure();
                return;
            }
            else
            {
                p.writeSuccess();
                writePage(p);
            }
        }

        public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
        {
            Console.WriteLine("POST request: {0}", p.http_url);

            String data = inputData.ReadToEnd();
            NameValueCollection pparams = HttpUtility.ParseQueryString(data);
            String text = pparams["pagernum"];
            bool emerg = true;
            if (String.IsNullOrEmpty(pparams["emergency"]))
                emerg = false;
            PagerList.Instance.AddEntry(text, emerg);

            p.writeSuccess();
            writePage(p);
        }

        private void writePage(HttpProcessor p)
        {
            p.outputStream.WriteLine("<html><head><meta http-equiv=\"refresh\" content=\"60\"></head><body>");
            p.outputStream.WriteLine("<h1>Pager System</h1>");

            p.outputStream.WriteLine("<form method=post action=/>");
            p.outputStream.WriteLine("Pager Number<input type=text name=pagernum>");
            p.outputStream.WriteLine("<input type=submit value=Page>");
            p.outputStream.WriteLine("<br>Emergency<input type=checkbox name=emergency>");
            p.outputStream.WriteLine("</form>");

            p.outputStream.WriteLine("<h2>Current Page List</h2>");
            p.outputStream.WriteLine("<table border=\"1\">");
            p.outputStream.WriteLine("<tr><th>Pager Number</th><th>Emergency</th><th>Outstanding</th><th>Requested</th><th>Last Displayed</th></tr>");
            writePagerList(p);
            p.outputStream.WriteLine("</table>");
            p.outputStream.WriteLine("<form method=get action=/><input type=submit value=Refresh></form>");

            p.outputStream.WriteLine("</body></html>");
        }

        private void writePagerList(HttpProcessor p)
        {
            List<PagerList.PagerEntry> plist = new List<PagerList.PagerEntry>();
            String text = "";
            PagerList.Instance.GetState(ref plist, ref text);
            foreach (var entry in plist)
            {
                p.outputStream.WriteLine("<tr><td>");
                p.outputStream.WriteLine(entry.pagerText);
                p.outputStream.WriteLine("</td><td>");
                p.outputStream.WriteLine(entry.emergency);
                p.outputStream.WriteLine("</td><td>");
                p.outputStream.WriteLine(entry.outstanding);
                p.outputStream.WriteLine("</td><td>");
                p.outputStream.WriteLine(entry.time);
                p.outputStream.WriteLine("</td><td>");
                p.outputStream.WriteLine(entry.displayTime);
                p.outputStream.WriteLine("</td></tr>");
            }
        }
    }

    public class HttpManager
    {
        /// <summary>
        /// Singleton variable
        /// </summary>
        private static HttpManager _instance;

        /// <summary>
        /// Gets the singleton of this class (field alternative)
        /// </summary>
        public static HttpManager Instance
        {
            get { return _instance ?? (_instance = new HttpManager()); }
        }

        private NurseryAlertHttpServer _httpServer;
        private int _port;

        /// <summary>
        /// The constructor
        /// </summary>
        private HttpManager()
        {
            Init();
        }

        /// <summary>
        /// Initialize the Web Server
        /// </summary>
        private void Init()
        {
            _port = Settings.Default.WebPort;
            _httpServer = new NurseryAlertHttpServer(_port);
        }

        /// <summary>
        /// Start the Web Server
        /// </summary>
        public void StartServer()
        {
            Thread thread = new Thread(new ThreadStart(_httpServer.listen));
            thread.Start();
        }

        /// <summary>
        /// Stop the Web Server
        /// </summary>
        public void StopServer()
        {
            _httpServer.Stop();
        }

        /// <summary>
        /// Restart the Web Server if the requested port has changed
        /// </summary>
        public void PortChange()
        {
            if (_port != Settings.Default.WebPort)
            {
                StopServer();
                _httpServer = null;
                Init();
                StartServer();
            }
        }
    }
}
