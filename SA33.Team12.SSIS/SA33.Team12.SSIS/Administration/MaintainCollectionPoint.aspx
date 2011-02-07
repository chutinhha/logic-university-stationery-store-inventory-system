<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaintainCollectionPoint.aspx.cs" Inherits="SA33.Team12.SSIS.Administration.MaintainCollectionPoint" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Maintain Collection Point</h2>

<fieldset>
    <legend>Collection Points List</legend>

    <asp:GridView runat="server" ID="CollectionPointGridView" AllowPaging="True" DataKeyNames="CollectionPointID"
        AutoGenerateColumns="False" DataSourceID="CollectionPointObjectDataSource">
        <Columns>
            <asp:TemplateField HeaderText="Name" 
                SortExpression="Name">
                <ItemTemplate>
                    <asp:DynamicControl runat="server"
                        ID="CollectionPointIDControl"
                        DataField="CollectionPointID"
                        Mode="ReadOnly" Visible="false" />
                    <asp:DynamicControl runat="server"
                        ID="NameLabelControl"
                        DataField="Name"
                        Mode="ReadOnly" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DynamicControl runat="server"
                        ID="CollectionPointIDControl"
                        DataField="CollectionPointID"
                        Mode="Edit" Visible="false" />
                    <asp:DynamicControl runat="server"
                        ID="NameEditTextbox"
                        DataField="Name"
                        Mode="Edit" ValidationGroup="Edit" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True"  ValidationGroup="Edit" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource runat="server" ID="CollectionPointObjectDataSource" 
        DataObjectTypeName="SA33.Team12.SSIS.DAL.CollectionPoint" 
        DeleteMethod="DeleteCollectionPoint" InsertMethod="CreateCollectionPoint" 
        SelectMethod="GetAllCollectionPoints" 
        TypeName="SA33.Team12.SSIS.BLL.UserManager" 
        UpdateMethod="UpdateCollectionPoint" />
<asp:ValidationSummary runat="server" ID="ValidationSummary" ValidationGroup="Edit" />
</fieldset>
<fieldset>
    <legend>New Collection Point</legend>
    <asp:DetailsView runat="server" ID="CollectionPointDetailView" 
            DefaultMode="Insert"
            DataSourceID="CollectionPointObjectDataSource" AutoGenerateRows="false">
        <EmptyDataTemplate>
        </EmptyDataTemplate>
        <Fields>
            <asp:TemplateField HeaderText="Name">
                <InsertItemTemplate>
                    <asp:DynamicControl runat="server"
                        ID="NameEditTextbox"
                        DataField="Name"
                        Mode="Insert" ValidationGroup="Insert" />
                </InsertItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowInsertButton="True" ValidationGroup="Insert" />
        </Fields>
    </asp:DetailsView>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Insert" />
</fieldset>

<asp:DynamicDataManager runat="server" ID="DynamicDataManager" />

</asp:Content>
