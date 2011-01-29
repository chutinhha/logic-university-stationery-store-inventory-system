<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="SA33.Team12.SSIS.UserAdministration.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>User List</h2>

<p class="failureNotification">
    <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
</p>

<fieldset>
<legend>User List</legend>
<asp:GridView runat="server"
    ID="UserGridView" AutoGenerateColumns="False" 
        DataSourceID="UserObjectDataSource" DataKeyNames="UserID" 
        onrowcommand="UserGridView_RowCommand" AllowPaging="True"
        SelectedRowStyle-BackColor="Silver" 
        onselectedindexchanged="UserGridView_SelectedIndexChanged">
    <Columns>
        <asp:BoundField DataField="UserName" HeaderText="UserName" 
            SortExpression="UserName" ReadOnly="True" />
        <asp:BoundField DataField="FirstName" HeaderText="FirstName" 
            SortExpression="UserName" ReadOnly="True" />
        <asp:BoundField DataField="LastName" HeaderText="LastName" 
            SortExpression="UserName" ReadOnly="True" />
        <asp:BoundField DataField="Email" HeaderText="Email" 
            SortExpression="Email" />
        <asp:CheckBoxField DataField="IsEnabled" HeaderText="Enabled" ReadOnly="true" />
        <asp:CommandField ShowSelectButton="True" />
        <asp:ButtonField ButtonType="Link" 
            CommandName="Disable" 
            Text="Disable" />
    </Columns>
</asp:GridView>

<asp:ObjectDataSource ID="UserObjectDataSource" runat="server" 
    SelectMethod="GetAllUsers" TypeName="SA33.Team12.SSIS.Utilities.Membership" 
        DeleteMethod="DeleteUser" DataObjectTypeName="SA33.Team12.SSIS.DAL.User" 
        InsertMethod="CreateUser" UpdateMethod="UpdateUser">
</asp:ObjectDataSource>

</fieldset>
<fieldset>
<legend>User Detail</legend>
<asp:DetailsView runat="server" ID="UserDetailView" 
        AutoGenerateRows="False" DataSourceID="UserDetailObjectDataSource">
    <EmptyDataTemplate>
        Please select an user to view its detail.
    </EmptyDataTemplate>
    <Fields>
        <asp:BoundField DataField="UserID" HeaderText="UserID" 
            SortExpression="UserID" />
        <asp:BoundField DataField="DepartmentID" HeaderText="DepartmentID" 
            SortExpression="DepartmentID" />
        <asp:TemplateField HeaderText="Department">
            <ItemTemplate>
                <asp:DynamicControl runat="server" ID="DepartmentLabel"
                    DataField="DepartmentID" Mode="ReadOnly" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList runat="server" ID="DepartmentDropdownList"
                    DataSourceID="DepartmentObjectDataSource"
                    DataTextField="Name" DataValueField="DepartmentID">
                </asp:DropDownList>
                <asp:ObjectDataSource 
                    runat="server" 
                    ID="DepartmentObjectDataSource" 
                    DataObjectTypeName="SA33.Team12.SSIS.DAL.Department"
                    SelectMethod="GetAllDepartments" 
                    TypeName="SA33.Team12.SSIS.BLL.UserManager">
                </asp:ObjectDataSource>    
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="UserName" HeaderText="UserName" 
            SortExpression="UserName" />
        <asp:BoundField DataField="FirstName" HeaderText="FirstName" 
            SortExpression="FirstName" />
        <asp:BoundField DataField="LastName" HeaderText="LastName" 
            SortExpression="LastName" />
        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
        <asp:CheckBoxField DataField="IsEnabled" HeaderText="IsEnabled" 
            SortExpression="IsEnabled" />
        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
            ShowInsertButton="True" />
    </Fields>
</asp:DetailsView>

<asp:ObjectDataSource ID="UserDetailObjectDataSource" runat="server" 
        DataObjectTypeName="SA33.Team12.SSIS.DAL.User" DeleteMethod="DeleteUser" 
        InsertMethod="CreateUser" SelectMethod="GetUsersById" 
        TypeName="SA33.Team12.SSIS.Utilities.Membership" UpdateMethod="UpdateUser">
    <SelectParameters>
        <asp:ControlParameter ControlID="UserGridView" Name="userId" 
            PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
</fieldset>

<asp:DynamicDataManager runat="server" ID="DynamicDataManager" >
</asp:DynamicDataManager>

</asp:Content>
