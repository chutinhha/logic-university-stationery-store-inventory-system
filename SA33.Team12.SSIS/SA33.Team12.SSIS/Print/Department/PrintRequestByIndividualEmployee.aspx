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
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="RequisitionForm" HeaderText="RequisitionForm" 
                        SortExpression="RequisitionForm" />
                    <asp:BoundField DataField="DateRequested" HeaderText="DateRequested" 
                        SortExpression="DateRequested" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="UserName" HeaderText="UserName" 
                        SortExpression="UserName" />
                    <asp:BoundField DataField="WithSpecial" HeaderText="WithSpecial" 
                        ReadOnly="True" SortExpression="WithSpecial" />
                    <asp:BoundField DataField="Expense" HeaderText="Expense" ReadOnly="True" 
                        SortExpression="Expense" />
                </Columns>
            </asp:GridView>
        </fieldset>
    </fieldset>
</asp:Content>
