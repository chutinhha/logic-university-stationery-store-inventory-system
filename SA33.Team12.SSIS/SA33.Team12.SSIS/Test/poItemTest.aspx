<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="poItemTest.aspx.cs" Inherits="SA33.Team12.SSIS.Test.poItemTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Enter PO ID: "></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
     <asp:Button ID="Button1" runat="server" Text="FindPOItem" 
        onclick="Button1_Click" />
        <br />
    <asp:Label ID="Label2" runat="server" Text="Enter PO Item ID: "></asp:Label>
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    <asp:Button ID="Button2" runat="server" Text="UpdatePOItemByID" 
        onclick="Button2_Click" />
        <br />
    <asp:Button ID="ButtonAddNew" runat="server" Text="Add New PO Item" 
        onclick="ButtonAddNew_Click" />
    <asp:Button ID="ButtonDelete" runat="server" Text="Delete PO Item By ID" 
        onclick="ButtonDelete_Click" />
    
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
 

</asp:Content>
