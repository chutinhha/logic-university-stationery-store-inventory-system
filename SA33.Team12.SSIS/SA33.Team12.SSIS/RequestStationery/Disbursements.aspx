<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Disbursements.aspx.cs" Inherits="SA33.Team12.SSIS.RequestStationery.Disbursements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Disbursements</h2>

<fieldset>
<legend>Maintain Disbursements</legend>

<asp:GridView runat="server" ID="DisbursementGridView">
</asp:GridView>

</fieldset>

</asp:Content>
