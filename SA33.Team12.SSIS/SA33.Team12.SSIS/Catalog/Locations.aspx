<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Locations.aspx.cs" Inherits="SA33.Team12.SSIS.Catalog.Locations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 109px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Locations</h2>
<fieldset>
    <fieldset>
    <legend>Add Location</legend>
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    Location Name</td>
                <td>
                    <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                    <asp:Button ID="SubmitButton" runat="server" onclick="SubmitButton_Click" 
                        style="margin-left: 0px" Text="Add" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
    </fieldset>
    <fieldset>
    <legend>Location List</legend>
    <asp:GridView runat="server" ID="LocationGridView" AllowPaging="True"
        AutoGenerateColumns="False" DataSourceID="LocationObjectDataSource"
        SelectedRowStyle-BackColor="LightGray" 
        DataKeyNames="LocationID">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="LocationID" HeaderText="LocationID" 
                SortExpression="LocationID" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
           <asp:TemplateField>
           <ItemTemplate>
           <%# ((SA33.Team12.SSIS.DAL.User) Eval("CreatedByUser")).UserName %>
           </ItemTemplate>
           </asp:TemplateField>
            <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" 
                SortExpression="CreatedDate" />
        </Columns>

<SelectedRowStyle BackColor="LightGray"></SelectedRowStyle>
    </asp:GridView>

    <asp:ObjectDataSource ID="LocationObjectDataSource" runat="server" 
        DataObjectTypeName="SA33.Team12.SSIS.DAL.Location" DeleteMethod="DeleteLocation" 
        InsertMethod="CreateLocation" SelectMethod="GetAllLocations" 
        TypeName="SA33.Team12.SSIS.BLL.CatalogManager" 
        UpdateMethod="UpdateLocation" OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>


    <asp:DynamicDataManager ID="DynamicDataManager" runat="server" />

    <asp:ValidationSummary runat="server" DisplayMode="BulletList" 
        CssClass="failureNotification" />
    </fieldset>
    </fieldset>
</asp:Content>
