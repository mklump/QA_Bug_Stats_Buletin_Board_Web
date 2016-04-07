using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Bug_Stats : System.Web.UI.Page
{
    private static DataSet dataset;
    private static DataView view;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            log.WritePageAccessEvent(this);
            GridView1.ToolTip = "Current filtered data view.";
            Cache["gridTip"] = GridView1.ToolTip;
        }
        else
        {
            GridView1.ToolTip = (null == Cache["gridTip"]) ?
                GridView1.ToolTip : (string)Cache["gridTip"];
        }
        Button1_Click(this, e);
        Cache["SqlCon"] = ConfigurationManager.ConnectionStrings["QADashboardConnectionString"];
    }
    protected void InputCalendar_SelectionChanged(object sender, EventArgs e)
    {
        Cache["CalenderDateSelection"] = InputCalendar.SelectedDate;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        InputCalendar.SelectedDate = DateTime.MinValue;
        TextBox1.Text = "";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //adp = new SqlDataAdapter(
        //   "SELECT * FROM [Bugs] WHERE (" +
        //   "datepart( mm, OpenedDate ) = '" + InputCalendar.SelectedDate.Month.ToString() + "' AND " +
        //   "datepart( dd, OpenedDate ) = '" + InputCalendar.SelectedDate.Day.ToString() + "' AND " +
        //   "datepart( yyyy, OpenedDate ) = '" + InputCalendar.SelectedDate.Year.ToString() + "' " +
        //   ")" +
        //   "ORDER BY [OpenedDate] DESC",
        //   Cache.Get("connection").ToString());


        // Apply filter(s)
        SqlCommand command = new SqlCommand(
            "dbo.[QADashboard_GetBugsByStatusDateAndAssignee]",
            new SqlConnection(Cache.Get("SqlCon").ToString()));
        command.CommandType = CommandType.StoredProcedure;
        SqlParameter[] sqlParams =
        {
            new SqlParameter("@Status", "Active"),
            new SqlParameter("@AssignedTo", (TextBox1.Text == "") ? null : TextBox1.Text ),
            new SqlParameter("@OpenedDate", InputCalendar.SelectedDate )
        };
        foreach (
            SqlParameter sqlParam in sqlParams)
        {
            sqlParam.Direction = ParameterDirection.Input;
            command.Parameters.Add(sqlParam);
        }
        if (InputCalendar.SelectedDate == DateTime.MinValue)
            command.Parameters.Remove(sqlParams[2]);
        SqlDataAdapter adp = new SqlDataAdapter(command);
        dataset = new DataSet("BugStatsDS");
        try
        {
            int rowsAffected = adp.Fill(dataset);
            // Filter the results from the last stored proc call to include only the data
            // fields that we want.
            view = GetInterestedColumns(dataset);
            if (0 < rowsAffected && InputCalendar.SelectedDate == DateTime.MinValue
                && "" == TextBox1.Text)
            {
                GridView1.ToolTip = "All active bugs stats selected.";
            }
            GridView1.DataSource = view;
            GridView1.DataBind();
        }
        catch (SqlException error)
        {
            Response.Write("An error occured with the SQL Query:\n\n" + error.ToString());
        }
    }
    private DataView GetInterestedColumns(DataSet source)
    {
        DataTable table = source.Tables["Table"];
        int size = table.Columns.Count;
        for (int i = 0; i < size; ++i)
        {
            if (i >= table.Columns.Count)
                continue;
            else if (table.Columns[i].ColumnName != "BugId" &&
                table.Columns[i].ColumnName != "AssignedTo" &&
                table.Columns[i].ColumnName != "Status" &&
                table.Columns[i].ColumnName != "OpenedDate" &&
                table.Columns[i].ColumnName != "ClosedDate" &&
                table.Columns[i].ColumnName != "Title")
            {
                table.Columns.RemoveAt( i );
                i = i - 1;
            }
        }
        // Apply DataView filter to resort the dataset by OpenedDate
        view = new DataView(table);
        view.RowStateFilter = DataViewRowState.CurrentRows;
        view.Sort = "OpenedDate DESC";
        // Apply changes back to source dataset parameter
        return view;
    }
}