using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class RoleManagement : System.Web.UI.Page
{
    private static ArrayList users;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            log.WritePageAccessEvent(this);
            // Check Security Clearce
            //if (!Roles.IsUserInRole(User.Identity.Name, "Admin"))
            //    Response.Redirect("~/Default.aspx");
            users = new ArrayList(0);
            users.AddRange(Roles.GetUsersInRole("User"));
            string[] temp = Roles.GetUsersInRole("Admin");
            foreach (string str in temp)
                users.Add(str);
        }
        PopulateGridView();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        PopulateGridView();
        return;
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Originally got the contents of the client side selected value as:
        // GridView1.SelectedRow.Cells[1].Text
        string userSelected = (string)users[GridView1.SelectedIndex];
        string[] temp = Roles.GetRolesForUser(userSelected);
        Roles.RemoveUserFromRoles( userSelected, temp );
        while( -1 != users.IndexOf( userSelected ) )
            users.Remove( userSelected );
        PopulateGridView();
        return;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        return;
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        users = (ArrayList)GridView1.DataSource;
        return;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Validate("ValidationGroup1");
        if (!IsValid)
            return;
        try
        {
            string user = TextBox1.Text + "@microsoft.com";
            Roles.AddUserToRole(user, DropDownList1.SelectedValue);
            if( -1 == users.IndexOf(user) )
                users.Add(user);
            TextBox1.Text = "";
        }
        catch (Exception error)
        {
            throw error.InnerException;
        }
        PopulateGridView();
    }
    /// <summary>
    /// Helper operation that populates the web control GridView1
    /// with all the users and their roles.
    /// </summary>
    private void PopulateGridView()
    {
        string[] rolesGroup = null; 
        ArrayList temp = new ArrayList( users );
        for (int x = 0; x < temp.Count; ++x)
        {
            string user = (string)temp[x];
            rolesGroup = Roles.GetRolesForUser(user);
            user += " in Application Role(s): ";
            int y = 0;
            foreach (string role in rolesGroup)
            {
                y = y + 1;
                user += role + (rolesGroup.Length > y ? ", " : "");
            }
            temp[x] = user;
        }
        GridView1.DataSource = temp;
        GridView1.DataBind();
        return;
    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (-1 != TextBox1.Text.IndexOfAny(new char[] { '@', '\\', '.' }) ||
            TextBox1.Text.Length > 8 || "" == TextBox1.Text)
        {
            args.IsValid = false;
            CustomValidator1.ErrorMessage = " Do not use characters '@', '\', '.', and not longer than 8 characters.";
        }
        else if (users.IndexOf(TextBox1.Text + "@microsoft.com") == users.LastIndexOf(TextBox1.Text + "@microsoft.com")
            && -1 != users.IndexOf(TextBox1.Text + "@microsoft.com") )
        {
            args.IsValid = false;
            CustomValidator1.ErrorMessage = "The alias you are trying to add is a duplicate.";
        }
        else
            args.IsValid = true;
    }
    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if ("" == TextBox1.Text)
            args.IsValid = false;
        else
            args.IsValid = true;
    }
    protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if ("" == DropDownList1.SelectedValue)
            args.IsValid = false;
        else
            args.IsValid = true;
    }
}
