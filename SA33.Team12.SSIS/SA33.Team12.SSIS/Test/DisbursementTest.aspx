<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisbursementTest.aspx.cs" Inherits="SA33.Team12.SSIS.Test.DisbursementTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"><br/>
    <h1>Disbursement List</h1><br/>
    <h3>Search Disbursement By ID</h3><br/>
    <asp:TextBox ID="tbxDisbursementID" runat="server"></asp:TextBox>
    <asp:Button ID="GetDisbursementByID"
        runat="server" Text="Search" onclick="btnGetDisbursementByID_Click" /><br/>
    <h3>Search Disbursement By Criteria</h3><br/>
    CreateBy:<asp:TextBox ID="txbCreateBy" runat="server"></asp:TextBox><br/>
    <asp:Button ID="GetDisbursementByCriteria" runat="server" Text="Search" 
        onclick="GetDisbursementByCriteria_Click" />
    <h3>Update Disbursement by ID</h3><br/>
    CreateBy:<asp:TextBox ID="txbCreateByforUpdate" runat="server"></asp:TextBox><br/>
    SRFID:<asp:TextBox ID="txbSRFID" runat="server"></asp:TextBox><br/>
    <asp:Button ID="btnUpdateDisbursement" runat="server" Text="Update" 
         OnClick="btnUpdateDisbursement_Click"/>
    <h3>Create Disbursement</h3><br/>
    <asp:Button ID="btnCreate" runat="server" Text="Create" 
        onclick="btnCreate_Click" />
    <h3>Delete Disbursement just created</h3><br/>
    DisbursementID:<asp:TextBox ID="tbxIDForDelete" runat="server"></asp:TextBox>
    <asp:Button ID="btnDeleteDisbursement" runat="server" Text="Delete" 
         OnClick="btnDeleteDisbursement_Click"/><br/>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
</asp:Content>
