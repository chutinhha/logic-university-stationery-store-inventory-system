<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InventoryStatus.aspx.cs" Inherits="SA33.Team12.SSIS.Print.Store.InventoryStatus1" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
    AutoDataBind="true" ToolPanelView="None" />

</asp:Content>
