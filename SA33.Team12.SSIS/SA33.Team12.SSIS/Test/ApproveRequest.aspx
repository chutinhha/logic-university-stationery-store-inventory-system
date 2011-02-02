<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveRequest.aspx.cs" Inherits="SA33.Team12.SSIS.Test.ApproveRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
    </div>
    </form>
</body>
</html>
