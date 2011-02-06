<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PrintRequestByIndividualEmployee.aspx.cs" Inherits="SA33.Team12.SSIS.Print.PrintRequestByIndividualEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <h2>
            Request History By Individual Employee</h2>
        <fieldset>
            <legend>Result</legend>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </fieldset>
    </fieldset>
</asp:Content>
