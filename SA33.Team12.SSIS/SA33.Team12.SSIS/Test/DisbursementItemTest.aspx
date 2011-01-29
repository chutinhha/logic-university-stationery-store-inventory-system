<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisbursementItemTest.aspx.cs" Inherits="SA33.Team12.SSIS.Test.DisbursementItemTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"><br/>
    <asp:TextBox ID="txbDisbursementItemID" runat="server"></asp:TextBox>
    <asp:Button ID="btnGetDisbursementItemByID"
        runat="server" Text="Search" onclick="btnGetDisbursementItemByID_Click" />
    <h3>Search DisbursementItem By Criteria</h3><br/>
    stationeryID:<asp:TextBox ID="txbStationeryID" runat="server"></asp:TextBox><br/>
    <asp:Button ID="btnGetItemsByID" runat="server" Text="Search" 
        onclick="btnGetItemsByID_Click" />
    <h3>Update/Delete Item by ID</h3><br/>
    QuantityDisbursed:<asp:TextBox ID="txbQuantity" runat="server"></asp:TextBox>
    <asp:Button ID="btnUpdateItem" runat="server" Text="Update" 
         OnClick="btnUpdateItem_Click"/>
    <h3>Create Disbursement Items</h3><br/>
    <asp:Button ID="btnCreate" runat="server" Text="Create" 
        onclick="btnCreate_Click" />
    <h3>delete Item just created</h3><br/>
    ID:<asp:TextBox ID="txbIDForDelete" runat="server"></asp:TextBox>
    <asp:Button ID="btnDelete" runat="server" Text="Delete" 
        onclick="btnDelete_Click" />
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
</asp:Content>
