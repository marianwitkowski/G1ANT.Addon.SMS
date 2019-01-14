using System;
using System.Net;
using G1ANT.Addon.SMSIntercept.Utils;
using G1ANT.Language;

namespace G1ANT.Addon.SMSIntercept
{
    [Command(Name = "smsintercept.init", Tooltip = "Init module")]
    public class OpenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "IP address")]
            public TextStructure hostName { get; set; } = new TextStructure("localhost");

            [Argument(Required = true, Tooltip = "Listener tcp port")]
            public IntegerStructure tcpPort { get; set; } = new IntegerStructure(1331);

        }



        public OpenCommand(AbstractScripter scripter) : base(scripter)
        {
        }


        private static string SendResponse(HttpListenerRequest request)
        {
            return "OK";
        }


        public void Execute(Arguments arguments)
        {
            try
            {
                SMSInterceptSettings.GetInstance().webServer = new WebServer(SendResponse, String.Format("http://{0}:{1}/", arguments.hostName, arguments.tcpPort) );
                SMSInterceptSettings.GetInstance().webServer.Run();
            }
            catch (Exception exc)
            {
                throw new ApplicationException($"Error occured while initializing", exc);
            }
        }

 
    }
}
