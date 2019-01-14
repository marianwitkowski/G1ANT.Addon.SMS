using System;
using System.Threading;
using G1ANT.Language;

namespace G1ANT.Addon.SMSIntercept
{
    [Command(Name = "smsintercept.readsms", Tooltip = "Read SMS")]
    public class ReadSMSCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = false, Tooltip = "Wait for SMS timeout")]
            public IntegerStructure waitTimeout { get; set; } = new IntegerStructure(60);

            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");
        }

        public ReadSMSCommand(AbstractScripter scripter) : base(scripter)
        {
        }


        public void Execute(Arguments arguments)
        {
            try
            {
                SMSInterceptSettings.GetInstance().lastReadSMS = null;
                int timeout = arguments.waitTimeout.Value;
                while (timeout>0)
                {
                    Thread.Sleep(1000);
                    String result = SMSInterceptSettings.GetInstance().lastReadSMS;
                    if (result!=null)
                    {
                        Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(result));
                        return;
                    }
                    timeout--;
                }
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(String.Empty));
            }
            catch (Exception exc)
            {
                throw new ApplicationException(exc.Message);
            }
        }
    }
}
