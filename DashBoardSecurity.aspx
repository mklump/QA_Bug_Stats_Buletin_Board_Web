<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Code/DashBoardSecurity.aspx.cs" Inherits="DashBoardSecurity" Trace="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DashBoard Security</title>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr >
                <td style="width: 20px; background-color: brown;" valign="top">
                    <asp:Panel ID="Panel1" runat="server" Height="243px" Width="174px" BackColor="Brown">
                    </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server" Height="180px" GroupingText="Manage Security Log" BackColor="Brown" ForeColor="White">
                        <br />
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh Log" Width="158px" OnClick="btnRefresh_Click" />
                        <br />
                        <asp:Button ID="btnSaveLog" runat="server" Text="Save Log" Width="158px" OnClick="btnSaveLog_Click" />
                        <br />
                        <asp:Button ID="btnClearLog" runat="server" Text="Clear Log" Width="158px" OnClick="btnClearLog_Click" />
                        <br />
                    </asp:Panel>
                </td>
                <td style="width: 750px; ">
                    <table style="width: 750px;">
                        <tr>
                            <td style="height:100px; color: white; background-color:brown;" align="left">
                                <asp:LoginView ID="LoginView1" runat="server">
                                    <AnonymousTemplate>
                                        Alias not authenticated. You are not authorized to view this page.
                                    </AnonymousTemplate>
                                    <RoleGroups>
                                        <asp:RoleGroup Roles="Admin">
                                            <ContentTemplate>
                                                <h3>
                                                    Authentication:</h3>
                                                <h2>
                                                    Administrator</h2>
                                            </ContentTemplate>
                                        </asp:RoleGroup>
                                        <asp:RoleGroup Roles="User">
                                            <ContentTemplate>
                                                <h3>
                                                    Authentication:</h3>
                                                <h2>
                                                    User<h2>
                                            </ContentTemplate>
                                        </asp:RoleGroup>
                                    </RoleGroups>
                                </asp:LoginView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="False"
                                    BackColor="#DEBA84" BorderColor="Brown" BorderStyle="Solid" BorderWidth="5px"
                                    Caption="Current Authenticated User Selection:" CaptionAlign="Left" CellPadding="0" CellSpacing="3"
                                    EnableSortingAndPagingCallbacks="False" Font-Bold="False" Font-Overline="False"
                                    Font-Strikeout="False" ForeColor="#333333" PagerSettings-PageButtonCount="5"
                                    PageSize="5" AutoGenerateSelectButton="True" SelectedIndex="-1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" >
                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast"
                                        NextPageText="Next Page &gt;&gt;" PageButtonCount="5" Position="TopAndBottom"
                                        PreviousPageText=" Preivious Page &lt;&lt;" />
                                    <SelectedRowStyle BackColor="#FF8000" />
                                    <AlternatingRowStyle BackColor="WhiteSmoke" BorderColor="Black" BorderStyle="Solid" />
                                </asp:GridView>
                                <asp:Button ID="Button1" runat="server" Text="Show All Activity" Width="205px" OnClick="Button1_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="False"
                                    BackColor="#DEBA84" BorderColor="Brown" BorderStyle="Solid" BorderWidth="5px"
                                    Caption="User Details and Activity Status:" CaptionAlign="Left" CellPadding="0" CellSpacing="3"
                                    EnableSortingAndPagingCallbacks="True" Font-Bold="False" Font-Overline="False"
                                    Font-Strikeout="False" ForeColor="#333333" PagerSettings-PageButtonCount="25"
                                    PageSize="25" >
                                    <SelectedRowStyle BackColor="#FF8000" />
                                    <AlternatingRowStyle BackColor="WhiteSmoke" BorderColor="Black" BorderStyle="Solid" />
                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast"
                                        NextPageText="Next Page &gt;&gt;" PageButtonCount="25" Position="TopAndBottom"
                                        PreviousPageText=" Preivious Page &lt;&lt;" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
