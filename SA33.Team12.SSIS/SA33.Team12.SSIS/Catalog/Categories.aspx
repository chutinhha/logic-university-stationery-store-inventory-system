<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="SA33.Team12.SSIS.Catalog.Categories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 109px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Category</h2>
<fieldset>
    <fieldset>
    <legend>Add Category</legend>
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    Category Name</td>
                <td>
                    <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                    <asp:Button ID="SubmitButton" runat="server" onclick="SubmitButton_Click" 
                        style="margin-left: 0px" Text="Add" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
    </fieldset>
    <fieldset>
    <legend>Category List</legend>
    <asp:GridView runat="server" ID="CategoryGridView" AllowPaging="True"
        AutoGenerateColumns="False" DataSourceID="CategoryObjectDataSource"
        SelectedRowStyle-BackColor="LightGray" 
        onselectedindexchanged="CategoryGridView_SelectedIndexChanged" 
        DataKeyNames="CategoryID">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                ShowSelectButton="True" />
            <asp:DynamicField DataField="CategoryID" HeaderText="CategoryID" />
            <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Name" 
                SortExpression="Name">
                <ItemTemplate>
                    <asp:DynamicControl ID="DynamicControl1" runat="server" DataField="Name" 
                        Mode="ReadOnly" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DynamicControl ID="DynamicControl1" runat="server" DataField="Name" 
                        Mode="Edit" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>

<SelectedRowStyle BackColor="LightGray"></SelectedRowStyle>
    </asp:GridView>

    <asp:ObjectDataSource ID="CategoryObjectDataSource" runat="server" 
        DataObjectTypeName="SA33.Team12.SSIS.DAL.Category" DeleteMethod="DeleteCategory" 
        InsertMethod="CreateCategory" SelectMethod="GetAllCategories" 
        TypeName="SA33.Team12.SSIS.BLL.CatalogManager" 
        UpdateMethod="UpdateCategory" OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>


    <asp:DynamicDataManager ID="DynamicDataManager" runat="server" />

    <asp:ValidationSummary runat="server" DisplayMode="BulletList" 
        CssClass="failureNotification" />
    <asp:GridView runat="server" ID="StationeryGridView"
        AutoGenerateColumns="true">
    </asp:GridView>
    </fieldset>
    </fieldset>
</asp:Content>
