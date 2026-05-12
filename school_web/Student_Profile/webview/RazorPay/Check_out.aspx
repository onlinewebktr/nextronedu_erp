<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Check_out.aspx.cs" Inherits="school_web.Student_Profile.webview.RazorPay.Check_out" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../assets/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function PostData() {
            document.getElementById("form1").submit();
        }
    </script>
</head>
<body onload="PostData()">

    <form id="form1" runat="server" method="post" action="https://api.razorpay.com/v1/checkout/embedded">

        <asp:HiddenField ID="key_id" runat="server" />
        <asp:HiddenField ID="order_id" runat="server" />
        <asp:HiddenField ID="amount" runat="server" />
        <asp:HiddenField ID="name" runat="server"  />
        <asp:HiddenField ID="description" runat="server" Value="Payment" />
        <input type="hidden" name="prefill[email]" ID="email"   runat="server"  />
          <input type="hidden" name="prefill[contact]" ID="contact"   runat="server"  />
        <asp:HiddenField ID="transaction_id" runat="server" />
        <asp:HiddenField ID="callback_url" runat="server" />

        <button>Submit</button>


    </form>
</body>
</html>
