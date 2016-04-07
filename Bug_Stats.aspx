<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Code/Bug_Stats.aspx.cs" Inherits="Bug_Stats" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="_mainContent" runat="Server">
    <asp:Label ID="Label1" runat="server" Text="1) Select Opened Date in bug stats to filter by Opened Bugs:" />
    <asp:Calendar ID="InputCalendar" runat="server" OnSelectionChanged="InputCalendar_SelectionChanged" />
    <br />
    <asp:Label ID="Label2" runat="server" Text="2) Input alias of the person to filter what Bugs are assigned to them:" /><br />
    <asp:TextBox ID="TextBox1" runat="server" />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Apply Filter(s)"
        Width="165px" />&nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click"
            Text="Reset Filter(s)" Width="144px" /><br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
        Caption="Current Bug Stats View:" EnableSortingAndPagingCallbacks="True" CaptionAlign="Left"
        BackColor="#DEBA84" BorderColor="Brown" Font-Bold="False" BorderStyle="Solid" BorderWidth="5px"
        Font-Overline="False"
        Font-Strikeout="False" CellSpacing="3" ForeColor="#333333" CellPadding="0" 
        PagerSettings-PageButtonCount="15" 
        PageSize="15" >
        <SelectedRowStyle BackColor="#FF8000" />
        <AlternatingRowStyle BackColor="WhiteSmoke" BorderColor="Black" BorderStyle="Solid" />
        <PagerSettings Mode="NextPreviousFirstLast" NextPageText="Next Page &gt;&gt;" Position="TopAndBottom"
            PreviousPageText=" Preivious Page &lt;&lt;" PageButtonCount="15" FirstPageText="First" LastPageText="Last" />
    </asp:GridView>
</asp:Content>
