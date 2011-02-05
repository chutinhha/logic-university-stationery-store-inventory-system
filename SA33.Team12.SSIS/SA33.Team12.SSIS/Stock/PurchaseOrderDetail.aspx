﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PurchaseOrderDetail.aspx.cs" Inherits="SA33.Team12.SSIS.Stock.PurchaseOrderDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 203px;
        }
        .style2
        {
            width: 271px;
        }
        .style3
        {
            width: 118px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>PO Information</legend>
        <table style="width: 100%;">
            <tr>
                <td class="style1">
                    PO Number:
                </td>
                <td class="style2">
                    <asp:Label ID="lblPONumber" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="style3">
                    Order Date:</td>
                <td>
                    <asp:Label ID="lblOrderDate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Status:</td>
                <td class="style2">
                    <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="style3">
                    Supply By:</td>
                <td>
                    <asp:Label ID="lblDateToSupply" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            </table>
    </fieldset>
    <fieldset>
        <legend>PO Items</legend>
        <asp:GridView ID="gvPODetails" runat="server">
            <Columns>
                <asp:BoundField DataField="StationeryID" HeaderText="Stationery ID" SortExpression="StationeryID" />
                <asp:BoundField DataField="QuantityToOrder" HeaderText="Quantity" />
            </Columns>
        </asp:GridView>
    </fieldset>
    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp;
                <asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="Back" />
            </td>
            <td>
                &nbsp;
                <asp:Button ID="btnPrint" runat="server" Text="Print" />
            </td>
            <td>
                &nbsp;
                <asp:Button ID="btnReplenish" runat="server" Enabled="False" 
                    onclick="btnReplenish_Click" Text="Replenish" />
            </td>
        </tr>
    </table>
</asp:Content>
