<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PlacePurchaseOrder.aspx.cs" Inherits="SA33.Team12.SSIS.Stock.PlacePurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 129px;
        }
        .style2
        {
            width: 118px;
        }
        .style4
        {
            width: 225px;
        }
        .style5
        {
            width: 175px;
        }
        .style7
        {
            width: 396px;
        }
        .style8
        {
            width: 324px;
        }
        .style9
        {
            width: 330px;
        }
        .style10
        {
            width: 141px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1> Stationery Purchase Order</h1>
    <fieldset>

    <legend> Information </legend>
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                Created By:
            </td>
            <td class="style5">
                <asp:Label ID="lblCreatedBy" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style10">
                Attention To:</td>
            <td>
                <asp:DropDownList ID="ddlAttentionTo" runat="server" 
                    DataSourceID="UserDataSource" DataTextField="UserName" DataValueField="UserID">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="UserDataSource" runat="server" 
                    SelectMethod="GetAllUsers" TypeName="SA33.Team12.SSIS.BLL.UserManager">
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Date:</td>
            <td class="style5">
                <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style10">
                Supply By</td>
            <td>
                <asp:TextBox ID="txtDateToSupply" runat="server"></asp:TextBox>
&nbsp;(dd/mm/yyyy)</td>
        </tr>
    </table>
    </fieldset>
    <fieldset> 
    <legend> Add New Purchase Order Item</legend>
    <table style="width: 100%;">
        <tr>
            <td class="style2">
                Category
            </td>
            <td class="style9">
                Item Description 
            </td>
            <td class="style4">
                &nbsp; Order Quantity
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:DropDownList ID="ddlCategory" runat="server" DataSourceID="CategoryDataSource"
                    DataTextField="Name" DataValueField="CategoryID" 
                    AutoPostBack="True">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="CategoryDataSource" runat="server" 
                    SelectMethod="GetAllCategories" TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                </asp:ObjectDataSource>
            </td>
            <td class="style9">
                &nbsp;<asp:DropDownList ID="ddlDescription" runat="server" 
                    DataSourceID="ObjectDataSource4" DataTextField="Description" 
                    DataValueField="StationeryID">
                </asp:DropDownList>
            </td>
            <td class="style4">
                &nbsp;
                <asp:TextBox ID="txtOrderQuantity" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
            </td>
        </tr>
        </table>
    </fieldset>
    
    <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" 
        SelectMethod="GetStationeriesByCategory" 
        TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCategory" Name="CategoryID" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <fieldset>
    <legend>Items To Order</legend>
    <asp:GridView ID="gvPOItems" runat="server" AutoGenerateColumns="False" 
            onrowdatabound="gvPOItems_RowDataBound" DataKeyNames="StationeryID">
        <Columns>
            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
            <asp:BoundField DataField="Description" HeaderText="Item Description" />
            <asp:BoundField DataField="QuantityInHand" HeaderText="Current Stock" />
            <asp:BoundField DataField="ReorderLevel" HeaderText="Reorder Level" 
                ReadOnly="True" SortExpression="ReorderLevel" />
            <asp:BoundField DataField="ReorderQuantity" HeaderText="Reorder Quantity" 
                ReadOnly="True" SortExpression="ReorderQuantity" />
            <asp:TemplateField HeaderText="Supplier">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlSupplier" runat="server" 
                        DataSourceID="SupplierDataSource" DataTextField="CompanyName" 
                        DataValueField="SupplierID">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="SupplierDataSource" runat="server" 
                        SelectMethod="GetAllSuppliers" TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                    </asp:ObjectDataSource>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Recommended Reorder Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="txtRecommend" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </fieldset>
    
    <table style="width: 100%;">
        <tr>
            <td class="style8">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" 
                    Text="Submit" Height="44px" Width="98px" />
            </td>
        </tr>
        </table>
</asp:Content>
