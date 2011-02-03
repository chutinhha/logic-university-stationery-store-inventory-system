<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewPurchaseOrder.aspx.cs" Inherits="SA33.Team12.SSIS.Stock.ViewPurchaseOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 112px;
        }
        .style2
        {
            width: 276px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1>View Purchase Order</h1>
    <table style="width:100%;">
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                Supplier</td>
            <td class="style2">
                <asp:DropDownList ID="ddlSupplier" runat="server" 
                    DataSourceID="ObjectDataSource1" DataTextField="CompanyName" 
                    DataValueField="SupplierID">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                    SelectMethod="GetAllSuppliers" TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                </asp:ObjectDataSource>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                Order Date</td>
            <td class="style2">
                From
                <asp:TextBox ID="txtStartDateOfOrder" runat="server"></asp:TextBox>
            </td>
            <td>
                To
                <asp:TextBox ID="txtEndDateOfOrder" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnView" runat="server" onclick="Button1_Click" Text="View" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvPurchaseOrder" runat="server" AutoGenerateColumns="False" 
        onselectedindexchanged="gvPurchaseOrder_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="PONumber" HeaderText="PO Number" />
            <asp:CommandField ShowSelectButton="True" />
            <asp:HyperLinkField DataTextField="PONumber" HeaderText="PO Number" />
        </Columns>
    </asp:GridView>
    </asp:Content>
