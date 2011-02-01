<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationeryRequest.aspx.cs" Inherits="SA33.Team12.SSIS.Test.StationeryRequest" %>
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
        .style6
        {
            width: 104px;
            height: 36px;
        }
        .style7
        {
            width: 117px;
            height: 36px;
            font-size: small;
        }
        .style8
        {
            width: 69px;
            height: 36px;
            font-size: small;
        }
        .style9
        {
            width: 427px;
            height: 36px;
        }
        .style10
        {
            height: 36px;
        }
        .style11
        {
            width: 104px;
            height: 2px;
        }
        .style12
        {
            width: 117px;
            height: 2px;
            font-size: small;
        }
        .style13
        {
            width: 69px;
            height: 2px;
            font-size: small;
        }
        .style14
        {
            width: 427px;
            height: 2px;
        }
        .style15
        {
            height: 2px;
        }
        .style16
        {
            width: 80px;
        }
        .style17
        {
            width: 762px;
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
                <td>
                    <asp:Label ID="lblAdd" runat="server" Text="Action"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                        DataSourceID="ObjectDataSource2" DataTextField="Name" 
                        DataValueField="CategoryID">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
                        SelectMethod="GetAllCategories" TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                    </asp:ObjectDataSource>
                    </td>
                <td class="style7">
        <asp:DropDownList ID="ddlStationeryID" runat="server" Height="21px" 
                        style="margin-left: 0px" Width="250px" DataSourceID="ObjectDataSource1" 
                        DataTextField="Description" DataValueField="StationeryID">
        </asp:DropDownList>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                        SelectMethod="GetStationeriesByCategory" 
                        TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownList1" Name="CategoryID" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
                <td class="style8">
                    <asp:TextBox ID="txtQuantity" runat="server" Width="71px"></asp:TextBox>
                </td>
                <td class="style10">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                </td>
            </tr>
            <tr>
                <td class="style11">
                    </td>
                <td class="style12">
                    <asp:Literal ID="ltDescription" runat="server"></asp:Literal>
                </td>
                <td class="style13">
                    <asp:Literal ID="ltQuantity" runat="server"></asp:Literal>
                </td>
                <td class="style15">
                </td>
            </tr>
        </table>
    </div>
        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Horizontal" AutoGenerateDeleteButton="True" 
        onrowdeleting="GridView1_RowDeleting" Width="918px">
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
    <div>
        <table style="width:100%;">
            <tr>
                <td class="style17">
                    &nbsp;</td>
                <td class="style16">
                    <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                        style="margin-left: 50px; margin-right: 3px" Text="Submit" />
                </td>
                <td>
                    <asp:Button ID="btnBack" runat="server" Text="Back" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    </asp:Content>
