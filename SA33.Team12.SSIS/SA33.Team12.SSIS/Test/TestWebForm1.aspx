<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestWebForm1.aspx.cs" Inherits="SA33.Team12.SSIS.Test.TestWebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" 
            AutoGenerateRows="False" DefaultMode="Insert" EnableViewState="False" 
            oniteminserting="DetailsView1_ItemInserting">
            <Fields>
                <asp:TemplateField HeaderText="Item">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:DropDownList ID="CategoryDDL" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="CategoryDDL_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="StationeryDDL" runat="server" 
                            onselectedindexchanged="StationeryDDL_SelectedIndexChanged">
                        </asp:DropDownList>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="QuantityTextBox" runat="server"></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowInsertButton="True" />
            </Fields>
        </asp:DetailsView>
        <asp:GridView ID="GridView1" runat="server" EnableViewState="False">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
