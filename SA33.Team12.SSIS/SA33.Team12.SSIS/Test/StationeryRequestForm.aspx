<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationeryRequestForm.aspx.cs" Inherits="SA33.Team12.SSIS.Test.StationeryRequestForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 181px;
        }
        .style2
        {
            width: 606px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Stationery request form</h1>
 
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    Requisition Date</td>
                <td>
                    <asp:Literal ID="RequisitionDateLiteral" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td class="style1">
                    Department Name</td>
                <td>
                    <asp:Literal ID="DepartmentNameLiteral" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td class="style1">
                    Department Code</td>
                <td>
                    <asp:Literal ID="DepartmentCodeLiteral" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Employee Name</td>
                <td>
                    <asp:Literal ID="EmployeeNameLiteral" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Employee Number</td>
                <td>
                    <asp:Literal ID="EmployeeNumberLiteral" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Employee Email</td>
                <td>
                    <asp:Literal ID="EmployeeEmailLiteral" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Urgency</td>
                <td>
                    <asp:DropDownList ID="UrgencyDDL" runat="server" DataSourceID="UrgencyDS" 
                        DataTextField="Name" DataValueField="UrgencyID">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="UrgencyDS" runat="server" 
                        SelectMethod="GetAllUrgencies" 
                        TypeName="SA33.Team12.SSIS.BLL.RequisitionManager"></asp:ObjectDataSource>
                </td>
            </tr>
        </table>
        <br />
    <table style="width: 100%;">
        <tr>
            <td>
                Category</td>
            <td>
                Item Description</td>
            <td>
                Item Code</td>
            <td>
                Quantity</td>
            <td>
                Action</td>
        </tr>
        <tr>
            <td style="margin-left: 40px">
                &nbsp;
                <asp:DropDownList ID="CategoryDDL" runat="server" DataSourceID="CategoryDS" 
                    DataTextField="Name" DataValueField="CategoryID" AutoPostBack="True">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="CategoryDS" runat="server" 
                    SelectMethod="GetAllCategories" 
                    TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                </asp:ObjectDataSource>
            </td>
            <td>
                <asp:DropDownList ID="StationeryDDL" runat="server" DataSourceID="ItemDS" 
                    DataTextField="Description" DataValueField="StationeryID" 
                    AutoPostBack="True">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="ItemDS" runat="server" 
                    SelectMethod="GetStationeriesByCategory" 
                    TypeName="SA33.Team12.SSIS.DAL.CatalogDAO">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="CategoryDDL" Name="CategoryID" 
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td>
                <asp:Literal ID="ItemCodeLiteral" runat="server"></asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="QuantityTextBox" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="AddButton" runat="server" Text="Add" 
                    onclick="AddButton_Click" />
                </td>
        </tr>        
    </table>
    <br />
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <br />
    <table style="width:100%;">
        <tr>
            <td>
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
