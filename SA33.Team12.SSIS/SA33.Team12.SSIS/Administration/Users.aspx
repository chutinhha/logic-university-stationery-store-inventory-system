﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Users.aspx.cs" Inherits="SA33.Team12.SSIS.UserAdministration.Users" %>

<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        User List</h2>
    <p class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
    </p>
    <fieldset>
        <legend>User List</legend>
        <asp:GridView runat="server" ID="UserGridView" AutoGenerateColumns="False" DataKeyNames="UserID"
            OnRowCommand="UserGridView_RowCommand" AllowPaging="True" 
            SelectedRowStyle-BackColor="Silver" 
            onpageindexchanging="UserGridView_PageIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName"
                    ReadOnly="True" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:TemplateField HeaderText="Department">
                    <ItemTemplate>
                        <%# ((Department) Eval("Department")).Name %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
                <asp:CheckBoxField DataField="IsEnabled" HeaderText="Enabled" ReadOnly="true" />
                <asp:CommandField ShowSelectButton="True" />
                <asp:ButtonField ButtonType="Link" CommandName="Disable" Text="Disable" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="UserObjectDataSource" runat="server" SelectMethod="GetAllUsers"
            TypeName="SA33.Team12.SSIS.Utilities.Membership" DeleteMethod="DeleteUser" DataObjectTypeName="SA33.Team12.SSIS.DAL.User"
            InsertMethod="CreateUser" UpdateMethod="UpdateUser"></asp:ObjectDataSource>
    </fieldset>
    <fieldset>
        <legend>User Detail</legend>
        <asp:FormView runat="server" ID="UserFormView" EnableModelValidation="True" DataSourceID="UserDetailObjectDataSource"
            OnItemInserting="UserFormView_ItemInserting" OnItemUpdating="UserFormView_ItemUpdating"
            OnItemCommand="UserFormView_ItemCommand" OnDataBound="UserFormView_DataBound">
            <EmptyDataTemplate>
                Please select a user to view its detail.
                <br />
                <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                    Text="New" CssClass="button" />
            </EmptyDataTemplate>
            <EditItemTemplate>
                <table class="screenFriendlyGridView">
                    <tr class="odd">
                        <th>
                            DepartmentID:
                        </th>
                        <td>
                            <asp:DynamicControl ID="UserIDDynamicControl" runat="server" DataField="UserID" Visible="false" Mode="Edit" />
                            <asp:DynamicControl ID="DepartmentIDDynamicControl" runat="server" DataField="DepartmentID"
                                Mode="Edit" Visible="false" />
                            <asp:DropDownList runat="server" ID="DepartmentDropDownList" DataTextField="Name"
                                DataValueField="DepartmentID" DataSourceID="DepartmentObjectDataSource" SelectedValue='<%# Eval("DepartmentID") %>'>
                            </asp:DropDownList>
                            <asp:ObjectDataSource runat="server" ID="DepartmentObjectDataSource" DataObjectTypeName="SA33.Team12.SSIS.DAL.User"
                                TypeName="SA33.Team12.SSIS.BLL.UserManager" SelectMethod="GetAllDepartments">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            UserName:
                        </th>
                        <td>
                            <asp:DynamicControl ID="UserNameDynamicControl" runat="server" DataField="UserName"
                                Mode="Edit" />
                        </td>
                    </tr>
                    <tr class="odd">
                        <th>
                            FirstName:
                        </th>
                        <td>
                            <asp:DynamicControl ID="FirstNameDynamicControl" runat="server" DataField="FirstName"
                                Mode="Edit" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            LastName:
                        </th>
                        <td>
                            <asp:DynamicControl ID="LastNameDynamicControl" runat="server" DataField="LastName"
                                Mode="Edit" />
                        </td>
                    </tr>
                    <tr class="odd">
                        <th>
                            Email:
                        </th>
                        <td>
                            <asp:DynamicControl ID="EmailDynamicControl" runat="server" DataField="Email" Mode="Edit" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Password:
                        </th>
                        <td>
                            <asp:DynamicControl ID="PasswordDynamicControl" runat="server" DataField="Password"
                                Mode="Edit" />
                        </td>
                    </tr>
                    <tr class="odd">
                        <th>
                            Role:
                        </th>
                        <td>
                            <asp:DynamicControl ID="RoleDynamicControl" runat="server" DataField="Role" Mode="Edit"
                                Visible="false" />
                            <asp:DropDownList ID="MemebershipRoleDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="MembershipRoleObjectDataSource" runat="server" SelectMethod="GetAllRoles"
                                TypeName="System.Web.Security.Roles"></asp:ObjectDataSource>
                        </td>
                        <tr>
                            <th>
                                IsEnabled: &nbsp;&nbsp;
                            </th>
                            <td>
                                <asp:DynamicControl ID="IsEnabledDynamicControl" runat="server" DataField="IsEnabled"
                                    Mode="Edit" />
                            </td>
                        </tr>
                    <tr class="odd">
                            <th>
                                &nbsp;
                            </th>
                            <td>
                                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="Update" />
                                <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel" />
                            </td>
                        </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table class="screenFriendlyGridView">
                    <tr class="odd">
                        <td>
                            DepartmentID:
                        </td>
                        <td>
                            <asp:DynamicControl ID="DepartmentIDDynamicControl" runat="server" DataField="DepartmentID"
                                Mode="Insert" Visible="false" />
                            <asp:DropDownList runat="server" ID="DepartmentDropDownList" DataTextField="Name"
                                DataValueField="DepartmentID" DataSourceID="DepartmentObjectDataSource">
                            </asp:DropDownList>
                            <asp:ObjectDataSource runat="server" ID="DepartmentObjectDataSource" DataObjectTypeName="SA33.Team12.SSIS.DAL.User"
                                TypeName="SA33.Team12.SSIS.BLL.UserManager" SelectMethod="GetAllDepartments">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            UserName:
                        </td>
                        <td>
                            <asp:DynamicControl ID="UserNameDynamicControl" runat="server" DataField="UserName"
                                Mode="Insert" />
                        </td>
                    </tr>
                    <tr class="odd">
                        <td>
                            FirstName:
                        </td>
                        <td>
                            <asp:DynamicControl ID="FirstNameDynamicControl" runat="server" DataField="FirstName"
                                Mode="Insert" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            LastName:
                        </td>
                        <td>
                            <asp:DynamicControl ID="LastNameDynamicControl" runat="server" DataField="LastName"
                                Mode="Insert" />
                        </td>
                    </tr>
                    <tr class="odd">
                        <td>
                            Email:
                        </td>
                        <td>
                            <asp:DynamicControl ID="EmailDynamicControl" runat="server" DataField="Email" Mode="Insert" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Password:
                        </td>
                        <td>
                            <asp:DynamicControl ID="PasswordDynamicControl" runat="server" DataField="Password"
                                Mode="Insert" />
                        </td>
                    </tr>
                    <tr class="odd">
                        <th>
                            Role:
                        </th>
                        <td>
                            <asp:DynamicControl ID="RoleDynamicControl" runat="server" DataField="Role" Mode="Insert"
                                Visible="false" />
                            <asp:DropDownList ID="MemebershipRoleDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="MembershipRoleObjectDataSource" runat="server" SelectMethod="GetAllRoles"
                                TypeName="System.Web.Security.Roles"></asp:ObjectDataSource>
                        </td>
                        <tr>
                            <td>
                                IsEnabled: &nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:DynamicControl ID="IsEnabledDynamicControl" runat="server" DataField="IsEnabled"
                                    Mode="Insert" />
                            </td>
                        </tr>
                    <tr class="odd">
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="Insert" ValidationGroup="Insert" />
                                &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False"
                                    CommandName="Cancel" Text="Cancel" />
                            </td>
                        </tr>
                </table>
            </InsertItemTemplate>
            <ItemTemplate>
                <table class="screenFriendlyGridView">
                    <tbody>
                    <tr class="odd">
                            <th>
                                Department</th>
                            <td>
                                <%# ((Department) Eval("Department")).Name %>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                User Name</th>
                            <td>
                                <asp:DynamicControl ID="UserNameDynamicControl" runat="server" 
                                    DataField="UserName" Mode="ReadOnly" />
                            </td>
                        </tr>
                    <tr class="odd">
                            <th>
                                First Name</th>
                            <td>
                                <asp:DynamicControl ID="FirstNameDynamicControl" runat="server" 
                                    DataField="FirstName" Mode="ReadOnly" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Last Name</th>
                            <td>
                                <asp:DynamicControl ID="LastNameDynamicControl" runat="server" 
                                    DataField="LastName" Mode="ReadOnly" />
                            </td>
                        </tr>
                    <tr class="odd">
                            <th>
                                Email</th>
                            <td>
                                <asp:DynamicControl ID="EmailDynamicControl" runat="server" DataField="Email" 
                                    Mode="ReadOnly" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Password</th>
                            <td>
                                <asp:DynamicControl ID="PasswordDynamicControl" runat="server" 
                                    DataField="Password" Mode="ReadOnly" />
                            </td>
                        </tr>
                    <tr class="odd">
                            <th>
                                IsEnabled</th>
                            <td>
                                <asp:DynamicControl ID="IsEnabledDynamicControl" runat="server" 
                                    DataField="IsEnabled" Mode="ReadOnly" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                    Text="Edit" />
                &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                    Text="Delete" />
                &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                    Text="New" />
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="UserDetailObjectDataSource" runat="server" DataObjectTypeName="SA33.Team12.SSIS.DAL.User"
            DeleteMethod="DeleteUser" InsertMethod="CreateUser" SelectMethod="GetUsersById"
            TypeName="SA33.Team12.SSIS.Utilities.Membership" UpdateMethod="UpdateUser">
            <SelectParameters>
                <asp:ControlParameter ControlID="UserGridView" Name="userId" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </fieldset>
    <asp:DynamicDataManager runat="server" ID="DynamicDataManager"></asp:DynamicDataManager>
    <asp:ValidationSummary runat="server" ID="ValidationSummary" CssClass="failureNotification" />
</asp:Content>
