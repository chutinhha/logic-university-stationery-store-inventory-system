<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SpecialStationeries.aspx.cs" Inherits="SA33.Team12.SSIS.Catalog.SpecialStationeries" %>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Special stationery</h2>
<fieldset>
    <fieldset>
    <legend>Add Special Stationery</legend>
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    Name</td>
                <td class="style2">
                    <asp:TextBox ID="NameTextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="NameTextBox" Display="Dynamic" 
                        ErrorMessage="Name is required" ValidationGroup="input"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Category</td>
                <td class="style2">
                    <asp:DropDownList ID="CategoryDDL" runat="server" DataSourceID="CategoryDS" 
                        DataTextField="Name" DataValueField="CategoryID" ValidationGroup="input">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="CategoryDS" runat="server" 
                        SelectMethod="GetAllCategories" TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                    </asp:ObjectDataSource>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Description</td>
                <td class="style2">
                    <asp:TextBox ID="DescriptionTextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="DescriptionTextBox" Display="Dynamic" 
                        ErrorMessage="Description is required" ValidationGroup="input"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Unit of Measure</td>
                <td class="style2">
                    <asp:TextBox ID="UOMTextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="UOMTextBox" Display="Dynamic" 
                        ErrorMessage="Unit of Measure is required" ValidationGroup="input"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style2">
                    <asp:Button ID="SubmitButton" runat="server" onclick="SubmitButton_Click" 
                        style="margin-left: 0px" Text="Add" ValidationGroup="input" />
                    <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
    </fieldset>
    <fieldset>
    <legend>Special Stationery List</legend>
    <asp:GridView runat="server" ID="SpecialStationeryGridView" AllowPaging="True"
        AutoGenerateColumns="False" DataSourceID="SpecialStationeryObjectDataSource"
        SelectedRowStyle-BackColor="LightGray" 
        onselectedindexchanged="SpecialStationeryGridView_SelectedIndexChanged" 
            DataKeyNames="SpecialStationeryID">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="SpecialStationeryID" 
                HeaderText="SpecialStationeryID" SortExpression="SpecialStationeryID" />
            <asp:TemplateField HeaderText="Category">
            <ItemTemplate>
            <%# ((SA33.Team12.SSIS.DAL.Category)Eval("Category")) == null ? "" : ((SA33.Team12.SSIS.DAL.Category)Eval("Category")).Name%>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" 
                SortExpression="ItemCode" />
            <asp:BoundField DataField="Description" HeaderText="Description" 
                SortExpression="Description" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" 
                SortExpression="Quantity" />
            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" 
                SortExpression="DateCreated" />
            <asp:BoundField DataField="DateModified" HeaderText="DateModified" 
                SortExpression="DateModified" />
            <asp:BoundField DataField="DateApproved" HeaderText="DateApproved" 
                SortExpression="DateApproved" />
            <asp:TemplateField HeaderText="CreatedBy">
            <ItemTemplate>
            <%# ((SA33.Team12.SSIS.DAL.User) Eval("CreatedByUser")).UserName %>
            </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="ModifiedBy">
            <ItemTemplate>
            <%# ((SA33.Team12.SSIS.DAL.User) Eval("ModifiedByUser")).UserName %>
            </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="ApprovedBy">
            <ItemTemplate>
            <%# ((SA33.Team12.SSIS.DAL.User)Eval("ApprovedByUser")) == null ? "" : ((SA33.Team12.SSIS.DAL.User)Eval("ApprovedByUser")).UserName%>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UnitOfMeasure" HeaderText="UnitOfMeasure" 
                SortExpression="UnitOfMeasure" />
        </Columns>

<SelectedRowStyle BackColor="LightGray"></SelectedRowStyle>
    </asp:GridView>

    <asp:ObjectDataSource ID="SpecialStationeryObjectDataSource" runat="server" 
        DataObjectTypeName="SA33.Team12.SSIS.DAL.SpecialStationery" DeleteMethod="DeleteSpecialStationery" 
        InsertMethod="CreateSpecialStationery" SelectMethod="GetAllSpecialStationeries" 
        TypeName="SA33.Team12.SSIS.BLL.CatalogManager" 
        UpdateMethod="UpdateSpecialStationery" 
            OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>


    <asp:DynamicDataManager ID="DynamicDataManager" runat="server" />

    <asp:ValidationSummary runat="server" DisplayMode="BulletList" 
        CssClass="failureNotification" />
    </fieldset>
    </fieldset>
</asp:Content>
