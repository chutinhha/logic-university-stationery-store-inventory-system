<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="SA33.Team12.SSIS.Catalog.Categories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Category</h2>
    
    <asp:GridView runat="server" ID="CategoryGridView" AllowPaging="True" DataKeyNames="CategoryID"
        AutoGenerateColumns="False" DataSourceID="CategoryObjectDataSource"
        SelectedRowStyle-BackColor="LightGray" 
        onselectedindexchanged="CategoryGridView_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                ShowSelectButton="True" />
            <asp:BoundField DataField="CategoryID" HeaderText="CategoryID" 
                SortExpression="CategoryID" />
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:DynamicControl runat="server" ID="NameLabel"
                        DataField="Name" Mode="ReadOnly" /> 
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DynamicControl runat="server" ID="NameTextBox"
                        DataField="Name" Mode="Edit" /> 
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UnitOfMeasure" HeaderText="UnitOfMeasure" 
                SortExpression="UnitOfMeasure" />
        </Columns>
    </asp:GridView>

    <asp:ObjectDataSource ID="CategoryObjectDataSource" runat="server" 
        DataObjectTypeName="SA33.Team12.SSIS.DAL.Category" DeleteMethod="DeleteCategory" 
        InsertMethod="CreateCategory" SelectMethod="GetAllCategory" 
        TypeName="SA33.Team12.SSIS.BLL.CatalogManager" UpdateMethod="UpdateCategory">
    </asp:ObjectDataSource>


    <asp:DynamicDataManager ID="DynamicDataManager" runat="server" />

    <asp:ValidationSummary runat="server" DisplayMode="BulletList" 
        CssClass="failureNotification" />
    <asp:GridView runat="server" ID="StationeryGridView"
        AutoGenerateColumns="true">
    </asp:GridView>

</asp:Content>
