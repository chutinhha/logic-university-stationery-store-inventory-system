<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="SA33.Team12.SSIS.Test.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Find by criteria test</h2>

    First Name:
    <asp:TextBox ID="FirstNameTextBox" runat="server"></asp:TextBox>
    <br />
    <br />
    Last Name:
    <asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="SearchButton" runat="server" onclick="SearchButton_Click" 
        Text="Search" />

&nbsp;<asp:Button ID="ShowAllButton" runat="server" onclick="ShowAllButton_Click" 
        Text="Show All" />

<asp:GridView runat="server" ID="UserGridView">
</asp:GridView>


    <br />
    <br />


</asp:Content>
