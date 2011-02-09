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
                    <asp:TextBox ID="NameTextBox" runat="server" ValidationGroup="input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="NameTextBox" Display="Dynamic" 
                        ErrorMessage="Location name is required" ValidationGroup="input"></asp:RequiredFieldValidator>
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
    <legend>Location List</legend>
    <asp:GridView runat="server" ID="LocationGridView" AllowPaging="True"
        AutoGenerateColumns="False" DataSourceID="LocationObjectDataSource"
        SelectedRowStyle-BackColor="LightGray" 
        DataKeyNames="LocationID" onrowupdating="LocationGridView_RowUpdating">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
             <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Name" 
                SortExpression="Name">
                <ItemTemplate>
                    <asp:DynamicControl ID="DynamicControl1" runat="server" DataField="Name" 
                        Mode="ReadOnly" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DynamicControl ID="DynamicControl1" runat="server" DataField="Name" 
                        Mode="Edit" />
                </EditItemTemplate>
            </asp:TemplateField>
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
