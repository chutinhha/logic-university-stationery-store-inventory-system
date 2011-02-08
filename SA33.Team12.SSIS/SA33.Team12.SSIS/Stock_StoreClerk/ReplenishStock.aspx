<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ReplenishStock.aspx.cs" Inherits="SA33.Team12.SSIS.Stock_StoreClerk.ReplenishStock" %>
    <%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Information</legend>
        <table style="width: 100%;">
            <tr>
                <td>
                    &nbsp;
                    PO Number:</td>
                <td>
                    &nbsp;
                    <asp:Label ID="lblPONumber" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    Supplier:</td>
                <td>
                    <asp:Label ID="lblSupplier" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    Order Date:</td>
                <td>
                    &nbsp;
                    <asp:Label ID="lblOrderDate" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    Received Date:</td>
                <td>
                    <asp:TextBox ID="txtReceivedDate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    DO Number:</td>
                <td>
                    &nbsp;
                    <asp:TextBox ID="txtDONumber" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                    Received By:</td>
                <td>
                    <asp:Label ID="lblReceivedBy" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>Purchase Order Items</legend>
        <asp:GridView ID="gvPOitems" runat="server" AutoGenerateColumns="False">
            <Columns>
               <asp:TemplateField HeaderText="Item Description" 
                SortExpression="StationeryID">
                <ItemTemplate>
                    <%# ((Stationery) Eval("Stationery")).Description %>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="QuantityToOrder" HeaderText="Order Quantity" />
                <asp:BoundField DataField="QuantityToOrder" HeaderText="Delivery Quantity" />
            </Columns>
        </asp:GridView>
    </fieldset>
    <fieldset>
        <table style="width: 100%;">
            <tr>
                <td>
                    &nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="Back" />
                </td>
                <td>
                    &nbsp;
                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" 
                        onclick="btnConfirm_Click" />
                </td>
                <td>
                    &nbsp;
                    <asp:Button ID="btnAdjust" runat="server" Text="Adjust Inventory" 
                        onclick="btnAdjust_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
