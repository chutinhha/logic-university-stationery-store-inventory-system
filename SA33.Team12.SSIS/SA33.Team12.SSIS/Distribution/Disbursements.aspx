<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Disbursements.aspx.cs" Inherits="SA33.Team12.SSIS.Distribution.Disbursements" %>
<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Distribution</h2>

<fieldset>
<legend>Maintain Disbursements</legend>

<asp:GridView runat="server" 
    ID="DisbursementGridView" 
        AutoGenerateColumns="False" 
        AllowPaging="True">
    <Columns>
        <asp:BoundField DataField="DisbursementID" HeaderText="Disbursement Id" 
            SortExpression="DisbursementID" />
        <asp:TemplateField
            HeaderText="Stationery Retrieval No." 
            SortExpression="StationeryRetrievalFormID">
            <ItemTemplate>
                <a href='<%= Page.ResolveClientUrl("~") %>HandleRequest/StationeryRetrievalForm.aspx?ID=<%# Eval("StationeryRetrievalFormID") %>'>
                <%# ((StationeryRetrievalForm) Eval("StationeryRetrievalForm")).StationeryRetrievalNumber %></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="DepartmentID" 
            SortExpression="DepartmentID">
            <ItemTemplate>
                <%# ((Department)Eval("Department")).Name %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" 
            SortExpression="DateCreated" DataFormatString="{0:dd/MMM/yyyy}" />
        <asp:TemplateField HeaderText="CreatedBy" 
            SortExpression="CreatedBy">
            <ItemTemplate>
                <%# ((User)Eval("User")).UserName %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <a href='<%=Page.ResolveClientUrl("~") %>Distribution/Distribution.aspx?DisbursementID=<%# Eval("DisbursementID") %>' <%# (bool) Eval("IsDistributed") ? "style='display:none'" : "" %>>
                    Distribute</a>
                    
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

</fieldset>

</asp:Content>
