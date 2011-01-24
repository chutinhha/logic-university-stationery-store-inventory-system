<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Stationeries.aspx.cs" Inherits="SA33.Team12.SSIS.Catalog.Stationeries" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="StationeryGridView" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataSourceID="StationeryObjectDataSource" DataKeyNames="StationeryID">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                ShowSelectButton="True" />
            <asp:TemplateField HeaderText="Item Code">
                <ItemTemplate>
                    <asp:DynamicControl runat="server" ID="ItemCodeLabel"
                        DataField="ItemCode" Mode="ReadOnly" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DynamicControl runat="server" ID="ItemCodeTextBox"
                        DataField="ItemCode" Mode="Edit" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Description" HeaderText="Description" 
                SortExpression="Description" />
            <asp:BoundField DataField="ReorderLevel" HeaderText="ReorderLevel" 
                SortExpression="ReorderLevel" />
            <asp:BoundField DataField="ReorderQuantity" HeaderText="ReorderQuantity" 
                SortExpression="ReorderQuantity" />
            <asp:BoundField DataField="QuantityInHand" HeaderText="QuantityInHand" 
                SortExpression="QuantityInHand" />
            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" 
                SortExpression="DateCreated" />
            <asp:BoundField DataField="DateModified" HeaderText="DateModified" 
                SortExpression="DateModified" />
        </Columns>
    </asp:GridView>

    <asp:ObjectDataSource runat="server" ID="StationeryObjectDataSource" 
        DataObjectTypeName="SA33.Team12.SSIS.DAL.Stationery" DeleteMethod="Delete" 
        InsertMethod="Insert" SelectMethod="GetAll" 
        TypeName="SA33.Team12.SSIS.BLL.StationeryManager" UpdateMethod="Update">
    </asp:ObjectDataSource>

    <asp:DynamicDataManager runat="server" ID="DyanamicDataManager" />

    <asp:ValidationSummary runat="server" ID="ValidationSummary" DisplayMode="BulletList"
        CssClass="failureNotification" />
</asp:Content>
