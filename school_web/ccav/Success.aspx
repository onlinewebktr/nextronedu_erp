<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="school_web.ccav.Success" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="hd_bookingid" runat="server" />
            <asp:Label ID="lbl_date" runat="server" ></asp:Label>

            <asp:Label ID="lbl_transactionid" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="lbl_m_t_id" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="lbl_status" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
