<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequestApproval.aspx.cs" Inherits="SA33.Team12.SSIS.Approval.RequestApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Pending Request For Approval</h2>
    <p>
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </p>
   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="RequisitionID" 
            onrowcommand="GridView1_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="RequisitionID" SortExpression="RequisitionID">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" 
                            Text='<%# Eval("RequisitionID") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("RequisitionID") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RequisitionForm" HeaderText="RequisitionForm" 
                    SortExpression="RequisitionForm" />
                <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" 
                    SortExpression="CreatedBy" />
                <asp:BoundField DataField="DepartmentID" HeaderText="DepartmentID" 
                    SortExpression="DepartmentID" />
                <asp:BoundField DataField="StatusID" HeaderText="StatusID" 
                    SortExpression="StatusID" />
                <asp:BoundField DataField="UrgencyID" HeaderText="UrgencyID" 
                    SortExpression="UrgencyID" />
                <asp:BoundField DataField="DateRequested" HeaderText="DateRequested" 
                    SortExpression="DateRequested" />
                <asp:TemplateField HeaderText="Approve" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                            CommandName="test" CommandArgument='<%# Eval("RequisitionID") %>' Text="Approve"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="GetAllUnApprovedRequisition" 
            TypeName="SA33.Team12.SSIS.BLL.RequisitionManager"></asp:ObjectDataSource>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Approve All" />
</asp:Content>
