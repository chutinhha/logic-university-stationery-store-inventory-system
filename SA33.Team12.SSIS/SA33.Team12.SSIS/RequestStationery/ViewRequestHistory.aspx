 <%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewRequestHistory.aspx.cs" Inherits="SA33.Team12.SSIS.Test.ViewRequestHistory" %> 
 
 <%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Request History</h2>
    <p>&nbsp;</p>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="RequisitionID" HeaderText="RequisitionID" 
                SortExpression="RequisitionID" />
            <asp:TemplateField HeaderText="CreatedBy" 
                SortExpression="CreatedBy">
                <ItemTemplate>
                    <%# ((User) Eval("CreatedByUser")).UserName %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DepartmentID" HeaderText="DepartmentID" 
                SortExpression="DepartmentID" />
            <asp:BoundField DataField="ApprovedBy" HeaderText="ApprovedBy" 
                SortExpression="ApprovedBy" />
            <asp:BoundField DataField="RequisitionForm" HeaderText="RequisitionForm" 
                SortExpression="RequisitionForm" />
            <asp:BoundField DataField="StatusID" HeaderText="StatusID" 
                SortExpression="StatusID" />
            <asp:BoundField DataField="UrgencyID" HeaderText="UrgencyID" 
                SortExpression="UrgencyID" />
            <asp:BoundField DataField="DateRequested" HeaderText="DateRequested" 
                SortExpression="DateRequested" />
            <asp:BoundField DataField="DateApproved" HeaderText="DateApproved" 
                SortExpression="DateApproved" />
        </Columns>
    </asp:GridView>
</asp:Content>
