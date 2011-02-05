<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="StationeryRetrievalForm.aspx.cs" Inherits="SA33.Team12.SSIS.StationeryRetrieval.UpdateStationeryRetrievalForm" %>

<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Stationery Retrieval Form</h2>
    <asp:FormView runat="server" ID="StationeryRetrievalFormView" DataKeyNames="StationeryRetrievalFormID"
        AllowPaging="true" OnDataBound="StationeryRetrievalFormView_DataBound">
        <ItemTemplate>
            <table>
                <tr>
                    <th align="left">
                        StationeryRetrievalFormID :
                    </th>
                    <td>
                        <asp:Label ID="StationeryRetrievalFormIDLabel" runat="server" Text='<%# Bind("StationeryRetrievalFormID") %>' />
                    </td>
                </tr>
                <tr>
                    <th align="left">
                        StationeryRetrievalNumber :
                    </th>
                    <td>
                        <asp:Label ID="StationeryRetrievalNumberLabel" runat="server" Text='<%# Bind("StationeryRetrievalNumber") %>' />
                    </td>
                </tr>
                <tr>
                    <th align="left">
                        RetrievedBy :
                    </th>
                    <td>
                        <asp:Label ID="RetrievedByLabel" runat="server" Text='<%# Bind("RetrievedBy") %>' />
                    </td>
                </tr>
                <tr>
                    <th align="left">
                        DateRetrieved :
                    </th>
                    <td>
                        <asp:Label ID="DateRetrievedLabel" runat="server" Text='<%# Bind("DateRetrieved") %>' />
                    </td>
                </tr>
                <tr>
                    <th align="left">
                        RetrievedByUser :
                    </th>
                    <td>
                        <asp:Label ID="RetrievedByUserLabel" runat="server" Text='<%# Convert.ToInt32(Eval("RetrievedBy")) == 0 ? "" : ((User) Eval("RetrievedByUser")).UserName %>' />
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView runat="server" ID="StationeryRetrievalFormItemGridView" AutoGenerateColumns="False"
                DataKeyNames="StationeryRetrievalFormItemID">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="StationeryID" SortExpression="StationeryID">
                        <ItemTemplate>
                            <%# Convert.ToInt32(Eval("StationeryID")) == 0 ? "" : ((Stationery) Eval("Stationery")).ItemCode  %>
                            <%# Convert.ToInt32(Eval("SpecialStationeryID")) == 0 ? "" : ((SpecialStationery)Eval("SpecialStationery")).ItemCode%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="QuantityNeeded" HeaderText="QuantityNeeded" SortExpression="QuantityNeeded" />
                    <asp:TemplateField HeaderText="Quantity Retrieved">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="StationeryRetrievalFormItemIDHiddenField" Value='<%# Eval("StationeryRetrievalFormItemID") %>' />
                            <asp:TextBox runat="server" Visible='<%# !this.IsRetrieved || !this.IsCollected %>'
                                ID="QuantityRetrievedTextBox" Text='<%# ((int) Eval("QuantityNeeded")) - 5 %>'
                                CssClass="numericEntry">
                            </asp:TextBox>
                            <asp:Label runat="server" ID="QuantityRetrievedLabel" Visible='<%# this.IsRetrieved %>'
                                Text='<%# Eval("QuantityRetrieved") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:GridView 
                runat="server"
                ID="StationeryRetrievalFormItemByDeptGridView"
                AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="StationeryID" SortExpression="StationeryID">
                        <ItemTemplate>
                            <%# Convert.ToInt32(Eval("StationeryID")) == 0 
                                ? "" : ((Stationery) Eval("Stationery")).ItemCode  %>
                            <%# Convert.ToInt32(Eval("SpecialStationeryID")) == 0 
                                ? "" : ((SpecialStationery)Eval("SpecialStationery")).ItemCode%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="QuantityNeededByItem" HeaderText="Quantity Needed" />
                    <asp:TemplateField HeaderText="Quantity Retrieved">
                        <ItemTemplate>
                            <%# Eval("QuantityRetrieved") %>
                            <asp:HiddenField runat="server" 
                                ID="StationeryRetrievalFormItemIDHiddenField" 
                                Value='<%# Eval("StationeryRetrievalFormItemID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Department" SortExpression="DepartmentID">
                        <ItemTemplate>
                            <%# Convert.ToInt32(Eval("DepartmentID")) == 0 
                                ? "" : ((Department) Eval("Department")).Name %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="QuantityNeeded" 
                        HeaderText="QuantityNeeded" SortExpression="QuantityNeeded" />
                    <asp:TemplateField HeaderText="Quantity Recommended">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="QuantityRetrievedLabel" Text='<%# Eval("QuantityRecommended") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity Actual">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="StationeryRetrievalFormItemIDHiddenField" Value='<%# Eval("StationeryRetrievalFormItemID") %>' />
                            <asp:Label runat="server" ID="QuantityRetrievedLabel" Text='<%# Eval("QuantityActual") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ItemTemplate>
    </asp:FormView>
    <asp:Button runat="server" ID="BackButton" Text="Back" OnClick="BackButton_Click" />
    <asp:Button runat="server" ID="UpdateButton" Text="Update" OnClick="UpdateButton_Click" />
</asp:Content>
