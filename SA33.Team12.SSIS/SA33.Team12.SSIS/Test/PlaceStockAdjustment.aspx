<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlaceStockAdjustment.aspx.cs" Inherits="SA33.Team12.SSIS.Test.PlaceStockAdjustment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 104px;
        }
        .style2
        {
            width: 117px;
        }
        .style4
        {
            width: 427px;
        }
        .style5
        {
            width: 69px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    <asp:Label ID="lblCategoryID" runat="server" style="text-align: center" 
                        Text="Category"></asp:Label>
                </td>
                <td class="style2">
                    <asp:Label ID="lblStationeryID" runat="server" Text="Description"></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
                </td>
                <td class="style4">
                    <asp:Label ID="Reason" runat="server" Text="Reason"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblAdd" runat="server" Text="Action"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style2">
        <asp:DropDownList ID="ddlStationeryID" runat="server" DataSourceID="ObjectDataSource1" 
                        DataTextField="Description" DataValueField="StationeryID" Height="21px" 
                        style="margin-left: 0px" Width="250px">
        </asp:DropDownList>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                        SelectMethod="GetStationeryByID" TypeName="SA33.Team12.SSIS.DAL.CatalogDAO">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlCategoryID" Name="StationeryID" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
                <td class="style5">
                    <asp:TextBox ID="txtQuantity" runat="server" Width="71px"></asp:TextBox>
                </td>
                <td class="style4">
                    <asp:TextBox ID="TextBox1" runat="server" Width="421px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div>
        <asp:DropDownList ID="ddlCategoryID" runat="server" Height="30px" 
                onselectedindexchanged="ddlCategoryID_SelectedIndexChanged" 
                style="margin-left: 0px" Width="828px">
        </asp:DropDownList>
        </div>
    </div>
        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Horizontal">
        <AlternatingRowStyle BackColor="#F7F7F7" />
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <SortedAscendingCellStyle BackColor="#F4F4FD" />
        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
        <SortedDescendingCellStyle BackColor="#D8D8F0" />
        <SortedDescendingHeaderStyle BackColor="#3E3277" />
    </asp:GridView>
    <br />
    <br />
    </asp:Content>
