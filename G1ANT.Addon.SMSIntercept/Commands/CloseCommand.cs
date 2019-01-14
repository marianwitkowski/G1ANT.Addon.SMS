using System;
using G1ANT.Language;

namespace G1ANT.Addon.SMSIntercept
{
    [Command(Name = "smsintercept.close", Tooltip = "Close module")]
    public class CloseCommand : Command
    {
        public class Arguments : CommandArguments
        {
        }



        public CloseCommand(AbstractScripter scripter) : base(scripter)
        {
        }


        public void Execute(Arguments arguments)
        {
            try
            {
                SMSInterceptSettings.GetInstance().webServer.Stop();
                SMSInterceptSettings.GetInstance().webServer = null;
            }
            catch (Exception exc)
            {
                throw new ApplicationException($"Error occured while stopping server", exc);
            }
        }
    }
}
