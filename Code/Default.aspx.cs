using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Security.Principal;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Roles.RoleExists("User"))
                Roles.CreateRole("User");
            else if (!Roles.RoleExists("Admin"))
                Roles.CreateRole("Admin");
            log.WritePageAccessEvent(this);
            CheckDBUserAuthorization();
            CheckUserRole();
        }
    }
    /// <summary>
    /// Helper operations add the current logged on identidy as an
    /// application user in the appropriate role and starts impersonation
    /// for them if not already done so.
    /// </summary>
    private void CheckUserRole()
    {
        // Everyone that access the website on corpnet automatically
        // authenticates as a User. Administrators done else where.
        if(!Roles.IsUserInRole("User"))
            Roles.AddUserToRole(User.Identity.Name, "User");
        IIdentity WinId = HttpContext.Current.User.Identity;
        WindowsIdentity wi = (WindowsIdentity)WinId;
        WindowsImpersonationContext wic = wi.Impersonate();
    }
    /// <summary>
    /// Helper operation checks the Authorization Credentials of the
    /// current user to access the application's data source.
    /// </summary>
    private void CheckDBUserAuthorization()
    {
        IIdentity WinId = HttpContext.Current.User.Identity;
        WindowsIdentity wi = (WindowsIdentity)WinId;
        WindowsImpersonationContext wic = null;
        try
        {
            wic = wi.Impersonate();
            // Code to access network resources goes here.
            SqlCommand command = new SqlCommand(
                "dbo.[QADashboard_GetBugsByStatusDateAndAssignee]",
                new SqlConnection(ConfigurationManager.ConnectionStrings
                ["QADashboardConnectionString"].ConnectionString));
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter[] sqlParams =
            {
                new SqlParameter("@Status", "Active"),
                new SqlParameter("@AssignedTo", null ),
                new SqlParameter("@OpenedDate", null )
            };
            foreach (SqlParameter sqlParam in sqlParams)
            {
                sqlParam.Direction = ParameterDirection.Input;
                command.Parameters.Add(sqlParam);
            }
            SqlDataAdapter adp = new SqlDataAdapter(command);
            DataSet ds = new DataSet("AccountTest");
            adp.Fill(ds);
        }
        catch (Exception Error)
        {
            // Ensure that an exception is not propagated higher in the call stack.
            Response.Write("Your alias " + wi.Name + " is not authorized to use this application.<br />" +
                "Please see the website Administrator. Details:<br /><br />" + Error.ToString());
        }
        finally
        {
            // Make sure to remove the impersonation token
            if (wic != null)
                wic.Undo();
        }
    }
}
