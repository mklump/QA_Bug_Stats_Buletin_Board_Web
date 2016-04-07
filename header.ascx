<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Code/header.ascx.cs" Inherits="header" %>
<div id="Header1" class="header" style="background-color: #FFFBD6">
    <asp:Table ID="Table1" runat="server" Height="65px" >
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" Width="195px" BackColor="Brown" ForeColor="White">
                <asp:LoginView ID="LoginView1" runat="server" >
                    <AnonymousTemplate>
                    Alias not authenticated. Please see administrator.
                    </AnonymousTemplate>
                    
                    <RoleGroups>
                        <asp:RoleGroup Roles="Admin">
                            <ContentTemplate>
                                <h3>Authentication:</h3> <h2>Administrator</h2>
                            </ContentTemplate>
                        </asp:RoleGroup>
                        <asp:RoleGroup Roles="User">
                            <ContentTemplate>
                                <h3>Authentication:</h3> <h2>User<h2>
                            </ContentTemplate>
                        </asp:RoleGroup>
                    </RoleGroups>
                    
                </asp:LoginView>
            </asp:TableCell>
            <asp:TableCell runat="server" BackColor="Brown" ForeColor="White">
                <br />
                This space reserved for the header and/or MS Branding Logo if needed.
                <br />
                <br />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<!-- <LoggedInTemplate>
    You are authenticated as: "<asp:LoginName ID="LoginName1" runat="server" />"
</LoggedInTemplate> -->