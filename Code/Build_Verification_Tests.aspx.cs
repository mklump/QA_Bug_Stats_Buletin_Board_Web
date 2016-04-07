using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Build_Verification_Tests : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        log.WritePageAccessEvent(this);
    }

    public void Menu1_MenuItemClick(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = int.Parse(((MenuEventArgs)e).Item.Value);
        // Make the selected menu item reflect the correct imageurl.
        switch (MultiView1.ActiveViewIndex)
        {
            case 0:
                Menu1.Items[0].ImageUrl = "~/Images/selectedtabWMU.gif";
                Menu1.Items[1].ImageUrl = "~/Images/unselectedtabYONA.gif";
                break;
            case 1:
                Menu1.Items[0].ImageUrl = "~/Images/unselectedtabWMU.gif";
                Menu1.Items[1].ImageUrl = "~/Images/selectedtabYONA.gif";
                break;
            default:
                Menu1.Items[0].ImageUrl = "~/Images/unselectedtabWMU.gif";
                Menu1.Items[1].ImageUrl = "~/Images/unselectedtabYONA.gif";
                break;
        }
    }
}
