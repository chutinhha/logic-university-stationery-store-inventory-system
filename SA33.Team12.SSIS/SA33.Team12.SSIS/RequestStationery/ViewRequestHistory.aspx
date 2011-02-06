<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ViewRequestHistory.aspx.cs" Inherits="SA33.Team12.SSIS.Test.ViewRequestHistory" %>

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
    <h2>
        Request History</h2>
        <fieldset>
            <legend>Filter</legend>
      <table>
        <tr>
            <th align="left">
                Month :
            </th>
            <td>
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
            <td>&nbsp;</td>
            <th align="left">
                Year :
            </th>
            <td>
                <asp:DropDownList ID="YearDDL" runat="server">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="SearchButton" runat="server" OnClick="SearchButton_Click"
                    Text="Search" />
                <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
        </fieldset>
        <fieldset>
            <legend>Results</legend>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Requisition Form#">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl='<%# String.Format("~/RequestStationery/StationeryRequest.aspx?RequestID=" + Eval("RequisitionID")) %>'><%# Eval("RequisitionForm")%></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CreatedBy" SortExpression="CreatedBy">
                <ItemTemplate>
                    <%# ((User) Eval("CreatedByUser")).UserName %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Department" SortExpression="DepartmentID">
                <ItemTemplate>
                    <%# ((Department) Eval("Department")).Name %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Approved By" SortExpression="ApprovedBy">
                <ItemTemplate>
                    <%# (Convert.ToInt32(Eval("ApprovedBy")) == 0)? "" : ((User) Eval("ApprovedByUser")).UserName %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status" SortExpression="StatusID">
                <ItemTemplate>
                    <%# (Convert.ToInt32(Eval("StatusID")) == 0)? "" : ((Status) Eval("Status")).Name %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Urgency" SortExpression="UrgencyID">
                <ItemTemplate>
                    <%# (Convert.ToInt32(Eval("UrgencyID")) == 0)? "" : ((Urgency) Eval("Urgency")).Name %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DateRequested" HeaderText="DateRequested" SortExpression="DateRequested"
                DataFormatString="{0:dd/MMM/yyyy}" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="DateApproved" HeaderText="DateApproved" SortExpression="DateApproved"
                DataFormatString="{0:dd/MMM/yyyy}" ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </asp:GridView>
        </fieldset>
</asp:Content>
