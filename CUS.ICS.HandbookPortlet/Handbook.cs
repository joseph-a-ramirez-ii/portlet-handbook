using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jenzabar.Portal.Framework.Security.Authorization;

namespace CUS.ICS.Handbook
{
    [PortletOperation(
    "CanAccess",
    "Can Access Portlet",
    "Whether a user can access this portlet or not",
    PortletOperationScope.Global)]

    [PortletOperation(
        "CanAdmin",
        "Can Admin Portlet",
        "Whether a user can admin this portlet or not",
        PortletOperationScope.Global)]

    public class Handbook : Jenzabar.Portal.Framework.Web.UI.SecuredPortletBase
    {
        protected override Jenzabar.Portal.Framework.Web.UI.PortletViewBase GetCurrentScreen()
        {
            Jenzabar.Portal.Framework.Web.UI.PortletViewBase screen = null;

            switch (this.CurrentPortletScreenName)
            {
                case "Admin":
                    screen = this.LoadPortletView("ICS/HandbookPortlet/Admin_View.ascx");
                    break;
                case "Default":
                default:
                    screen = this.LoadPortletView("ICS/HandbookPortlet/Default_View.ascx");
                    break;
            }

            return screen;
        }
    }
}
