<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Suppliers.aspx.cs" Inherits="SA33.Team12.SSIS.Catalog.Suppliers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 109px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Suppliers</h2>
<fieldset>
    <fieldset>
    <legend>Add Supplier</legend>
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    Supplier Name</td>
                <td>
                    <asp:TextBox ID="NameTextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="NameTextBox" Display="Dynamic" 
                        ErrorMessage="Supplier name is required" ValidationGroup="input"></asp:RequiredFieldValidator>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Supplier Code</td>
                <td>
                    <asp:TextBox ID="SupplierCodeTextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="SupplierCodeTextBox" Display="Dynamic" 
                        ErrorMessage="Supplier code is required" ValidationGroup="input"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="SupplierCodeTextBox" Display="Dynamic" 
                        ErrorMessage="Enter 4 Charecters" ValidationExpression="\w{4}"></asp:RegularExpressionValidator>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Tender Year</td>
                <td>
                    <asp:Calendar ID="TenderYearCalender" runat="server"></asp:Calendar>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Ranking</td>
                <td>
                    <asp:DropDownList ID="RankingDDL" runat="server" ValidationGroup="input">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
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
    <legend>Supplier List</legend>
    <asp:GridView runat="server" ID="SupplierGridView" AllowPaging="True"
        AutoGenerateColumns="False" DataSourceID="SupplierObjectDataSource"
        SelectedRowStyle-BackColor="LightGray" 
        DataKeyNames="SupplierID">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="SupplierCode" HeaderText="SupplierCode" 
                SortExpression="SupplierCode" />
            <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" 
                SortExpression="CompanyName" />
            <asp:TemplateField HeaderText="TenderedYear" SortExpression="TenderedYear">
                <ItemTemplate>
                    <%# DateTime.Parse(Eval("TenderedYear").ToString()).Year %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Calendar ID="Calendar1" runat="server" 
                        SelectedDate='<%# Bind("TenderedYear") %>'></asp:Calendar>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="PreferredRank" HeaderText="PreferredRank" 
                SortExpression="PreferredRank" />
        </Columns>

<SelectedRowStyle BackColor="LightGray"></SelectedRowStyle>
    </asp:GridView>

    <asp:ObjectDataSource ID="SupplierObjectDataSource" runat="server" 
        DataObjectTypeName="SA33.Team12.SSIS.DAL.Supplier" DeleteMethod="DeleteSupplier" 
        InsertMethod="CreateSupplier" SelectMethod="GetAllSuppliers" 
        TypeName="SA33.Team12.SSIS.BLL.CatalogManager" 
        UpdateMethod="UpdateSupplier" OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>


    <asp:DynamicDataManager ID="DynamicDataManager" runat="server" />

    <asp:ValidationSummary runat="server" DisplayMode="BulletList" 
        CssClass="failureNotification" />
    </fieldset>
    </fieldset>
</asp:Content>
