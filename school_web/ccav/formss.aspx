<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="formss.aspx.cs" Inherits="school_web.ccav.formss" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="hd_bookingid" Value="5959544566666" runat="server" />
            <asp:TextBox ID="txt_name" placeholder="Name" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_email" placeholder="Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_mobile" placeholder="Mobile" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_amount" placeholder="Amount" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Pay" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
