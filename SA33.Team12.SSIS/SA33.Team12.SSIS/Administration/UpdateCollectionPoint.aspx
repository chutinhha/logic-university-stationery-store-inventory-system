<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateCollectionPoint.aspx.cs" Inherits="SA33.Team12.SSIS.Administration.UpdateCollectionPoint" %>
<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Collection Point</h2>
<fieldset>
    <legend>Update Collection Point</legend>
    <asp:Label runat="server" ID="ErrorMessage" CssClass="failureNotification" />
    <asp:DetailsView runat="server" ID="DepartmentDetailView" 
        AutoGenerateRows="False" DataKeyNames="DepartmentID">
        <Fields>
            <asp:BoundField DataField="DepartmentID" HeaderText="DepartmentID" 
                SortExpression="DepartmentID" Visible="false" />
            <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:TemplateField HeaderText="Current Collection Point" 
                SortExpression="CollectionPointID">
                <ItemTemplate>
                    <%# ((CollectionPoint) Eval("CollectionPoint")).Name %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="New Collection Point" 
                SortExpression="CollectionPointID">
                <ItemTemplate>
                     <asp:DropDownList runat="server" 
                        ID="CollectionPointDropDownList" DataSourceID="CollectionPointObjectDataSource" 
                        DataTextField="Name" DataValueField="CollectionPointID"
                        SelectedValue='<%# Eval("CollectionPointID") %>'>
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="CollectionPointObjectDataSource" runat="server" 
                        SelectMethod="GetAllCollectionPoints" 
                        TypeName="SA33.Team12.SSIS.BLL.UserManager"></asp:ObjectDataSource>
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:CheckBoxField DataField="IsBlackListed" HeaderText="IsBlackListed" 
                SortExpression="IsBlackListed" Visible="false" />
        </Fields>
    </asp:DetailsView>
    <asp:Button runat="server" ID="UpdateCollectionPointButton" Text="Update" 
        onclick="UpdateCollectionPointButton_Click" />
</fieldset>
</asp:Content>
