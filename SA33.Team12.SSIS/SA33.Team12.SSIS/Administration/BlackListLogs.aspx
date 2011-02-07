<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BlackListLogs.aspx.cs" Inherits="SA33.Team12.SSIS.Administration.BlackListLogs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Black List Logs</h2>

<fieldset>
<legend>Black List Logs</legend>

    <asp:GridView runat="server" ID="BlackListLogGridView" AllowPaging="True" 
        AutoGenerateColumns="False" DataSourceID="BlackListLogObjectDataSource" >
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="BlacklistLogID" HeaderText="BlacklistLogID" 
                SortExpression="BlacklistLogID" />
            <asp:BoundField DataField="DepartmentID" HeaderText="DepartmentID" 
                SortExpression="DepartmentID" />
            <asp:TemplateField HeaderText="DateBlacklisted">
                <ItemTemplate>
                    <asp:DynamicControl runat="server" DataField="DateBlacklisted" 
                        Mode="ReadOnly"/>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DynamicControl runat="server" DataField="DateBlacklisted" 
                        Mode="Edit"/>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</fieldset>


<fieldset>
<legend>New Black List Log</legend>

    <asp:DetailsView runat="server" ID="BlackListLogDetailsView" 
        AutoGenerateRows="False" DataSourceID="BlackListLogObjectDataSource" >
        <Fields>
            <asp:BoundField DataField="BlacklistLogID" HeaderText="BlacklistLogID" 
                SortExpression="BlacklistLogID" />
            <asp:BoundField DataField="DepartmentID" HeaderText="DepartmentID" 
                SortExpression="DepartmentID" />
            <asp:BoundField DataField="DateBlacklisted" HeaderText="DateBlacklisted" 
                SortExpression="DateBlacklisted" />
            <asp:CommandField ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>

</fieldset>

<asp:ObjectDataSource runat="server" ID="BlackListLogObjectDataSource" 
        DataObjectTypeName="SA33.Team12.SSIS.DAL.BlacklistLog" 
        DeleteMethod="DeleteBlacklistLog" InsertMethod="CreateBlacklistLog" 
        SelectMethod="GetAllBlacklistLogs" TypeName="SA33.Team12.SSIS.BLL.UserManager" 
        UpdateMethod="UpdateBlacklistLog">
</asp:ObjectDataSource>

<asp:DynamicDataManager runat="server" ID="DynamicDataManager" />

</asp:Content>
