 <%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewRequestHistory.aspx.cs" Inherits="SA33.Team12.SSIS.Test.ViewRequestHistory" %> 
 
 <%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style2
        {
            width: 96px;
        }
        .style3
        {
            width: 70px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Request History</h2>
    <p>
        <table style="width:100%;">
            <tr>
                <td class="style3">
                    Month</td>
                <td class="style2">
                    Year</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:DropDownList ID="MonthDDL" runat="server">
                        <asp:ListItem Value="1">Jan</asp:ListItem>
                        <asp:ListItem Value="2">Feb</asp:ListItem>
                        <asp:ListItem Value="3">Mar</asp:ListItem>
                        <asp:ListItem Value="4">Apr</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">Jun</asp:ListItem>
                        <asp:ListItem Value="7">Jul</asp:ListItem>
                        <asp:ListItem Value="8">Aug</asp:ListItem>
                        <asp:ListItem Value="9">Sep</asp:ListItem>
                        <asp:ListItem Value="10">Oct</asp:ListItem>
                        <asp:ListItem Value="11">Nov</asp:ListItem>
                        <asp:ListItem Value="12">Dec</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style2">
                    <asp:DropDownList ID="YearDDL" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="SearchButton" runat="server" onclick="SearchButton_Click" 
                        style="margin-left: 0px" Text="Search" />
                    <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </p>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
          <asp:TemplateField HeaderText="RequestID">
          <ItemTemplate>
              <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl='<%# String.Format("~/RequestStationery/StationeryRequest.aspx?RequestID=" + Eval("RequisitionID")) %>'><%# Eval("RequisitionID") %></asp:LinkButton>
          </ItemTemplate>
          </asp:TemplateField>
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
