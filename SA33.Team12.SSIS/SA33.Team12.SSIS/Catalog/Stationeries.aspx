<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Stationeries.aspx.cs" Inherits="SA33.Team12.SSIS.Catalog.Stationeries" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 109px;
        }
        .style2
        {
            width: 145px;
        }
        .style3
        {
            width: 182px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>stationeries</h2>
<fieldset>
    <fieldset>
    <legend>Add Stationery</legend>
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    Item Code</td>
                <td class="style2">
                    <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                </td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Description</td>
                <td class="style2">
                    <asp:TextBox ID="DescriptionTextBox" runat="server"></asp:TextBox>
                </td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Unit of Measure</td>
                <td class="style2">
                    <asp:TextBox ID="UOMTextBox" runat="server"></asp:TextBox>
                </td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Category</td>
                <td class="style2">
                    <asp:DropDownList ID="CategoryDDL" runat="server" DataSourceID="CategoryDS" 
                        DataTextField="Name" DataValueField="CategoryID">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="CategoryDS" runat="server" 
                        SelectMethod="GetAllCategories" TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                    </asp:ObjectDataSource>
                </td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Location</td>
                <td class="style2">
                    <asp:DropDownList ID="LocationDDL" runat="server" DataSourceID="locDS" 
                        DataTextField="Name" DataValueField="LocationID">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="locDS" runat="server" SelectMethod="GetAllLocations" 
                        TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                </td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Reorder Level</td>
                <td class="style2">
                    <asp:TextBox ID="ReorderLevelTextBox" runat="server"></asp:TextBox>
                </td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Reorder Quantity</td>
                <td class="style2">
                    <asp:TextBox ID="ReorderQtyTextBox" runat="server"></asp:TextBox>
                </td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Supplier</td>
                <td class="style2">
                    <asp:DropDownList ID="Supplier1DDL" runat="server" DataSourceID="supplierDS" 
                        DataTextField="CompanyName" DataValueField="SupplierID">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="supplierDS" runat="server" 
                        SelectMethod="GetAllSuppliers" TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                    </asp:ObjectDataSource>
                </td>
                <td class="style3">
                    <asp:DropDownList ID="Supplier2DDL" runat="server" DataSourceID="supplierDS" 
                        DataTextField="CompanyName" DataValueField="SupplierID">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="Supplier3DDL" runat="server" DataSourceID="supplierDS" 
                        DataTextField="CompanyName" DataValueField="SupplierID">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Price</td>
                <td class="style2">
                    <asp:TextBox ID="Price1TextBox" runat="server"></asp:TextBox>
                </td>
                <td class="style3">
                    <asp:TextBox ID="Price2TextBox" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="Price3TextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td class="style3">
                    <asp:Button ID="SubmitButton" runat="server" onclick="SubmitButton_Click" 
                        style="margin-left: 0px" Text="Add" />
                    <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
    </fieldset>
    <fieldset>
    <legend>Stationery List</legend>
    <asp:GridView runat="server" ID="StationeryGridView" AllowPaging="True"
        AutoGenerateColumns="False" DataSourceID="StationeryObjectDataSource"
        SelectedRowStyle-BackColor="LightGray" 
        onselectedindexchanged="SpecialStationeryGridView_SelectedIndexChanged" 
            DataKeyNames="StationeryID">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="StationeryID" 
                HeaderText="StationeryID" SortExpression="StationeryID" />
            <asp:TemplateField HeaderText="CategoryID" SortExpression="CategoryID">
                <EditItemTemplate>
                    <asp:DropDownList ID="CategoryDDL" runat="server" 
                        DataSourceID="ObjectDataSource1" DataTextField="Name" 
                        DataValueField="CategoryID">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                        SelectMethod="GetAllCategories" TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                    </asp:ObjectDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                  <%# ((SA33.Team12.SSIS.DAL.Category) Eval("Category")).Name %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="LocationID" SortExpression="LocationID">
                <EditItemTemplate>
                    <asp:DropDownList ID="LocationDDL" runat="server" DataSourceID="LocationDS" 
                        DataTextField="Name" DataValueField="LocationID">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="LocationDS" runat="server" 
                        SelectMethod="GetAllLocations" TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                    </asp:ObjectDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <%# ((SA33.Team12.SSIS.DAL.Location) Eval("Location")).Name %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" 
                SortExpression="ItemCode" />
            <asp:BoundField DataField="Description" HeaderText="Description" 
                SortExpression="Description" />
            <asp:BoundField DataField="UnitOfMeasure" HeaderText="UnitOfMeasure" 
                SortExpression="UnitOfMeasure" />
            <asp:BoundField DataField="ReorderLevel" HeaderText="ReorderLevel" 
                SortExpression="ReorderLevel" />
            <asp:BoundField DataField="ReorderQuantity" HeaderText="ReorderQuantity" 
                SortExpression="ReorderQuantity" />
            <asp:BoundField DataField="QuantityInHand" HeaderText="QuantityInHand" 
                SortExpression="QuantityInHand" />
            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" 
                SortExpression="DateCreated" />
            <asp:BoundField DataField="DateModified" HeaderText="DateModified" 
                SortExpression="DateModified" />
            <asp:TemplateField HeaderText="CreatedBy" SortExpression="CreatedBy">
                <ItemTemplate>
                    <%# ((SA33.Team12.SSIS.DAL.User) Eval("CreatedByUser")).UserName %>
                                    </ItemTemplate>
     
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ModifiedBy" SortExpression="ModifiedBy">
                <ItemTemplate>
                     <%# ((SA33.Team12.SSIS.DAL.User) Eval("ModifiedByUser")).UserName %>
                </ItemTemplate>
            
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ApprovedBy" SortExpression="ApprovedBy">
                <ItemTemplate>
                     <%# ((SA33.Team12.SSIS.DAL.User)Eval("ApprovedByUser")) == null ? "" : ((SA33.Team12.SSIS.DAL.User)Eval("ApprovedByUser")).UserName %>
                </ItemTemplate>
             
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsApproved" HeaderText="IsApproved" 
                SortExpression="IsApproved" />
            <asp:BoundField DataField="DateApproved" HeaderText="DateApproved" 
                SortExpression="DateApproved" />
        </Columns>

<SelectedRowStyle BackColor="LightGray"></SelectedRowStyle>
    </asp:GridView>

    <asp:ObjectDataSource ID="StationeryObjectDataSource" runat="server" 
        DataObjectTypeName="SA33.Team12.SSIS.DAL.Stationery" DeleteMethod="DeleteStationery" 
        InsertMethod="CreateStationery" SelectMethod="GetAllStationeries" 
        TypeName="SA33.Team12.SSIS.BLL.CatalogManager" 
        UpdateMethod="UpdateStationery" 
            OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>


    <asp:DynamicDataManager ID="DynamicDataManager" runat="server" />

    <asp:ValidationSummary runat="server" DisplayMode="BulletList" 
        CssClass="failureNotification" />
    </fieldset>
    </fieldset>
</asp:Content>
