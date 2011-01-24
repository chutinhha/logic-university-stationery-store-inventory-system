<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Stationeries.aspx.cs" Inherits="SA33.Team12.SSIS.Test.Stationeries" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Stationery List</h2>

<asp:GridView runat="server" ID="StationeryGridView" AllowPaging="True" 
        DataSourceID="StationeryObjectDataSource">
    <Columns>
        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
    </Columns>
</asp:GridView>

<asp:ObjectDataSource runat="server" 
    ID="StationeryObjectDataSource" 
    DataObjectTypeName="SA33.Team12.SSIS.DAL.Stationery" 
    TypeName="SA33.Team12.SSIS.BLL.StationeryManager" 
    DeleteMethod="DeleteStationery" 
    InsertMethod="CreateStationery" 
    SelectMethod="GetAllStationery" 
    UpdateMethod="UpdateStationery">
</asp:ObjectDataSource>

<asp:ValidationSummary runat="server" ID="ValidationSummary"
    CssClass="failureNotification" />

<asp:DynamicDataManager runat="server" ID="DynamicDataManager" />

</asp:Content>
