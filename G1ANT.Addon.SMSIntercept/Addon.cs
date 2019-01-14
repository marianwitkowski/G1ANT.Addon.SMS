using G1ANT.Language;

namespace G1ANT.Addon.SMSIntercept
{
    [Addon(Name = "SMSIntercept", Tooltip = "Addon for SMS interception")]
    [Copyright(Author = "Marian Witkowski", Copyright = "(c) 2019 Marian Witkowski", Email = "marian.witkowski@gmail.com")]
    [License(Type = "LGPL", ResourceName = "License.txt")]
    [CommandGroup(Name = "smsintercept", Tooltip = "Module for intercept SMS from cellphone")]
    public class Addon : Language.Addon
    {
            public override void Initialize()
            {
                base.Initialize();
            }
    }
}
