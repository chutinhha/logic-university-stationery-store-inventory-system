<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategoriesTestV.aspx.cs" Inherits="SA33.Team12.SSIS.Catalog.CategoriesTestV" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Category</h2>
<fieldset>
<legend>Maintain</legend>    
    <asp:GridView runat="server" ID="CategoryGridView" AllowPaging="True" DataKeyNames="CategoryID"
        AutoGenerateColumns="False" DataSourceID="CategoryObjectDataSource"
        SelectedRowStyle-BackColor="LightGray" 
        onselectedindexchanged="CategoryGridView_SelectedIndexChanged" 
        onrowdatabound="CategoryGridView_RowDataBound">
        <Columns>
            <%--<asp:BoundField DataField="UnitOfMeasure" HeaderText="UnitOfMeasure" 
                SortExpression="UnitOfMeasure" />--%>
            <asp:BoundField DataField="CategoryID" HeaderText="CategoryID" 
                SortExpression="CategoryID" />
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:DynamicControl runat="server" ID="NameLabel"
                        DataField="Name" Mode="ReadOnly" /> 
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DynamicControl runat="server" ID="NameTextBox"
                        DataField="Name" Mode="Edit" /> 
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsApproved" HeaderText="IsApproved" 
                ReadOnly="True" SortExpression="IsApproved" />
            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" 
                SortExpression="DateCreated" ReadOnly="True" />
            <asp:TemplateField HeaderText="CreatedBy" SortExpression="CreatedBy">
                <ItemTemplate>
                    <asp:Literal runat="server" ID="CreatedByLiteral" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DateModified" HeaderText="DateModified" 
                SortExpression="DateModified" ReadOnly="True" />
            <asp:TemplateField HeaderText="ModifiedBy" SortExpression="ModifiedBy">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ModifiedBy") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ModifiedBy") %>'></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DateApproved" HeaderText="DateApproved" 
                SortExpression="DateApproved" ReadOnly="True" />
            <asp:TemplateField HeaderText="ApprovedBy" SortExpression="ApprovedBy">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ApprovedBy") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("ApprovedBy") %>'></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                ShowSelectButton="True" />
        </Columns>

<SelectedRowStyle BackColor="LightGray"></SelectedRowStyle>
    </asp:GridView>

    <asp:ObjectDataSource ID="CategoryObjectDataSource" runat="server" 
        DataObjectTypeName="SA33.Team12.SSIS.DAL.Category" DeleteMethod="DeleteCategory" 
        InsertMethod="CreateCategory" SelectMethod="GetAllCategories" 
        TypeName="SA33.Team12.SSIS.BLL.CatalogManager" UpdateMethod="UpdateCategory">
    </asp:ObjectDataSource>


    <asp:DynamicDataManager ID="DynamicDataManager" runat="server" />

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" 
        CssClass="failureNotification" />
    <asp:GridView runat="server" ID="StationeryGridView"
        AutoGenerateColumns="true">
    </asp:GridView>
</fieldset>
</asp:Content>
