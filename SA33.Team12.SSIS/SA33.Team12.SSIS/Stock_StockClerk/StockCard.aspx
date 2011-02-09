<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="StockCard.aspx.cs" Inherits="SA33.Team12.SSIS.Stock_StoreClerk.StockCard" %>

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
        .style3
        {
            margin-left: 361px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Stock Card</h1>
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
                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>Stock Information</legend>
        <asp:DetailsView ID="dvStockCard" runat="server" Height="50px" Width="273px" AutoGenerateRows="False">
            <Fields>
                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                <asp:BoundField DataField="Description" HeaderText="Item Description" />
                <asp:TemplateField HeaderText="Bin#:">
                    <ItemTemplate>
                        <%# ((Location)Eval("Location")).Name %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UnitOfMeasure" HeaderText="UOM" />
            </Fields>
        </asp:DetailsView>
    </fieldset>
    <fieldset>
        <legend>Transaction History</legend>
        <asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Reason" HeaderText="Reason" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="Balance" HeaderText="Balance" />
            </Columns>
        </asp:GridView>
    </fieldset>
    <div>
        <asp:Button ID="btnPrint" runat="server" CssClass="style3" Enabled="True"
            Text="Print" OnClientClick="window.print();" />
    </div>
</asp:Content>
