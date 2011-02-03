<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="SA33.Team12.SSIS.Test.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Find by criteria test</h2>

    First Name:
    <asp:TextBox ID="FirstNameTextBox" runat="server"></asp:TextBox>
    <br />
    <br />
    Last Name:
    <asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="SearchButton" runat="server" onclick="SearchButton_Click" 
        Text="Search" />

&nbsp;<asp:Button ID="ShowAllButton" runat="server" onclick="ShowAllButton_Click" 
        Text="Show All" />

<asp:GridView runat="server" ID="UserGridView">
</asp:GridView>


    <br />


    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="ObjectDataSource1">
        <Columns>
            <asp:BoundField DataField="CategoryID" HeaderText="CategoryID" 
                SortExpression="CategoryID" />
            <asp:BoundField DataField="StationeryID" HeaderText="StationeryID" 
                SortExpression="StationeryID" />
            <asp:BoundField DataField="LocationID" HeaderText="LocationID" 
                SortExpression="LocationID" />
            <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" 
                SortExpression="ItemCode" />
            <asp:BoundField DataField="Description" HeaderText="Description" 
                SortExpression="Description" />
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
            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" 
                SortExpression="CreatedBy" />
            <asp:BoundField DataField="ModifiedBy" HeaderText="ModifiedBy" 
                SortExpression="ModifiedBy" />
            <asp:BoundField DataField="ApprovedBy" HeaderText="ApprovedBy" 
                SortExpression="ApprovedBy" />
            <asp:CheckBoxField DataField="IsApproved" HeaderText="IsApproved" 
                SortExpression="IsApproved" />
            <asp:BoundField DataField="DateApproved" HeaderText="DateApproved" 
                SortExpression="DateApproved" />
            <asp:BoundField DataField="UnitOfMeasure" HeaderText="UnitOfMeasure" 
                SortExpression="UnitOfMeasure" />
            <asp:TemplateField HeaderText="Heading"></asp:TemplateField>
            <asp:TemplateField HeaderText = " User Name ">
                <ItemTemplate>
                    <asp:Label runat="server" ID="UserNameLabel" />
                </ItemTemplate>

                <EditItemTemplate>
                    <asp:TextBox runat="server" ID="UserNameTextBox"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetAllStationery" 
        TypeName="SA33.Team12.SSIS.BLL.StationeryManager"></asp:ObjectDataSource>
    <br />


</asp:Content>
