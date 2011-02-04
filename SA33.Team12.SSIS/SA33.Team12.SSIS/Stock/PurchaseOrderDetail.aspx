﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PurchaseOrderDetail.aspx.cs" Inherits="SA33.Team12.SSIS.Stock.PurchaseOrderDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 203px;
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
                <td>
                    <asp:Label ID="lblPONumber" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Order Date
                </td>
                <td>
                    <asp:Label ID="lblOrderDate" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Supply By
                </td>
                <td>
                    <asp:Label ID="lblDateToSupply" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    &nbsp;
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
</asp:Content>
