<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ViewAdjustmentVoucher.aspx.cs" Inherits="SA33.Team12.SSIS.Stock_StoreSupervisor_Manager.ViewAdjustmentVoucher" %>

<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        View Adjustment Voucher</h1>
    <fieldset>
        <legend>Adjustment Vouchers</legend>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="AdjustmentVoucherDataSource"
            AllowPaging="True" onrowcommand="gvAdjustments_RowCommand">
            <Columns>
                <asp:BoundField DataField="AdjustmentVoucherID" HeaderText="AdjustmentVoucherID"
                    SortExpression="AdjustmentVoucherID" />
                <asp:TemplateField HeaderText="Voucher Number">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="AdjustmentVoucherDetail"
                            CommandArgument='<%# Eval("AdjustmentVoucherID") %>' Text='<%# Eval("VoucherNumber") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AdjustmentVoucherID") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CreatedBy">
                    <ItemTemplate>
                        <%# Convert.ToInt32(Eval("CreatedBy")) == 0 ?
    "": ((User) Eval("CreatedByUser")).UserName %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ApprovedBy">
                    <ItemTemplate>
                        <%# Convert.ToInt32(Eval("ApprovedBy")) == 0 ?
    "": ((User) Eval("ApprovedByUser")).UserName %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DateIssued" HeaderText="DateIssued" 
                    SortExpression="DateIssued" />
                <asp:BoundField DataField="DateApproved" HeaderText="DateApproved" SortExpression="DateApproved" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="AdjustmentVoucherDataSource" runat="server" SelectMethod="GetAllAdjustmentVoucher"
            TypeName="SA33.Team12.SSIS.BLL.AdjustmentVoucherManager"></asp:ObjectDataSource>
    </fieldset>
    <div>
    </div>
</asp:Content>
