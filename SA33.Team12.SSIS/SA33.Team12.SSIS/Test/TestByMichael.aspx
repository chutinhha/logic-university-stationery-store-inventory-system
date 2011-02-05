<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestByMichael.aspx.cs" Inherits="SA33.Team12.SSIS.Test.TestByMichael" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Always put heading</h2>
<fieldset>
    <legend>Search</legend>
    <table style="width:100%;">
        <tr>
            <td>
                Created By:</td>
            <td>
                <asp:DropDownList ID="CreatedByDropDownList" runat="server" 
                    DataSourceID="CreatedByDataSource" DataTextField="UserName" 
                    DataValueField="UserID">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="CreatedByDataSource" runat="server" 
                    SelectMethod="GetAllUsers" TypeName="SA33.Team12.SSIS.BLL.UserManager">
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="SearchButton" runat="server" onclick="SearchButton_Click" 
                    style="height: 26px" Text="Search" />
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
<legend>Disbursements</legend>


<asp:GridView runat="server" ID="DisbursementGridView" AllowPaging="True"
    AutoGenerateColumns="False" onrowdatabound="DisbursementGridView_RowDataBound" 
        onpageindexchanging="DisbursementGridView_PageIndexChanging" 
        onrowcommand="DisbursementGridView_RowCommand" 
        DataKeyNames="DisbursementID" BorderColor="#CCCCCC" BorderStyle="Solid" 
        BorderWidth="1px" Width="100%">
    <AlternatingRowStyle BackColor="#FFFF99" BorderColor="#CCCCCC" 
        BorderStyle="Solid" BorderWidth="1px" CssClass="alternateRow" />
    <Columns>
        <asp:TemplateField HeaderText="Disbursement ID">
            <ItemTemplate>
                <asp:LinkButton runat="server" ID="Submit" CommandName="submit" Text='<%# Eval("DisbursementID") %>' CommandArgument='<%# Eval("DisbursementID") %>'></asp:LinkButton>

                <a href='TestMichaelDetail.aspx?ID=<%# Eval("DisbursementID") %>'>
                DisbusementID = <%# Eval("DisbursementID") %></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" 
            SortExpression="DateCreated" />
        <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" 
            SortExpression="CreatedBy" />
        <asp:BoundField DataField="StationeryRetrievalFormID" 
            HeaderText="StationeryRetrievalFormID" 
            SortExpression="StationeryRetrievalFormID" />

        <asp:TemplateField>
            <ItemTemplate>
                <asp:Literal runat="server" ID="CreatedByLiteral" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ShowSelectButton="true" />

    </Columns>
    <RowStyle BackColor="#CCFFFF" BorderColor="#CCCCCC" BorderStyle="Solid" 
        BorderWidth="1px" CssClass="normalRow" />
</asp:GridView>

<asp:DetailsView ID="DisbursementDetailView" runat="server" AutoGenerateRows="true">
</asp:DetailsView>

<asp:GridView runat="server" ID="DisbursementItemGridView" AllowPaging="True">
</asp:GridView>

</fieldset>

</asp:Content>
