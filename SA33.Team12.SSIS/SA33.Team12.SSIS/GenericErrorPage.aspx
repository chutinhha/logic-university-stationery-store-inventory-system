<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GenericErrorPage.aspx.cs" Inherits="SA33.Team12.SSIS.GenericErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Error occurred</h2>
<p>
    <asp:Literal runat="server"  ID="ErrorMessage" />
</p>

<p>
    Are you looking for a place to <asp:HyperLink runat="server" Text="login in" NavigateUrl="~/Account/Login.aspx" EnableViewState="false" />?
</p>
</asp:Content>
