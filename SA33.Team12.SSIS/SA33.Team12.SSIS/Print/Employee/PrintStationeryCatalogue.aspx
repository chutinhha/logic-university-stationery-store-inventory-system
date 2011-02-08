<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrintStationeryCatalogue.aspx.cs" Inherits="SA33.Team12.SSIS.Print.Employee.PrintStationeryCatalogue1" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 118px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<fieldset>
<fieldset>
<legend>Filters</legend>
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                &nbsp;
                Category</td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="CategoryDS" 
                    DataTextField="Name" DataValueField="Name">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="CategoryDS" runat="server" 
                    SelectMethod="GetAllCategories" TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                </asp:ObjectDataSource>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
            </td>
            <td>
                <asp:Button ID="FilterButton" runat="server" onclick="FilterButton_Click" 
                    Text="Filter" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
            </td>
            <td>
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
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
        AutoDataBind="true" ToolPanelView="None" />
</fieldset>
</fieldset>
</asp:Content>
