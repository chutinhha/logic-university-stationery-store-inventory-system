<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisbursementBLLTest.aspx.cs" Inherits="SA33.Team12.SSIS.Test.DisbursementBLLTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<center>
<h1>Disbusement List</h1>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        DataObjectTypeName="SA33.Team12.SSIS.DAL.Disbursement" 
        DeleteMethod="DeleteDisbursement" InsertMethod="CreateDisbursement" 
        SelectMethod="FindAllDisbursement" 
        TypeName="SA33.Team12.SSIS.BLL.DisbursementManager" 
        UpdateMethod="UpdateDisbursement"></asp:ObjectDataSource>
    <br/>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
        <Columns>
            <asp:CommandField ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="DisbursementID" HeaderText="DisbursementID" 
                SortExpression="DisbursementID" />
            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" 
                SortExpression="DateCreated" />
            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" 
                SortExpression="CreatedBy" />
            <asp:BoundField DataField="StationeryRetrievalFormID" 
                HeaderText="StationeryRetrievalFormID" 
                SortExpression="StationeryRetrievalFormID" />
        </Columns>
    </asp:GridView>
</center>
</asp:Content>
