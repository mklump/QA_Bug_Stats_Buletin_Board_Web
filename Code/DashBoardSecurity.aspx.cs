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

public partial class DashBoardSecurity : System.Web.UI.Page
{
    private static DataSet originalDS, filteredDS;
    private static DataTable table;
    private static string[] source;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            log.WritePageAccessEvent(this);
            //if (!Roles.IsUserInRole(User.Identity.Name, "Admin"))
            //    Response.Redirect("~/Default.aspx");
            originalDS = new DataSet("original");
            PopulateGridView1();
        }
        Button1_Click(this, e);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1_SelectedIndexChanged(sender, e);
        GridView1.SelectedIndex = -1;
    }
    /// <summary>
    /// Helper operation that populates the GridView1 control.
    /// </summary>
    private void PopulateGridView1()
    {
        filteredDS = new DataSet("UserList");
        originalDS.ReadXml(Request.MapPath("App_Data/log.xml"));
        filteredDS.ReadXml(Request.MapPath("App_Data/log.xml"));
        string[] users = Roles.GetUsersInRole("User"),
            admin = Roles.GetUsersInRole("Admin");
        source = new string[users.Length + admin.Length];
        table = filteredDS.Tables["AccessEvent"];
        int index = 0;
        for (int i = 0; i < users.Length; ++i )
        {
            string x = users[i], temp = "";
            foreach (DataRow row in table.Rows) // Walk through User Group list and find find first instance of current user in Security Log of the format DOMAIN\\user
            {
                foreach (string str in row.ItemArray)
                    if (str.EndsWith(x.Split('@')[0]) && x != "")
                    {
                        x = temp = str;
                        break;
                    }
                if (x == temp)
                    break;
            }
            int num = table.Select("UserName = '" + x + "'").Length;
            source[index++] += x + " in Role Group: Users having " + num + " page access events";
        }
        for (int i = 0; i < admin.Length; ++i )
        {
            string y = users[i], temp = "";
            foreach (DataRow row in table.Rows) // Walk through User Group list and find find first instance of current user in Security Log of the format DOMAIN\\user
            {
                foreach (string str in row.ItemArray)
                    if (str.EndsWith(y.Split('@')[0]) && y != "")
                    {
                        y = temp = str;
                        break;
                    }
                if (y == temp)
                    break;
            }
            int num = table.Select("UserName = '" + y + "'").Length;
            source[index++] += y + " in Role Group: Admin having " + num + " page access events";
        }
        Cache["UserSelectionList"] = source;
        Cache["table"] = table;
        GridView1.DataSource = source;
        GridView1.DataBind();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (-1 != GridView1.SelectedIndex)
        {
            string[] list = source;
            string valSelect = list[GridView1.SelectedIndex], y = "", temp = " ";
            DataView view = new DataView(table);
            foreach (DataRow row in view.Table.Rows) // Walk through User Group list and find find first instance of current user in Security Log of the format DOMAIN\\user
            {
                foreach (string str in row.ItemArray)
                {
                    if (-1 == valSelect.IndexOf('@') && -1 != valSelect.IndexOf('\\'))
                        valSelect = valSelect.Split(' ')[0].Split('\\')[1];
                    else
                        valSelect = valSelect.Split(' ')[0].Split('@')[0];
                    if (str.EndsWith(valSelect) || str.StartsWith(valSelect))
                    {
                        y = temp = str;
                        break;
                    }
                    if (y == temp)
                        break;
                }
            }
            view.RowFilter = "UserName = '" + y + "'";
            view.Sort = "UserName DESC";
            GridView2.DataSource = view;
            filteredDS.Tables.Remove("AccessEvent");
            filteredDS.Tables.Add(view.ToTable());
        }
        else
        {
            GridView2.DataSource = originalDS;
        }
        GridView2.DataBind();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Button1_Click(sender, e);
    }
    protected void btnSaveLog_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("App_Data/log.xml");
        System.IO.FileInfo targetFile = new System.IO.FileInfo(filePath);
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name);
        Response.AddHeader("Content-Length", targetFile.Length.ToString());
        Response.ContentType = "application/octet-stream";
        Response.WriteFile(targetFile.FullName);
    }
    protected void btnClearLog_Click(object sender, EventArgs e)
    {
        log.ClearLog(this);
        originalDS.Clear();
        filteredDS.Clear();
    }
}
