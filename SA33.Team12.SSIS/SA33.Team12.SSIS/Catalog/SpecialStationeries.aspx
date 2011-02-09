<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SpecialStationeries.aspx.cs" Inherits="SA33.Team12.SSIS.Catalog.SpecialStationeries" %>

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
    <h2>
        Special stationery</h2>
    <fieldset>
        <fieldset>
            <legend>Add Special Stationery</legend>
            <table style="width: 100%;">
                <tr>
                    <td class="style1">
                        Name
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="NameTextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NameTextBox"
                            Display="Dynamic" ErrorMessage="Name is required" ValidationGroup="input"></asp:RequiredFieldValidator>
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
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DescriptionTextBox"
                            Display="Dynamic" ErrorMessage="Description is required" ValidationGroup="input"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Unit of Measure
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="UOMTextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="UOMTextBox"
                            Display="Dynamic" ErrorMessage="Unit of Measure is required" ValidationGroup="input"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td class="style2">
                        <asp:Button ID="SubmitButton" runat="server" OnClick="SubmitButton_Click" Style="margin-left: 0px"
                            Text="Add" ValidationGroup="input" />
                        <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>Special Stationery List</legend>
            <asp:GridView runat="server" ID="SpecialStationeryGridView" AllowPaging="True" AutoGenerateColumns="False"
                DataSourceID="SpecialStationeryObjectDataSource" SelectedRowStyle-BackColor="LightGray"
                OnSelectedIndexChanged="SpecialStationeryGridView_SelectedIndexChanged" DataKeyNames="SpecialStationeryID"
                OnRowCommand="SpecialStationeryGridView_RowCommand">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                        ValidationGroup="edit" />
                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <%# ((SA33.Team12.SSIS.DAL.Category)Eval("Category")) == null ? "" : ((SA33.Team12.SSIS.DAL.Category)Eval("Category")).Name%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownList1" runat="server" 
                                DataSourceID="ObjectDataSource1" DataTextField="Name" 
                                DataValueField="CategoryID" SelectedValue='<%# Bind("CategoryID") %>'>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                                SelectMethod="GetAllCategories" TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                            </asp:ObjectDataSource>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ItemCode">
                        <ItemTemplate>
                            <%# Eval("ItemCode") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ItemCode") %>' 
                                ValidationGroup="edit"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="TextBox1" Display="Dynamic" 
                                ErrorMessage="ItemCode  Required" ValidationGroup="edit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="TextBox1" Display="Dynamic" 
                                ErrorMessage="Example Code: C001" ValidationExpression="^\w{1}\d{3}" 
                                ValidationGroup="edit"></asp:RegularExpressionValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <%# Eval("Description")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Description") %>' 
                                ValidationGroup="edit"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ControlToValidate="TextBox2" Display="Dynamic" 
                                ErrorMessage="Description Required!" ValidationGroup="edit"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UnitOfMeasure">
                        <ItemTemplate>
                            <%# Eval("UnitOfMeasure")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("UnitOfMeasure") %>' 
                                ValidationGroup="edit"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                ControlToValidate="TextBox3" Display="Dynamic" ErrorMessage="UOM Required!" 
                                ValidationGroup="edit"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <%# Eval("Quantity") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Quantity") %>' 
                                ValidationGroup="edit"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                ControlToValidate="TextBox4" Display="Dynamic" 
                                ErrorMessage="Quantity Required!" ValidationGroup="edit"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                ControlToValidate="TextBox4" Display="Dynamic" 
                                ErrorMessage="Value should be 1 - 10000" MaximumValue="10000" MinimumValue="1" 
                                Type="Integer" ValidationGroup="edit"></asp:RangeValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <SelectedRowStyle BackColor="LightGray"></SelectedRowStyle>
            </asp:GridView>
            <asp:ObjectDataSource ID="SpecialStationeryObjectDataSource" runat="server" DataObjectTypeName="SA33.Team12.SSIS.DAL.SpecialStationery"
                DeleteMethod="DeleteSpecialStationery" InsertMethod="CreateSpecialStationery"
                SelectMethod="GetAllSpecialStationeries" TypeName="SA33.Team12.SSIS.BLL.CatalogManager"
                UpdateMethod="UpdateSpecialStationery" OldValuesParameterFormatString="original_{0}">
            </asp:ObjectDataSource>
            <asp:DynamicDataManager ID="DynamicDataManager" runat="server" />
            <asp:ValidationSummary runat="server" DisplayMode="BulletList" CssClass="failureNotification" />
        </fieldset>
    </fieldset>
</asp:Content>
