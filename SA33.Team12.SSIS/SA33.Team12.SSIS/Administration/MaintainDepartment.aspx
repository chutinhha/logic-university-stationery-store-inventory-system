<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaintainDepartment.aspx.cs" Inherits="SA33.Team12.SSIS.Administration.MaintainDepartment" %>
<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Departments</h2>
<fieldset>
<legend>Maintain Departments</legend>
<asp:GridView runat="server" ID="DepartmentGridView" AutoGenerateColumns="False" 
        DataSourceID="DepartmentObjectDataSource" AllowPaging="True" 
        DataKeyNames="DepartmentID" onrowupdating="DepartmentGridView_RowUpdating">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="DepartmentID" HeaderText="DepartmentID" 
            SortExpression="DepartmentID" Visible="false" />
        <asp:TemplateField HeaderText="Code" SortExpression="Code">
            <ItemTemplate>
                <asp:DynamicControl runat="server" DataField="Code" ID="CodeLabel" Mode="ReadOnly" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DynamicControl runat="server" DataField="Code" ID="CodeTextBox" Mode="Edit" ValidationGroup="Edit" />
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name" SortExpression="Name">
            <ItemTemplate>
                <asp:DynamicControl runat="server" DataField="Name" ID="NameLabel" Mode="ReadOnly" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DynamicControl runat="server" DataField="Name" ID="NameTextBox" Mode="Edit" ValidationGroup="Edit" />
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Collection Point">
            <ItemTemplate>
                <%# ((CollectionPoint) Eval("CollectionPoint")).Name %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DynamicControl ID="CollectionPointIDControl" runat="server" 
                    DataField="CollectionPointID" Mode="Edit" Visible="false" />
                <asp:DropDownList runat="server" ID="CollectionPointDropDownList" 
                    DataSourceID="CollectionPointObjectDataSource" DataTextField="Name" 
                    DataValueField="CollectionPointID"
                    SelectedValue='<%# Eval("CollectionPointID") %>'>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="CollectionPointObjectDataSource" runat="server" 
                    SelectMethod="GetAllCollectionPoints" 
                    TypeName="SA33.Team12.SSIS.BLL.UserManager"></asp:ObjectDataSource>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:CheckBoxField DataField="IsBlackListed" HeaderText="Blacklisted" 
            SortExpression="IsBlackListed" />
        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
            HeaderText="Actions" ValidationGroup="Edit" />
    </Columns>
</asp:GridView>
    <asp:ObjectDataSource ID="DepartmentObjectDataSource" runat="server" 
        SelectMethod="GetAllDepartments" 
        TypeName="SA33.Team12.SSIS.BLL.UserManager" 
        DataObjectTypeName="SA33.Team12.SSIS.DAL.Department" 
        DeleteMethod="DeleteDepartment" InsertMethod="CreateDepartment" 
        OldValuesParameterFormatString="original_{0}" UpdateMethod="UpdateDepartment">
    </asp:ObjectDataSource>
<asp:ValidationSummary runat="server" ValidationGroup="Edit" />
<asp:DynamicDataManager runat="server" ID="DynamicDataManager" />

</fieldset>

<fieldset>
    <legend>
        New Department
    </legend>

    <asp:DetailsView ID="DepartmentDetailsView" runat="server" AutoGenerateRows="false"
        DataSourceID="DepartmentObjectDataSource" DefaultMode="Insert" 
        oniteminserting="DepartmentDetailsView_ItemInserting">
        <Fields>
            <asp:TemplateField HeaderText="Code">
                <InsertItemTemplate>
                    <asp:DynamicControl runat="server"
                        ID="CodeControl"
                        DataField="Code"
                        Mode="Insert" />
                </InsertItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
                <InsertItemTemplate>
                    <asp:DynamicControl runat="server"
                        ID="NameControl"
                        DataField="Name"
                        Mode="Insert" />
                </InsertItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Collection Point">
                <InsertItemTemplate>
                    <asp:DynamicControl runat="server"
                        ID="CollectionPointControl"
                        DataField="CollectionPointID"
                        Mode="Insert" Visible="false" />
                <asp:DropDownList runat="server" ID="CollectionPointDropDownList" 
                    DataSourceID="CollectionPointObjectDataSource" 
                    DataTextField="Name" 
                    DataValueField="CollectionPointID">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="CollectionPointObjectDataSource" 
                    runat="server" 
                    SelectMethod="GetAllCollectionPoints" 
                    TypeName="SA33.Team12.SSIS.BLL.UserManager">
                    </asp:ObjectDataSource>
                </InsertItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="IsBlacklisted">
                <InsertItemTemplate>
                    <asp:DynamicControl runat="server"
                        ID="IsBlacklistedControl"
                        DataField="IsBlacklisted"
                        Mode="Insert" />
                </InsertItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>

</fieldset>
</asp:Content>
