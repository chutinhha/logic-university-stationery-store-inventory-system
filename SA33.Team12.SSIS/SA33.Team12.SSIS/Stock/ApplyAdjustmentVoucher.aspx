<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ApplyAdjustmentVoucher.aspx.cs" Inherits="SA33.Team12.SSIS.Stock.AdjustmentVoucher" %>

<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 254px;
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
                    Reason:
                </td>
                <td>
                    Quantity</td>
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
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Width="51px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>Adjustment Items</legend>
        <asp:GridView ID="gvAdjustmentItems" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Description" HeaderText="Item Description" 
                    SortExpression="Description" />
                <asp:TemplateField HeaderText="Reason">
                    <ItemTemplate>
                        <asp:Literal ID="ltlReason" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity"></asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </fieldset>
</asp:Content>
