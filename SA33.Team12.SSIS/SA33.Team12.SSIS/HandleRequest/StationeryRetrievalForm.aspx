<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="StationeryRetrievalForm.aspx.cs" Inherits="SA33.Team12.SSIS.StationeryRetrieval.UpdateStationeryRetrievalForm" %>

<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<fieldset>
<legend><h2>Stationery Retrieval Form</h2></legend>

    <asp:FormView runat="server" ID="StationeryRetrievalFormView" 
        DataKeyNames="StationeryRetrievalFormID"
        AllowPaging="false" OnDataBound="StationeryRetrievalFormView_DataBound">
        <ItemTemplate>
            <table class="screenFriendlyGridView">
                <tr class="odd">
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
                <tr class="odd">
                    <th align="left">
                        DateRetrieved :
                    </th>
                    <td>
                        <asp:Label ID="DateRetrievedLabel" runat="server" Text='<%# Bind("DateRetrieved") %>' />
                    </td>
                </tr>
                <tr>
                    <th align="left">
                        Retrieved By :
                    </th>
                    <td>
                        <asp:Label ID="RetrievedByUserLabel" runat="server" Text='<%# Convert.ToInt32(Eval("RetrievedBy")) == 0 ? "" : ((User) Eval("RetrievedByUser")).UserName %>' />
                    </td>
                </tr>
            </table>
            <fieldset>
                <legend>Item List</legend>
            <asp:GridView runat="server" ID="StationeryRetrievalFormItemGridView" AutoGenerateColumns="False"
                DataKeyNames="StationeryRetrievalFormItemID">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stationery" SortExpression="StationeryID">
                        <ItemTemplate>
                            <%# Convert.ToInt32(Eval("StationeryID")) == 0 ? "" : ((Stationery) Eval("Stationery")).ItemCode  %>
                            <%# Convert.ToInt32(Eval("SpecialStationeryID")) == 0 ? "" : ((SpecialStationery)Eval("SpecialStationery")).ItemCode%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Special">
                        <ItemTemplate>
                            <input type="checkbox" <%# Convert.ToInt32(Eval("StationeryID")) == 0 ? "checked='checked'" : ""  %> disabled="disabled" />
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
                    <asp:TemplateField HeaderText="Stationery" SortExpression="StationeryID">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("IsSpecial")) ? ((SpecialStationery)Eval("SpecialStationery")).ItemCode : ((Stationery)Eval("Stationery")).ItemCode%>          
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CheckBoxField DataField="IsSpecial" HeaderText="Is Special" />
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
                    <asp:TemplateField HeaderText="Urgency" SortExpression="Urgency">
                        <ItemTemplate>
                            <%# Eval("Urgency") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="QuantityNeeded" 
                        HeaderText="Quantity Needed" SortExpression="QuantityNeeded" />
                    <asp:TemplateField HeaderText="Quantity Recommended">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="QuantityRetrievedLabel" Text='<%# Eval("QuantityRecommended") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity Actual">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="srfByDeptIDHiddenField" Value='<%# Eval("StationeryRetrievalFormItemByDeptID") %>' />
                            <asp:TextBox runat="server" ID="QuantityActualTextBox"
                                Text='<%# Eval("QuantityRecommended") %>'  Visible='<%# !this.IsCollected %>' />
                            <asp:Label runat="server" ID="QuantityActualLabel"
                                Text='<%# Eval("QuantityActual") %>' Visible='<%# this.IsCollected %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </fieldset>
        </ItemTemplate>
    </asp:FormView>
    <asp:Button runat="server" ID="BackButton" Text="Back" OnClick="BackButton_Click" />
    <asp:Button runat="server" ID="UpdateRetrievedQuantityButton" Text="Update" 
        oncommand="UpdateButton_Command" CommandName="Retrieved" />
    <asp:Button runat="server" ID="UpdateActualQuantityButton" Text="Update" 
        oncommand="UpdateButton_Command" CommandName="Actual" />

</fieldset>
</asp:Content>
