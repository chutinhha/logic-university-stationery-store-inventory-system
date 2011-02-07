<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ApplyAdjustmentVoucher.aspx.cs" Inherits="SA33.Team12.SSIS.Stock.AdjustmentVoucher" %>

<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 165px;
        }
        .style2
        {
            width: 301px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Apply Adjustment Voucher</h1>
    <fieldset>
        <legend>Select Stationery </legend>
        <table style="width: 100%;">
            <tr>
                <td class="style1">
                    Category
                </td>
                <td class="style2">
                    Item Description
                </td>
                <td>
                    Type</td>
                <td>
                    Quantity</td>
                <td>
                    Reason:
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:DropDownList ID="ddlCategory" runat="server" DataSourceID="CategoryDataSource"
                        DataTextField="Name" DataValueField="CategoryID" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="CategoryDataSource" runat="server" SelectMethod="GetAllCategories"
                        TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                </td>
                <td class="style2">
                    <asp:DropDownList ID="ddlDescription" runat="server" DataSourceID="ObjectDataSource4"
                        DataTextField="Description" DataValueField="StationeryID">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="GetStationeriesByCategory"
                        TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlCategory" Name="CategoryID" PropertyName="SelectedValue"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server">
                        <asp:ListItem Value="0">ADJ -</asp:ListItem>
                        <asp:ListItem Value="1">ADJ +</asp:ListItem>
                        <asp:ListItem Value="2">Consumed</asp:ListItem>
                        <asp:ListItem Value="3">Replenished</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtQuantity" runat="server" Width="51px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtReason" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>Adjustment Items</legend>
        <asp:GridView ID="gvAdjustmentItems" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="StationeryID" 
            onrowdatabound="gvAdjustmentItems_RowDataBound">
            <Columns>
                <%--<asp:TemplateField HeaderText="Item Description">
                    <ItemTemplate>
                        <%# ((Stationery )Eval("Stationery")).Description %>
                    </ItemTemplate>
                </asp:TemplateField>--%>
             <%--   <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                       <%# ((Stationery )Eval("Stationery")).Description %>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="StationeryID" HeaderText="Stationery ID" 
                    SortExpression="StationeryID" />
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Literal ID="ltlDescription" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Reason" HeaderText="Reason" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" 
                    SortExpression="Quantity" />
                <asp:BoundField DataField="Balance" HeaderText="Balance" 
                    SortExpression="Balance" />
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
            onclick="btnSubmit_Click" />
    </fieldset>
</asp:Content>
