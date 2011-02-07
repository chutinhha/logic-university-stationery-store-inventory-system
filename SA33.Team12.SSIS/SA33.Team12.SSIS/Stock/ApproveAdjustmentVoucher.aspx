<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApproveAdjustmentVoucher.aspx.cs" Inherits="SA33.Team12.SSIS.Stock.ApproveAdjustmentVoucher" %>

<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1>Approve Adjustment Voucher</h1>
<fieldset>
<legend>Pending Adjustment Vouchers</legend>
    <asp:GridView ID="gvAdjustments" runat="server" AutoGenerateColumns="False" 
        onrowdatabound="gvAdjustments_RowDataBound" 
        onrowcommand="gvAdjustments_RowCommand">
        <Columns>
        <asp:TemplateField HeaderText="Voucher Number" SortExpression="AdjustmentVoucherTransactionID">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="AdjustmentVoucherTransactionID" CommandArgument='<%# Eval("AdjustmentVoucherTransactionID") %>'
                            Text='<%# Eval("VoucherNumber") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AdjustmentVoucherTransactionID") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
            <asp:BoundField DataField="DateIssued" HeaderText="Date Issued" 
                SortExpression="DateIssued" />
            <asp:TemplateField HeaderText="Created By">
                <ItemTemplate>             
                    <asp:Literal ID="CreatedByLiteral" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Approve" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                            CommandName="Approve" CommandArgument='<%# Eval("AdjustmentVoucherTransactionID") %>' Text="Approve"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
</fieldset>
    <div>
        <asp:Button ID="btnApproveAll" runat="server" Text="Approve All" 
            onclick="btnApproveAll_Click" />
    </div>
</asp:Content>
