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
                    <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Supplier Code</td>
                <td>
                    <asp:TextBox ID="SupplierCodeTextBox" runat="server"></asp:TextBox>
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
                    <asp:DropDownList ID="RankingDDL" runat="server">
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
                        style="margin-left: 0px" Text="Add" />
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
            <asp:BoundField DataField="SupplierID" HeaderText="SupplierID" 
                SortExpression="SupplierID" />
            <asp:BoundField DataField="SupplierCode" HeaderText="SupplierCode" 
                SortExpression="SupplierCode" />
            <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" 
                SortExpression="CompanyName" />
            <asp:BoundField DataField="TenderedYear" HeaderText="TenderedYear" 
                SortExpression="TenderedYear" />
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
