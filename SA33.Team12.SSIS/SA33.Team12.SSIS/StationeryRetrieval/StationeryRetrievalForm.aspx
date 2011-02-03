<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationeryRetrievalForm.aspx.cs" Inherits="SA33.Team12.SSIS.StationeryRetrieval.StationeryRetrievalForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Stationery Retrieval Form</h2>


    <asp:FormView runat="server" ID="StationeryRetrievalFormView" 
        DataSourceID="ods" DataKeyNames="StationeryRetrievalFormID" 
        ondatabound="StationeryRetrievalFormView_DataBound">
        <ItemTemplate>
            StationeryRetrievalFormID:
            <asp:Label ID="StationeryRetrievalFormIDLabel" runat="server" 
                Text='<%# Bind("StationeryRetrievalFormID") %>' />
            <br />
            StationeryRetrievalNumber:
            <asp:Label ID="StationeryRetrievalNumberLabel" runat="server" 
                Text='<%# Bind("StationeryRetrievalNumber") %>' />
            <br />
            RetrievedBy:
            <asp:Label ID="RetrievedByLabel" runat="server" 
                Text='<%# Bind("RetrievedBy") %>' />
            <br />
            DateRetrieved:
            <asp:Label ID="DateRetrievedLabel" runat="server" 
                Text='<%# Bind("DateRetrieved") %>' />
            <br />
            StationeryRetrievalFormItems:
            <asp:Label ID="StationeryRetrievalFormItemsLabel" runat="server" 
                Text='<%# Bind("StationeryRetrievalFormItems") %>' />
            <br />
            RetrievedByUser:
            <asp:Label ID="RetrievedByUserLabel" runat="server" 
                Text='<%# Bind("RetrievedByUser") %>' />
            <br />
        <asp:GridView runat="server" ID="StationeryRetrievalFormItemGridView" 
            DataSourceID="ods2">
        </asp:GridView>        
    </ItemTemplate>
    </asp:FormView>
    
    <asp:ObjectDataSource runat="server" ID="ods2" />

    <asp:ObjectDataSource runat="server" ID="ods" 
        SelectMethod="GetAllStationeryRetrievalForms" 
        TypeName="SA33.Team12.SSIS.BLL.StationeryRetrievalManager">
    </asp:ObjectDataSource>
</asp:Content>
