<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AdjustmentVoucherDetail.aspx.cs" Inherits="SA33.Team12.SSIS.Stock_StockSupervisor_Manager.AdjustmentVoucherDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"> 
    <style type="text/css">
        .style1
        {
            width: 168px;
        }
        .style2
        {
            width: 71px;
        }
        .style3
        {
            width: 252px;
        }
        .style4
        {
            width: 660px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Adjustment Voucher Information</legend>
        <table style="width: 100%;">
            <tr>
                <td class="style1">
                    Voucher Number:
                </td>
                <td class="style3">
                    <asp:Label ID="lblVoucherNumber" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="style2">
                    Date Issued:
                </td>
                <td>
                    <asp:Label ID="lblIssueDate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Created By:
                </td>
                <td class="style3">
                    <asp:Label ID="lblCreatedBy" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="style2">
                    Total Cost:
                </td>
                <td>
                    <asp:Label ID="lblCost" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>Adjustment Items</legend>
        <asp:GridView ID="gvAdjustmentItems" runat="server" AutoGenerateColumns="False" DataKeyNames="StationeryID"
            OnRowDataBound="gvAdjustmentItems_RowDataBound">
            <Columns>
                <%--   <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                       <%# ((Stationery )Eval("Stationery")).Description %>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Item Description">
                    <ItemTemplate>
                        <asp:Literal ID="ltlDescription" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Reason" HeaderText="Reason" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                <asp:BoundField DataField="Balance" HeaderText="Balance" SortExpression="Balance" />
            </Columns>
        </asp:GridView>
    </fieldset>
    
</asp:Content>
