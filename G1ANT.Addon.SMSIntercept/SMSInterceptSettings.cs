using G1ANT.Addon.SMSIntercept.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.SMSIntercept
{
    public sealed class SMSInterceptSettings
    {

        public String lastReadSMS { get; set; }
        public WebServer webServer { get; set; }

        private SMSInterceptSettings()
        {
            lastReadSMS = null;
            webServer = null;
        }

        private static readonly SMSInterceptSettings instance = null;
        static SMSInterceptSettings()
        {
            instance = new SMSInterceptSettings();
        }

        public static SMSInterceptSettings GetInstance()
        {
            return instance;
        }

    }
}
