using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Responsible for LogFile Management
/// </summary>
public static class log
{
    /// <summary>
    /// Helper operation logs the current user access event
    /// </summary>
    /// <param name="page">The ASP.NET Page that is asking to
    /// write to the access security log.</param>
    public static void WritePageAccessEvent(Page page)
    {
        DataSet log = new DataSet("SecurityLog");
        log.ReadXml(page.Request.MapPath("App_Data/log.xml"));
        if (null == log.Tables["AccessEvent"])
        {
            log.Tables.Add("AccessEvent");
            string[] cols = { "UserName", "Role", "AccessTime", "PageViewed" };
            foreach (string col in cols)
                log.Tables["AccessEvent"].Columns.Add(col);
        }
        log.Tables["AccessEvent"].Rows.Add(new object[]
        {
            page.User.Identity.Name, 
            Roles.IsUserInRole("Admin") ? "Admin" : "User",
            DateTime.Now,
            page.Request.RawUrl
        });
        log.WriteXml(page.Request.MapPath("App_Data/log.xml"));
    }
    /// <summary>
    /// Helper operation clears the content of the security log.
    /// </summary>
    /// <param name="page">The ASP.NET Page that is asking to
    /// write to the access security log.</param>
    public static void ClearLog(Page page)
    {
        DataSet log = new DataSet("SecurityLog");
        log.ReadXml(page.Request.MapPath("App_Data/log.xml"));
        log.Clear();
        log.WriteXml(page.Request.MapPath("App_Data/log.xml"));
    }
}