<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="SA33.Team12.SSIS.UserAdministration.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>User List</h2>

<p class="failureNotification">
    <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
</p>


<asp:GridView runat="server"
    ID="UserGridView" AutoGenerateColumns="False" 
        DataSourceID="UserObjectDataSource" DataKeyNames="UserName" 
        onrowcommand="UserGridView_RowCommand" AllowPaging="True">
    <Columns>
        <asp:BoundField DataField="UserName" HeaderText="UserName" 
            SortExpression="UserName" ReadOnly="True" />
        <asp:BoundField DataField="Email" HeaderText="Email" 
            SortExpression="Email" />
        <asp:CheckBoxField DataField="IsApproved" HeaderText="IsApproved" 
            SortExpression="IsApproved" />
        <asp:CheckBoxField DataField="IsLockedOut" HeaderText="IsLockedOut" 
            ReadOnly="True" SortExpression="IsLockedOut" />
        <asp:BoundField DataField="LastLockoutDate" HeaderText="LastLockoutDate" 
            ReadOnly="True" SortExpression="LastLockoutDate" />
        <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" 
            ReadOnly="True" SortExpression="CreationDate" />
        <asp:BoundField DataField="LastLoginDate" HeaderText="LastLoginDate" 
            SortExpression="LastLoginDate" />
        <asp:BoundField DataField="LastActivityDate" HeaderText="LastActivityDate" 
            SortExpression="LastActivityDate" />
        <asp:BoundField DataField="LastPasswordChangedDate" 
            HeaderText="LastPasswordChangedDate" ReadOnly="True" 
            SortExpression="LastPasswordChangedDate" />
        <asp:CheckBoxField DataField="IsOnline" HeaderText="IsOnline" ReadOnly="True" 
            SortExpression="IsOnline" />
        <asp:CommandField ShowSelectButton="True" />
        <asp:ButtonField ButtonType="Link" 
            CommandName="Disable" 
            Text="Disable" />
    </Columns>
</asp:GridView>

<asp:ObjectDataSource ID="UserObjectDataSource" runat="server" 
    SelectMethod="GetAllUsers" TypeName="System.Web.Security.Membership" 
        DeleteMethod="DeleteUser">
    <DeleteParameters>
        <asp:Parameter Name="username" Type="String" />
    </DeleteParameters>
</asp:ObjectDataSource>



</asp:Content>
