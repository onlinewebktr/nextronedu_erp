<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Onlinepayment_For_career.aspx.cs" Inherits="school_web.Online_Payment_admission.RazorPay.Check_out_Onlinepayment_For_career" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="../../assets/js/jquery-1.10.2.min.js"></script>
     
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
        <asp:HiddenField ID="name" runat="server" />
        <asp:HiddenField ID="description" runat="server" Value="Payment" />
        <asp:HiddenField ID="email" runat="server" />
        <asp:HiddenField ID="contact" runat="server" />
        <asp:HiddenField ID="transaction_id" runat="server" />
        <asp:HiddenField ID="callback_url" runat="server" />

        <button>Submit</button>


    </form>
</body>
</html>
