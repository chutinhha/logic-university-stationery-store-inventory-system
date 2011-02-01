<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationeryRequestForm.aspx.cs" Inherits="SA33.Team12.SSIS.Test.StationeryRequestForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Stationery request form</h1>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            AutoGenerateEditButton="True" onrowediting="GridView1_RowEditing" 
            onrowupdating="GridView1_RowUpdating">
            <Columns>
                <asp:TemplateField HeaderText="Category">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" 
                            DataSourceID="ObjectDataSource1" DataTextField="Name" 
                            DataValueField="CategoryID">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                            SelectMethod="GetAllCategories" TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                        </asp:ObjectDataSource>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Stationery">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList2" runat="server" 
                            DataSourceID="ObjectDataSource2" DataTextField="Description" 
                            DataValueField="StationeryID" SelectedValue='<%# Bind("StationeryID") %>'>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
                            SelectMethod="GetAllStationeries" 
                            TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("StationeryID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity Needed">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" 
                            Text='<%# Bind("QuantityRequested") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("QuantityRequested") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    </p>
 
</asp:Content>
