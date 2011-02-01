﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlacePurchaseOrder.aspx.cs" Inherits="SA33.Team12.SSIS.Stock.PlacePurchaseOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 129px;
        }
        .style2
        {
            width: 118px;
        }
        .style4
        {
            width: 225px;
        }
        .style5
        {
            width: 462px;
        }
        .style6
        {
            width: 182px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Stationery Purchase Order</h1>
    <table style="width:100%;">
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                Created By:
            </td>
            <td class="style5">
                <asp:Label ID="lblCreatedBy" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td class="style2">
                &nbsp;
                Category</td>
            <td class="style6">
                &nbsp;
                Item Description</td>
            <td class="style4">
                &nbsp;
                Order Quantity</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;
                <asp:DropDownList ID="ddlCategory" runat="server" 
                    DataSourceID="ObjectDataSource2" DataTextField="Name" 
                    DataValueField="CategoryID" 
                    onselectedindexchanged="ddlCategory_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td class="style6">
                &nbsp;
                <asp:DropDownList ID="ddlDescription" runat="server">
                </asp:DropDownList>
            </td>
            <td class="style4">
                &nbsp;
                <asp:TextBox ID="txtOrderQuantity" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnAdd" runat="server" Text="Add" />
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;
                </td>
            <td class="style6">
                &nbsp;
            </td>
            <td class="style4">
                &nbsp;
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetAllStationeries" 
        TypeName="SA33.Team12.SSIS.DAL.CatalogDAO">
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
        SelectMethod="GetAllCategories" TypeName="SA33.Team12.SSIS.DAL.CatalogDAO">
    </asp:ObjectDataSource>

    <asp:GridView ID="gvPOItems" runat="server" DataSourceID="ObjectDataSource1" 
        AutoGenerateColumns="False" AllowPaging="True">
        <Columns>
            <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" 
                SortExpression="ItemCode" />
            <asp:BoundField DataField="Description" HeaderText="Description" 
                SortExpression="Description" />
            <asp:BoundField DataField="ReorderLevel" HeaderText="ReorderLevel" 
                SortExpression="ReorderLevel" />
            <asp:BoundField DataField="ReorderQuantity" HeaderText="ReorderQuantity" 
                SortExpression="ReorderQuantity" />
            <asp:BoundField DataField="QuantityInHand" HeaderText="QuantityInHand" 
                SortExpression="QuantityInHand" />
            <asp:TemplateField HeaderText="Supplier"></asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
