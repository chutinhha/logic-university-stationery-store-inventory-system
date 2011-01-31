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
        SelectedRowStyle-BackColor="Silver">
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

<asp:FormView runat="server" ID="UserFormView" EnableModelValidation="True"
        DataSourceID="UserDetailObjectDataSource" 
        oniteminserting="UserFormView_ItemInserting" 
        onitemupdating="UserFormView_ItemUpdating" 
        onitemcommand="UserFormView_ItemCommand" 
        ondatabound="UserFormView_DataBound">
    <EmptyDataTemplate>
        Please select a user to view its detail.
        <br />
        <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" 
            CommandName="New" Text="New" CssClass="button" />
    </EmptyDataTemplate>
    <EditItemTemplate>
        <table style="width:100%;">
            <tr>
                <td>
        DepartmentID:
                </td>
                <td>
        <asp:DynamicControl ID="DynamicControl1" runat="server" 
            DataField="DepartmentID" Mode="Edit" Visible="false" />

        <asp:DropDownList runat="server" ID="DepartmentDropDownList"
            DataTextField="Name" DataValueField="DepartmentID"
            DataSourceID="DepartmentObjectDataSource" 
            SelectedValue='<%# Eval("DepartmentID") %>'>
        </asp:DropDownList>
        <asp:ObjectDataSource runat="server" ID="DepartmentObjectDataSource"
            DataObjectTypeName="SA33.Team12.SSIS.DAL.User" 
            TypeName="SA33.Team12.SSIS.BLL.UserManager"
            SelectMethod="GetAllDepartments">
        </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td>
        UserName:
                </td>
                <td>
        <asp:DynamicControl ID="UserNameDynamicControl" runat="server" 
            DataField="UserName" Mode="Edit" />
                </td>
            </tr>
            <tr>
                <td>
        FirstName:
                </td>
                <td>
        <asp:DynamicControl ID="FirstNameDynamicControl" runat="server" 
            DataField="FirstName" Mode="Edit" />
                </td>
            </tr>
            <tr>
                <td>
        LastName:
                </td>
                <td>
        <asp:DynamicControl ID="LastNameDynamicControl" runat="server" 
            DataField="LastName" Mode="Edit" />
                </td>
            </tr>
            <tr>
                <td>
        Email:
                </td>
                <td>
        <asp:DynamicControl ID="EmailDynamicControl" runat="server" DataField="Email" 
            Mode="Edit" />
                </td>
            </tr>
            <tr>
                <td>
        Password:
                </td>
                <td>
        <asp:DynamicControl ID="PasswordDynamicControl" runat="server" 
            DataField="Password" Mode="Edit" />
                </td>
            </tr>
            <tr>
                <td>
                    Role:
                </td>
                <td>
                    <asp:RadioButtonList ID="MemebershipRoleRadioButtonList" runat="server">
                    </asp:RadioButtonList>
                    <asp:ObjectDataSource ID="MembershipRoleObjectDataSource" runat="server" 
                        SelectMethod="GetAllRoles" 
                        TypeName="System.Web.Security.Roles"></asp:ObjectDataSource>
                </td>
            <tr>
                <td>
                    IsEnabled: &nbsp;&nbsp;</td>
                <td>
                    <asp:DynamicControl ID="IsEnabledDynamicControl" runat="server" 
                        DataField="IsEnabled" Mode="Edit" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                        CommandName="Update" Text="Update" />
                    <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" 
                        CommandName="Cancel" Text="Cancel" />
                </td>
            </tr>
    </table>

    </EditItemTemplate>
    <InsertItemTemplate>
        DepartmentID:
        <asp:DropDownList runat="server" ID="DepartmentDropDownList"
            DataTextField="Name" DataValueField="DepartmentID" 
            DataSourceID="DepartmentObjectDataSource">
        </asp:DropDownList>
        <asp:ObjectDataSource runat="server" ID="DepartmentObjectDataSource"
            DataObjectTypeName="SA33.Team12.SSIS.DAL.User" 
            TypeName="SA33.Team12.SSIS.BLL.UserManager"
            SelectMethod="GetAllDepartments">
        </asp:ObjectDataSource>
        <br />
        UserName:
        <asp:DynamicControl ID="UserNameDynamicControl" runat="server" 
            DataField="UserName" Mode="Insert" ValidationGroup="Insert" />
        <br />
        FirstName:
        <asp:DynamicControl ID="FirstNameDynamicControl" runat="server" 
            DataField="FirstName" Mode="Insert" ValidationGroup="Insert" />
        <br />
        LastName:
        <asp:DynamicControl ID="LastNameDynamicControl" runat="server" 
            DataField="LastName" Mode="Insert" ValidationGroup="Insert" />
        <br />
        Email:
        <asp:DynamicControl ID="EmailDynamicControl" runat="server" DataField="Email" 
            Mode="Insert" ValidationGroup="Insert" />
        <br />
        Password:
        <asp:DynamicControl ID="PasswordDynamicControl" runat="server" 
            DataField="Password" Mode="Insert" ValidationGroup="Insert" />
        <br />
        IsEnabled:
        <asp:DynamicControl ID="IsEnabledDynamicControl" runat="server" 
            DataField="IsEnabled" Mode="Insert" ValidationGroup="Insert" />
        <br />
        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
            CommandName="Insert" Text="Insert" ValidationGroup="Insert" />
        &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
            CausesValidation="False" CommandName="Cancel" Text="Cancel" />
    </InsertItemTemplate>
    <ItemTemplate>
        UserID:
        <asp:DynamicControl ID="UserIDDynamicControl" runat="server" DataField="UserID" 
            Mode="ReadOnly" />
        <br />
        DepartmentID:
        <asp:DynamicControl ID="DepartmentIDDynamicControl" runat="server" 
            DataField="DepartmentID" Mode="ReadOnly" />
        <br />
        UserName:
        <asp:DynamicControl ID="UserNameDynamicControl" runat="server" 
            DataField="UserName" Mode="ReadOnly" />
        <br />
        FirstName:
        <asp:DynamicControl ID="FirstNameDynamicControl" runat="server" 
            DataField="FirstName" Mode="ReadOnly" />
        <br />
        LastName:
        <asp:DynamicControl ID="LastNameDynamicControl" runat="server" 
            DataField="LastName" Mode="ReadOnly" />
        <br />
        Email:
        <asp:DynamicControl ID="EmailDynamicControl" runat="server" DataField="Email" 
            Mode="ReadOnly" />
        <br />
        Password:
        <asp:DynamicControl ID="PasswordDynamicControl" runat="server" 
            DataField="Password" Mode="ReadOnly" />
        <br />
        IsEnabled:
        <asp:DynamicControl ID="IsEnabledDynamicControl" runat="server" 
            DataField="IsEnabled" Mode="ReadOnly" />
        <br />
        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" 
            CommandName="Edit" Text="Edit" />
        &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" 
            CommandName="Delete" Text="Delete" />
        &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" 
            CommandName="New" Text="New" />
    </ItemTemplate>
</asp:FormView>

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

<asp:ValidationSummary runat="server" ID="ValidationSummary" 
    CssClass="failureNotification" />


</asp:Content>
