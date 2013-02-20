<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        名称：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
        地址：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
        电话：<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br />
        邮箱：<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox><br />
        状态：<asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ObjectDataSource1"
            DataTextField="Name" DataValueField="Id" Width="156px">
        </asp:DropDownList><asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllUserState"
            TypeName="BLL.UserStateBLL"></asp:ObjectDataSource>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" /><br />
        <hr />
    
    </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
            DataSourceID="ObjectDataSource2" ForeColor="#333333" GridLines="None" Height="183px"
            Width="488px" DataKeyNames="id">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="编号" SortExpression="Id" />
                <asp:BoundField DataField="Name" HeaderText="名称" SortExpression="Name" />
                <asp:BoundField DataField="Address" HeaderText="地址" SortExpression="Address" />
                <asp:BoundField DataField="Tel" HeaderText="电话" SortExpression="Tel" />
                <asp:BoundField DataField="Email" HeaderText="邮箱" SortExpression="Email" />
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                       <%# Eval("user_state.name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" DeleteMethod="Remove"
            SelectMethod="FindAllUser" TypeName="BLL.UserBLL">
            <DeleteParameters>
                <asp:Parameter Name="id" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
