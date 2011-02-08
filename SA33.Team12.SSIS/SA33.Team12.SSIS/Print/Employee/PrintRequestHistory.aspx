<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrintRequestHistory.aspx.cs" Inherits="SA33.Team12.SSIS.Print.Employee.PrintRequestHistory" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Request history</h2>

    <br />
    <fieldset>
    <legend>Result</legend>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="RequisitionID" 
        onpageindexchanging="GridView1_PageIndexChanging">
        <Columns>
           <asp:TemplateField HeaderText="RequisitionID">
           <ItemTemplate>
               <asp:LinkButton ID="LinkButton1" PostBackUrl='<%# String.Format("~/RequestStationery/StationeryRequest.aspx?RequestID={0}", Eval("RequisitionID")) %>' runat="server"><%# Eval("RequisitionID") %></asp:LinkButton>
           </ItemTemplate>
           </asp:TemplateField>
            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" 
                SortExpression="CreatedBy" />
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
    </fieldset>
    <input id="Button1" type="button" value="Print" onclick="window.print();" />
</asp:Content>
