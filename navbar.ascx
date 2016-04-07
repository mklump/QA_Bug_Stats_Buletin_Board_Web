<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Code/navbar.ascx.cs" Inherits="navbar" %>
<asp:Label runat="server" ID="Label1" Width="200px" Height="25px"
    BackColor="#FFFBD6" ForeColor="#990000" Font-Bold="True" Font-Size="Large" >
    <b style="font-size:larger">Quick Launch</b>
</asp:Label>
<asp:Menu ID="Menu1" runat="server" BackColor="#FFFBD6" DynamicHorizontalOffset="2"
    Font-Names="Verdana" Font-Size="Medium" ForeColor="#990000" Height="375px" StaticSubMenuIndent="10px"
    Width="165px">
    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
    <DynamicMenuStyle BackColor="#FFFBD6" />
    <StaticSelectedStyle BackColor="#FFCC66" />
    <DynamicSelectedStyle BackColor="#FFCC66" />
    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <Items>
        <asp:MenuItem Text="Home" Value="Home" NavigateUrl="~/Default.aspx" ToolTip="http://.../sidenav/Default.aspx">
        </asp:MenuItem>
        <asp:MenuItem Text="Test Passes" Value="Test Passes" NavigateUrl="~/Test_Passes.aspx"
            ToolTip="http://.../sidenav/Test_Passes.aspx"></asp:MenuItem>
        <asp:MenuItem Text="Performance" Value="Performance" NavigateUrl="~/Performance.aspx"
            ToolTip="http://.../sidenav/Performance.aspx"></asp:MenuItem>
        <asp:MenuItem Text="Stress" Value="Stress" NavigateUrl="~/Stress.aspx" ToolTip="http://.../sidenav/Stress.aspx">
        </asp:MenuItem>
        <asp:MenuItem Text="MTTF" Value="MTTF" NavigateUrl="~/MTTF.aspx" ToolTip="http://.../sidenav/MTTF.aspx">
        </asp:MenuItem>
        <asp:MenuItem Text="Code Coverage" Value="Code Coverage" NavigateUrl="~/Code_Coverage.aspx"
            ToolTip="http://.../sidenav/Code_Coverage.aspx"></asp:MenuItem>
        <asp:MenuItem Text="Bug Stats" Value="Bug Stats" NavigateUrl="~/Bug_Stats.aspx" ToolTip="http://.../sidenav/Bug_Stats.aspx">
        </asp:MenuItem>
        <asp:MenuItem Text="Dogfood Stats" Value="Dogfood Stats" NavigateUrl="~/Dogfood_Stats.aspx"
            ToolTip="http://.../sidenav/Dogfood_Stats.aspx"></asp:MenuItem>
        <asp:MenuItem Text="App Verifier" Value="App Verifier" NavigateUrl="~/App_Verifier.aspx"
            ToolTip="http://.../sidenav/App_Verifier.aspx"></asp:MenuItem>
        <asp:MenuItem Text="Build Verification Tests" Value="Build Verification Tests" NavigateUrl="~/Build_Verification_Tests.aspx"
            ToolTip="http://.../sidenav/Build_Verification_Tests.aspx"></asp:MenuItem>
    </Items>
    <StaticHoverStyle BackColor="#990000" ForeColor="White" />
</asp:Menu>
