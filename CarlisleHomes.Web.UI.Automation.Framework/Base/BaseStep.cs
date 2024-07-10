using CarlisleHomes.Web.UI.Automation.Framework.Config;
using CarlisleHomes.Web.UI.Automation.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarlisleHomes.Web.UI.Automation.Framework.Base
{
    public abstract class BaseStep : Base
    {
        //Page Navigation to the required URL
        public virtual void NavigateSite()
        {
            DriverContext.Browser.GoToUrl(Settings.AUT);
            LogHelper.SerilogWrite("Open the Browser !!!");
        }
    }
}