<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Disbursement.aspx.cs" Inherits="SA33.Team12.SSIS.Handle_Request.Disbursement" %>
<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<fieldset>
<legend>Stationery Retrieval Form List</legend>
Filter By Disbursement Status:
    <asp:DropDownList ID="DDLIsDisbursed" runat="server">
        <asp:ListItem Value="true">Already Disbursed</asp:ListItem>
        <asp:ListItem Value="false">Never Disbursed</asp:ListItem>
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="BtnFilter" runat="server" Text="Filter" 
        onclick="BtnFilter_Click" />
    <asp:GridView ID="SRFGridView" runat="server" AutoGenerateColumns="False"
         AllowPaging="True" onpageindexchanging="SRFGridView_PageIndexChanging" 
        onrowdatabound="SRFGridView_RowDataBound">
        <Columns>
            <asp:BoundField DataField="StationeryRetrievalFormID" HeaderText="SRFID" 
                SortExpression="StationeryRetrievalFormID" />
            <asp:BoundField DataField="StationeryRetrievalNumber" 
                HeaderText="StationeryRetrievedNumber" 
                SortExpression="StationeryRetrievalNumber" />
            <asp:BoundField DataField="DateRetrieved" HeaderText="DateRetrieved" 
                SortExpression="DateRetrieved" />
            <asp:TemplateField HeaderText="RetrievedBy">
                 <ItemTemplate>
                     <asp:Literal runat="server" ID="RetrievedByLiteral" />
                 </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="IsDistributed" HeaderText="IsDisbursed" 
                SortExpression="IsDistributed" />
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
</fieldset>

<fieldset>
<legend>Disbursement List</legend>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="FindAllDisbursement" 
        TypeName="SA33.Team12.SSIS.BLL.DisbursementManager"></asp:ObjectDataSource>
    <asp:GridView ID="DisbursementGridView" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="DisbursementID" 
        DataSourceID="ObjectDataSource1" SelectedRowStyle-BackColor="Silver"
        onrowdatabound="DisbursementGridView_RowDataBound">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="DisbursementID" HeaderText="DisbursementID" 
                SortExpression="DisbursementID" />
            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" 
                SortExpression="DateCreated" />
            <asp:TemplateField HeaderText="StationeryRetrievedNumber" 
                SortExpression="StationeryRetrievalFormID">
                <ItemTemplate>
                    <%# ((StationeryRetrievalForm) Eval("StationeryRetrievalForm")).StationeryRetrievalNumber %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CreateBy">
                <ItemTemplate>
                    <asp:Label ID="UserNameLabel" runat="server" Text='<%# Eval("CreatedBy") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</fieldset>

<fieldset>
<legend>Disbursement Items</legend>
    <asp:ObjectDataSource ID="DisbursementItemObjectDataSource" runat="server"></asp:ObjectDataSource>
</fieldset>
</asp:Content>
