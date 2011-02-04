<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationeryRetrievalForm.aspx.cs" Inherits="SA33.Team12.SSIS.StationeryRetrieval.StationeryRetrievalForm" %>
<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Stationery Retrieval Form</h2>


    <asp:FormView runat="server" ID="StationeryRetrievalFormView" 
        DataSourceID="ods" DataKeyNames="StationeryRetrievalFormID"
        AllowPaging="true" ondatabound="StationeryRetrievalFormView_DataBound">
        <ItemTemplate>
            StationeryRetrievalFormID:
            <asp:Label ID="StationeryRetrievalFormIDLabel" runat="server" 
                Text='<%# Bind("StationeryRetrievalFormID") %>' />
            <br />
            StationeryRetrievalNumber:
            <asp:Label ID="StationeryRetrievalNumberLabel" runat="server" 
                Text='<%# Bind("StationeryRetrievalNumber") %>' />
            <br />
            RetrievedBy:
            <asp:Label ID="RetrievedByLabel" runat="server" 
                Text='<%# Bind("RetrievedBy") %>' />
            <br />
            DateRetrieved:
            <asp:Label ID="DateRetrievedLabel" runat="server" 
                Text='<%# Bind("DateRetrieved") %>' />
            <br />
            StationeryRetrievalFormItems:
            <asp:Label ID="StationeryRetrievalFormItemsLabel" runat="server" 
                Text='<%# Bind("StationeryRetrievalFormItems") %>' />
            <br />
            RetrievedByUser:
            <asp:Label ID="RetrievedByUserLabel" runat="server" 
                Text='<%# Bind("RetrievedByUser") %>' />
            <br />
        <asp:GridView runat="server" ID="StationeryRetrievalFormItemGridView" 
        AutoGenerateColumns="False" DataKeyNames="StationeryRetrievalFormItemID">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="StationeryID" 
                    SortExpression="StationeryID">
                    <ItemTemplate>
                        <%# Convert.ToInt32(Eval("StationeryID")) == 0 ? "" : ((Stationery) Eval("Stationery")).ItemCode  %>
                        <%# Convert.ToInt32(Eval("SpecialStationeryID")) == 0 ? "" : ((SpecialStationery)Eval("SpecialStationery")).ItemCode%>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="QuantityNeeded" HeaderText="QuantityNeeded" 
                    SortExpression="QuantityNeeded" />
                <asp:BoundField DataField="QuantityRetrieved" HeaderText="QuantityRetrieved" 
                    SortExpression="QuantityRetrieved" />
                <asp:TemplateField HeaderText="Quantity Retrieved">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="StationeryRetrievalFormItemIDHiddenField"
                            Value='<%# Eval("StationeryRetrievalFormItemID") %>' />
                        <asp:TextBox 
                            runat="server"
                            ID="QuantityRetrievedTextBox" Text="1" CssClass="numericEntry">
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>        
    </ItemTemplate>
    </asp:FormView>
    <asp:Button runat="server" ID="UpdateButton" Text="Update" onclick="UpdateButton_Click"
        />    

    <asp:ObjectDataSource runat="server" ID="ods" 
        SelectMethod="GetAllStationeryRetrievalForms" 
        TypeName="SA33.Team12.SSIS.BLL.StationeryRetrievalManager">
    </asp:ObjectDataSource>
</asp:Content>
