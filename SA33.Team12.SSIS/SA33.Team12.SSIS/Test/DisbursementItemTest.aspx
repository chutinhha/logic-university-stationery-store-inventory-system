<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisbursementItemTest.aspx.cs" Inherits="SA33.Team12.SSIS.Test.DisbursementItemTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"><br/>
    <asp:TextBox ID="txbDisbursementItemID" runat="server"></asp:TextBox>
    <asp:Button ID="btnGetDisbursementItemByID"
        runat="server" Text="Search" onclick="btnGetDisbursementItemByID_Click" />
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
</asp:Content>
