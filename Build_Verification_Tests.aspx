<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Code/Build_Verification_Tests.aspx.cs" Inherits="Build_Verification_Tests"
    Title="Build Verification Tests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="_mainContent" runat="Server">

    <asp:Menu ID="Menu1" Width="168px" runat="server" Orientation="Horizontal"
    StaticEnableDefaultPopOutImage="False" OnMenuItemClick="Menu1_MenuItemClick">
        <Items>
            <asp:MenuItem ImageUrl="~/Images/selectedtabWMU.GIF" Text="" Value="0"></asp:MenuItem>
            <asp:MenuItem ImageUrl="~/Images/unselectedtabYONA.GIF" Text="" Value="1"></asp:MenuItem>
        </Items>
    </asp:Menu>
    
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="tabWMU" runat="server">
            <table style="width:600px; height:400px" cellpadding="0" cellspacing="0" >
                <tr valign="top">
                    <td class="TabArea" style="width: 600px" >
                        <%
                            Response.WriteFile("~/App_Data/WMU_BVT_Summary_xBow_15328_s4_1088.html");
                        %>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="tabYONA" runat="server">
            <table style="width:600px; height:400px" cellpadding="0" cellspacing="0">
                <tr valign="top">
                    <td class="TabArea" style="width: 600px">
                        <br />
                        <br />
                        TAB VIEW 2 INSERT YOUR CONENT IN HERE CHANGE SELECTED IMAGE URL AS NECESSARY
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    
</asp:Content>
