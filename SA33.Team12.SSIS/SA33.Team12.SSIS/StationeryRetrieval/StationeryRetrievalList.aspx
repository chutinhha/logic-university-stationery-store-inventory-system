<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationeryRetrievalList.aspx.cs" Inherits="SA33.Team12.SSIS.StationeryRetrieval.StationeryRetrievalList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Statoinery Retrieval Forms</h2>
<fieldset>
<legend>Stationery Retrieval Forms</legend>
<asp:GridView runat="server" ID="StationeryRetrievalFormGridView" 
        AutoGenerateColumns="False" 
        onrowdatabound="StationeryRetrievalFormGridView_RowDataBound">
    <Columns>
        <asp:BoundField DataField="StationeryRetrievalFormID" 
            HeaderText="StationeryRetrievalFormID" 
            SortExpression="StationeryRetrievalFormID" />
        <asp:BoundField DataField="StationeryRetrievalNumber" 
            HeaderText="StationeryRetrievalNumber" 
            SortExpression="StationeryRetrievalNumber" />
        <asp:TemplateField HeaderText="RetrievedBy" SortExpression="RetrievedBy">
            <ItemTemplate>
                <asp:Literal runat="server" ID="RetrievedByLiteral" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="DateRetrieved" HeaderText="DateRetrieved" 
            SortExpression="DateRetrieved" DataFormatString="{0:dd/MMM/yyyy}" />
    </Columns>
</asp:GridView>
</fieldset>
</asp:Content>
