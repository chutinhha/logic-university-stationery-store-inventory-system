<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="StationeryRequisitionTrendReportByDept.aspx.cs" Inherits="SA33.Team12.SSIS.Test.StationeryRequisitionTrendReportByDept" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 71px;
        }
        .style2
        {
            width: 228px;
        }
        .style3
        {
            width: 57px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Logic University - Stationery Requisition trend Report</h2>
    <fieldset>
        <fieldset>
        <legend>Filters</legend>
            <table style="width: 100%;">
                <tr>
                    <td class="style1">
                        Department
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="DepartmentDDL" runat="server" AppendDataBoundItems="True" DataSourceID="DepartmentDS"
                            DataTextField="Name" DataValueField="Name">
                            <asp:ListItem>Select Department</asp:ListItem>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="DepartmentDS" runat="server" SelectMethod="GetAllDepartments"
                            TypeName="SA33.Team12.SSIS.BLL.UserManager"></asp:ObjectDataSource>
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="CategoryDDL" runat="server" DataSourceID="CategoryDS" DataTextField="Name"
                            DataValueField="Name" AppendDataBoundItems="True" Visible="False">
                            <asp:ListItem>Select a category</asp:ListItem>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="CategoryDS" runat="server" SelectMethod="GetAllCategories"
                            TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Month
                    </td>
                    <td class="style2">
                        &nbsp;
                        <asp:ListBox ID="MonthListBox" runat="server" Height="84px" SelectionMode="Multiple"
                            Width="111px">
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May </asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:ListBox>
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td class="style2">
                        &nbsp;
                        <asp:Button ID="FilterButton" runat="server" OnClick="FilterButton_Click" Text="Filter" />
                        <asp:Button ID="ResetButton" runat="server" OnClick="ResetButton_Click" Text="Reset" />
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </fieldset>
    <fieldset>
    <legend>Result</legend>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
            ToolPanelView="None" />
    </fieldset>
    </fieldset>
</asp:Content>
