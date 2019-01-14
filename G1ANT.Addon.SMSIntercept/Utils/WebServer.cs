using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.SMSIntercept.Utils
{
    public class WebServer
    {
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<HttpListenerRequest, string> _responderMethod;
        private Func<HttpListenerRequest, string, string> sendResponse;
        private string v;

        public WebServer(IReadOnlyCollection<string> prefixes, Func<HttpListenerRequest, string> method)
        {
            if (!HttpListener.IsSupported)
            {
                throw new NotSupportedException("Needs Windows XP SP2, Server 2003 or later.");
            }

            if (prefixes == null || prefixes.Count == 0)
            {
                throw new ArgumentException("URI prefixes are required");
            }

            foreach (var s in prefixes)
            {
                _listener.Prefixes.Add(s);
            }

            _responderMethod = method ?? throw new ArgumentException("responder method required");
            _listener.Start();
        }


        public WebServer(Func<HttpListenerRequest, string> method, params string[] prefixes)
           : this(prefixes, method)
        {
        }

        public void Run()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(o =>
            {
                Console.WriteLine("Webserver running...");
                try
                {
                    while (_listener.IsListening)
                    {
                        System.Threading.ThreadPool.QueueUserWorkItem(c =>
                        {
                            var ctx = c as HttpListenerContext;
                            try
                            {
                                if (ctx == null)
                                {
                                    return;
                                }

                                String rstr = null;
                                try
                                {
                                    String sender = ctx.Request.QueryString["sender"].ToString();
                                    String msg = ctx.Request.QueryString["msg"].ToString();
                                    String ts = ctx.Request.QueryString["ts"].ToString();
                                    rstr = "OK";

                                    SMSInterceptSettings.GetInstance().lastReadSMS = String.Format("{0}\t{1}\t{2}", sender, msg, ts);

                                } catch (Exception exc)
                                {
                                    if (exc.Message != null)
                                    {
                                        rstr = exc.Message;
                                    } else {
                                        rstr = "ERROR";
                                    }
                                }
                                var buf = Encoding.UTF8.GetBytes(rstr);
                                ctx.Response.AddHeader("Content-Type", "text/plain; charset=utf-8");
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            }
                            catch
                            {
                                // ignored
                            }
                            finally
                            {
                                // always close the stream
                                if (ctx != null)
                                {
                                    ctx.Response.OutputStream.Close();
                                }
                            }
                        }, _listener.GetContext());
                    }
                }
                catch (Exception ex)
                {
                    // ignored
                }
            });
        }

        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}
