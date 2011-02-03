<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestMichaelDetail.aspx.cs" Inherits="SA33.Team12.SSIS.Test.TestMichaelDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Detail Page</h2>

<fieldset>
<legend>Detail</legend>

    <asp:GridView runat="server" ID="Detail" AutoGenerateColumns="true" />
</fieldset>

</asp:Content>
