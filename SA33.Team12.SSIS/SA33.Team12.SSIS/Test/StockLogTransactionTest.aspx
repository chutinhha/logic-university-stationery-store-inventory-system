<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockLogTransactionTest.aspx.cs" Inherits="SA33.Team12.SSIS.Test.StockLogTransactionTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    Criteria: <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    &nbsp;<asp:Button ID="ButtonFind" runat="server" Text="Find" 
        onclick="ButtonFind_Click" />
        <asp:RadioButton ID="rbnStockLogID" runat="server" />
    <asp:Label ID="Label2" runat="server" Text="StockLogID"></asp:Label>
    <asp:RadioButton ID="rbnAdjVoucherTran" runat="server" />
    <asp:Label ID="Label3" runat="server" Text="VoucherID"></asp:Label>
    <asp:RadioButton ID="rbnStationeryID" runat="server" />
    <asp:Label ID="Label6" runat="server" Text="StationeryID"></asp:Label>
    <asp:RadioButton ID="rbnType" runat="server" />
    <asp:Label ID="Label4" runat="server" Text="Type"></asp:Label>
    <asp:RadioButton ID="rbnReason" runat="server" />
    <asp:Label ID="Label5" runat="server" Text="Reason"></asp:Label>
    <asp:RadioButton ID="rbnQty" runat="server" />
    <asp:Label ID="Label7" runat="server" Text="Qty"></asp:Label>
    <asp:RadioButton ID="rbnBal" runat="server" />
    <asp:Label ID="Label8" runat="server" Text="Bal"></asp:Label>
        <br/>
        <br />
        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Horizontal">
        <AlternatingRowStyle BackColor="#F7F7F7" />
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <SortedAscendingCellStyle BackColor="#F4F4FD" />
        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
        <SortedDescendingCellStyle BackColor="#D8D8F0" />
        <SortedDescendingHeaderStyle BackColor="#3E3277" />
    </asp:GridView>
    <br />
    <br />
    </asp:Content>
