<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationeryRetrievalList.aspx.cs" Inherits="SA33.Team12.SSIS.StationeryRetrieval.StationeryRetrievalList" %>
<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
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
            HeaderText="ID" 
            SortExpression="StationeryRetrievalFormID" />
        <asp:TemplateField 
            HeaderText="Form No." 
            SortExpression="StationeryRetrievalNumber">
            <ItemTemplate>
                <a href='StationeryRetrievalForm.aspx?ID=<%# Eval("StationeryRetrievalFormID")%>'>
                <%# Eval("StationeryRetrievalNumber")%>
                </a>
            </ItemTemplate>
            
            </asp:TemplateField>

        <asp:TemplateField HeaderText="RetrievedBy" SortExpression="RetrievedBy">
            <ItemTemplate>
                <%# DBNull.Value.Equals(Eval("RetrievedByUser"))
                                                            ? "" : ((User)Eval("RetrievedByUser")).UserName%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="DateRetrieved" HeaderText="DateRetrieved" 
            SortExpression="DateRetrieved" DataFormatString="{0:dd/MMM/yyyy}" />
    </Columns>
</asp:GridView>
</fieldset>
</asp:Content>
