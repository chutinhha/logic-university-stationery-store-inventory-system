<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Disbursement.aspx.cs" Inherits="SA33.Team12.SSIS.Handle_Request.Disbursement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<center>
<h1>Disbursement List</h1><br />
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="FindAllDisbursement" 
        TypeName="SA33.Team12.SSIS.BLL.DisbursementManager"></asp:ObjectDataSource>
    <asp:GridView ID="DisbursementGridView" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="DisbursementID" 
        DataSourceID="ObjectDataSource1" 
        onrowdatabound="DisbursementGridView_RowDataBound">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="DisbursementID" HeaderText="DisbursementID" 
                SortExpression="DisbursementID" />
            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" 
                SortExpression="DateCreated" />
            <asp:BoundField DataField="CreatedBy" HeaderText="CreateBy" 
                SortExpression="CreatedBy" />
            <asp:BoundField DataField="StationeryRetrievalFormID" 
                HeaderText="StationeryRetrievalFormID" 
                SortExpression="StationeryRetrievalFormID" />
            <asp:TemplateField HeaderText="UserName">
                <ItemTemplate>
                    <asp:Label ID="UserNameLabel" runat="server" Text='<%# Eval("CreatedBy") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</center>
</asp:Content>
