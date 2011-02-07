<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Stationeries.aspx.cs" Inherits="SA33.Team12.SSIS.Catalog.Stationeries" %>

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
    <h2>
        stationeries</h2>
    <fieldset>
        <fieldset>
            <legend>Add Stationery</legend>
            <table style="width: 100%;">
                <tr>
                    <td class="style1">
                        Item Code
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="NameTextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NameTextBox"
                            Display="Dynamic" ErrorMessage="Item Code is required" ValidationGroup="input"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Description
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="DescriptionTextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DescriptionTextBox"
                            ErrorMessage="Description is Required" ValidationGroup="input"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Unit of Measure
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="UOMTextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="UOMTextBox"
                            ErrorMessage="UOM is required" ValidationGroup="input"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Category
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="CategoryDDL" runat="server" DataSourceID="CategoryDS" DataTextField="Name"
                            DataValueField="CategoryID" ValidationGroup="input">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="CategoryDS" runat="server" SelectMethod="GetAllCategories"
                            TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Location
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="LocationDDL" runat="server" DataSourceID="locDS" DataTextField="Name"
                            DataValueField="LocationID" ValidationGroup="input">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="locDS" runat="server" SelectMethod="GetAllLocations" TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                        </asp:ObjectDataSource>
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Reorder Level
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="ReorderLevelTextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ReorderLevelTextBox"
                            Display="Dynamic" ErrorMessage="Re Order Level is required" ValidationGroup="input"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ReorderLevelTextBox"
                            Display="Dynamic" ErrorMessage="Invalid Number" MaximumValue="10000" MinimumValue="1"
                            Type="Integer" ValidationGroup="input"></asp:RangeValidator>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Reorder Quantity
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="ReorderQtyTextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ReorderQtyTextBox"
                            Display="Dynamic" ErrorMessage="Reorder Quantity is required" ValidationGroup="input"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ReorderQtyTextBox"
                            Display="Dynamic" ErrorMessage="Invalid Number" MaximumValue="100000" MinimumValue="1"
                            Type="Integer" ValidationGroup="input"></asp:RangeValidator>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Supplier
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="Supplier1DDL" runat="server" DataSourceID="supplierDS" DataTextField="CompanyName"
                            DataValueField="SupplierID" ValidationGroup="input">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="supplierDS" runat="server" SelectMethod="GetAllSuppliers"
                            TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                    </td>
                    <td class="style3">
                        <asp:DropDownList ID="Supplier2DDL" runat="server" DataSourceID="supplierDS" DataTextField="CompanyName"
                            DataValueField="SupplierID" ValidationGroup="input">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="Supplier3DDL" runat="server" DataSourceID="supplierDS" DataTextField="CompanyName"
                            DataValueField="SupplierID" ValidationGroup="input">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Price
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="Price1TextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Price1TextBox"
                            Display="Dynamic" ErrorMessage="Price is required" ValidationGroup="input"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="Price1TextBox"
                            Display="Dynamic" ErrorMessage="Invalid Number" MaximumValue="1000000" MinimumValue=".01"
                            Type="Double" ValidationGroup="input"></asp:RangeValidator>
                    </td>
                    <td class="style3">
                        <asp:TextBox ID="Price2TextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Price2TextBox"
                            Display="Dynamic" ErrorMessage="Price is required" ValidationGroup="input"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="Price2TextBox"
                            Display="Dynamic" ErrorMessage="Invalid Number" MaximumValue="1000000" MinimumValue=".01"
                            Type="Double" ValidationGroup="input"></asp:RangeValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="Price3TextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="Price3TextBox"
                            Display="Dynamic" ErrorMessage="Price is required" ValidationGroup="input"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="Price3TextBox"
                            Display="Dynamic" ErrorMessage="Invalid Number" MaximumValue="1000000" MinimumValue=".01"
                            Type="Double" ValidationGroup="input"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td class="style2">
                        &nbsp;
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="SubmitButton" runat="server" OnClick="SubmitButton_Click" Style="margin-left: 0px"
                            Text="Add" ValidationGroup="input" />
                        <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>Stationery List</legend>
            <asp:GridView runat="server" ID="StationeryGridView" AllowPaging="True" AutoGenerateColumns="False"
                DataSourceID="StationeryObjectDataSource" SelectedRowStyle-BackColor="LightGray"
                OnSelectedIndexChanged="SpecialStationeryGridView_SelectedIndexChanged" DataKeyNames="StationeryID">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
                    <asp:BoundField DataField="StationeryID" HeaderText="StationeryID" SortExpression="StationeryID" />
                    <asp:TemplateField HeaderText="CategoryID" SortExpression="CategoryID">
                        <EditItemTemplate>
                            <asp:DropDownList ID="CategoryDDL" runat="server" DataSourceID="ObjectDataSource1"
                                DataTextField="Name" DataValueField="CategoryID" 
                                SelectedValue='<%# Bind("CategoryID") %>'>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllCategories"
                                TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%# ((SA33.Team12.SSIS.DAL.Category) Eval("Category")).Name %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LocationID" SortExpression="LocationID">
                        <EditItemTemplate>
                            <asp:DropDownList ID="LocationDDL" runat="server" DataSourceID="LocationDS" DataTextField="Name"
                                DataValueField="LocationID" SelectedValue='<%# Bind("LocationID") %>'>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="LocationDS" runat="server" SelectMethod="GetAllLocations"
                                TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%# ((SA33.Team12.SSIS.DAL.Location) Eval("Location")).Name %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" SortExpression="ItemCode" />
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                    <asp:BoundField DataField="UnitOfMeasure" HeaderText="UnitOfMeasure" SortExpression="UnitOfMeasure" />
                    <asp:BoundField DataField="ReorderLevel" HeaderText="ReorderLevel" SortExpression="ReorderLevel" />
                    <asp:BoundField DataField="ReorderQuantity" HeaderText="ReorderQuantity" SortExpression="ReorderQuantity" />
                    <asp:BoundField DataField="QuantityInHand" HeaderText="QuantityInHand" SortExpression="QuantityInHand" />
                    <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
                    <asp:BoundField DataField="DateModified" HeaderText="DateModified" SortExpression="DateModified" />
                    <asp:TemplateField HeaderText="CreatedBy" SortExpression="CreatedBy">
                        <ItemTemplate>
                            <%# ((SA33.Team12.SSIS.DAL.User) Eval("CreatedByUser")).UserName %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <%# Eval("CreatedBy") %>
                        </EditItemTemplate>
                    </asp:TemplateField>
                            
                    <asp:CheckBoxField DataField="IsApproved" HeaderText="IsApproved" SortExpression="IsApproved" />
                    <asp:BoundField DataField="DateApproved" HeaderText="DateApproved" SortExpression="DateApproved" />
                    <asp:TemplateField HeaderText="ModifiedBy">
                        <EditItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("ModifiedBy")%>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%# Eval("ModifiedBy") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <SelectedRowStyle BackColor="LightGray"></SelectedRowStyle>
            </asp:GridView>
            <asp:ObjectDataSource ID="StationeryObjectDataSource" runat="server" DataObjectTypeName="SA33.Team12.SSIS.DAL.Stationery"
                DeleteMethod="DeleteStationery" InsertMethod="CreateStationery" SelectMethod="GetAllStationeries"
                TypeName="SA33.Team12.SSIS.BLL.CatalogManager" UpdateMethod="UpdateStationery"
                OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            <asp:DynamicDataManager ID="DynamicDataManager" runat="server" />
            <asp:ValidationSummary runat="server" DisplayMode="BulletList" CssClass="failureNotification" />
        </fieldset>
        <fieldset>
            <legend>Stationery Price</legend>
            <asp:GridView ID="StationeryPriceGridView" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                DataSourceID="StationeryPriceDS" DataKeyNames="StationeryPriceID" OnRowEditing="StationeryPriceGridView_RowEditing"
                OnRowUpdating="StationeryPriceGridView_RowUpdating">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="StationeryPriceID" HeaderText="StationeryPriceID" SortExpression="StationeryPriceID" />
                    <asp:BoundField DataField="StationeryID" HeaderText="StationeryID" SortExpression="StationeryID" />
                    <asp:TemplateField HeaderText="SupplierID" SortExpression="SupplierID">
                        <EditItemTemplate>
                            <asp:DropDownList ID="SupplierDDL" runat="server" DataSourceID="supplierDS1" DataTextField="CompanyName"
                                DataValueField="SupplierID" SelectedValue='<%# Bind("SupplierID") %>'>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="supplierDS1" runat="server" SelectMethod="GetAllSuppliers"
                                TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%# ((SA33.Team12.SSIS.DAL.Supplier) Eval("Supplier")).CompanyName %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="StationeryPriceDS" runat="server" SelectMethod="GetStationeryPricesByStationeryID"
                TypeName="SA33.Team12.SSIS.BLL.CatalogManager" DataObjectTypeName="SA33.Team12.SSIS.DAL.StationeryPrice"
                DeleteMethod="DeleteStationeryPrice" InsertMethod="CreateStationeryPrice" OldValuesParameterFormatString="original_{0}"
                UpdateMethod="UpdateStationeryPrice">
                <SelectParameters>
                    <asp:ControlParameter ControlID="StationeryGridView" Name="stationeryID" PropertyName="SelectedValue"
                        Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </fieldset>
    </fieldset>
</asp:Content>
