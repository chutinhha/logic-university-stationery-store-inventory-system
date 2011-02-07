<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationeryRetrievalList.aspx.cs" Inherits="SA33.Team12.SSIS.StationeryRetrieval.StationeryRetrievalList" %>
<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Statoinery Retrieval Forms</h2>
<asp:Label runat="server" ID="ErrorMessage" CssClass="failureNotification"></asp:Label>
<fieldset>
<legend>Stationery Retrieval Forms</legend>
<asp:GridView runat="server" ID="StationeryRetrievalFormGridView" 
        AutoGenerateColumns="False" 
        onrowdatabound="StationeryRetrievalFormGridView_RowDataBound" 
        onrowcommand="StationeryRetrievalFormGridView_RowCommand">
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
        <asp:CheckBoxField DataField="IsRetrieved" HeaderText="Is Retrieved" 
            SortExpression="IsRetrieved" />
        <asp:CheckBoxField DataField="IsCollected" HeaderText="Is Collected" 
            SortExpression="IsCollected" />
        <asp:CheckBoxField DataField="IsDistributed" HeaderText="Is Disbursed" 
            SortExpression="IsDistributed" />
        <asp:TemplateField>
            <ItemTemplate>
                <a href='<%= Page.ResolveClientUrl("~") %>HandleRequest/Disbursements.aspx?SRFId=<%# Eval("StationeryRetrievalFormID") %>'<%# (bool) Eval("IsDistributed") ? "" : " style='display:none'" %>>View Disbursements</a>
                <asp:LinkButton runat="server" ID="DisburseLinkButton" CommandName="Disburse"
                    CommandArgument='<%# Eval("StationeryRetrievalFormID")%>'
                    Text="Disburse" Visible='<%# (bool) Eval("IsCollected") && !(bool) Eval("IsDistributed") %>' />

            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</fieldset>
<fieldset>
<legend>Create a new stationery retrieval form form all pending and partially fulfilled requisitions</legend>
<asp:Button runat="server" Text="Create A New Stationery Retrieval Form" 
        ID="CreateButton" onclick="CreateButton_Click" />
</fieldset>
</asp:Content>
