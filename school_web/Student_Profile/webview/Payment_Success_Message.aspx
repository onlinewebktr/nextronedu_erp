<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment_Success_Message.aspx.cs" Inherits="school_web.Student_Profile.webview.Payment_Success_Message" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Success Message</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Language" content="en" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>

    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" />

    <link href="../../font-awesome-4.0.3/css/font-awesome.css" rel="stylesheet" />
    <style>
        #form1, body {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            font-size: 23px;
        }

        .main {
            margin: 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            float: left;
        }



        .success-message {
            margin: 0px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            float: left;
            min-height: 150px;
            text-align: center;
            border: 1px solid #d6d6d6;
            background: #08375c;
        }

        .success-h {
            margin: 10px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            float: left;
            text-align: center;
            color: #208f0c;
            font-size: 30px;
            line-height: 40px;
        }


        .success-icon {
            margin: 16px 10px 15px 0px;
            padding: 0px;
            text-align: center;
            font-size: 27px;
            background: #ffffff;
            width: 40px;
            height: 40px;
            border-radius: 49%;
            color: #208f0c;
            line-height: 40px;
        }

        .success-lbl {
            margin: 0px 0px 15px 0px;
            padding: 0px 15px;
            text-align: center;
            width: 100%;
            height: auto;
            float: left;
            font-size: 17px;
            color: #fff;
        }
    </style>
     <script type="text/javascript">
         function closeWindow() {
             window.close();
         }
    </script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css">
    </head>
<body>
  <form id="form1" runat="server">
    <style>
        body {
            background: #f4f6f9;
            font-family: 'Segoe UI', sans-serif;
        }

        .main {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        .success-card {
            background: #fff;
            padding: 30px 40px;
            border-radius: 10px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.1);
            text-align: center;
            width: 400px;
        }

        .success-icon {
            font-size: 60px;
            color: #28a745;
            margin-bottom: 17px;
        }

        .success-title {
            font-size: 26px;
            color: #28a745;
            margin-bottom: 10px;
            font-weight: bold;
        }

        .success-text {
            font-size: 16px;
            color: #555;
            margin-bottom: 20px;
        }

        .details {
            text-align: left;
            margin-top: 15px;
        }

        .details p {
            margin: 8px 0;
            font-size: 15px;
        }

        .label {
            font-weight: bold;
            color: #333;
        }

        .btn-back {
            margin-top: 20px;
            background: #0019dc;
            color: #fff;
            border: none;
            padding: 10px 25px;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn-back:hover {
            background: #000fb3;
        }
    </style>

    <div class="main">
        <div class="success-card">

         <div class="success-icon" style="    text-align: center;
    width: 100%;"><i class="fa fa-check-circle"></i></div>

            <div class="success-title">Payment Successful</div>

            <div class="success-text">
                Your payment has been successfully received.
            </div>

            <div class="details">
                <p><span class="label">Amount:</span> <asp:Label ID="lbl_ampunt" runat="server" /></p>
                <p><span class="label">Order ID:</span> <asp:Label ID="lbl_Order_id" runat="server" /></p>
                <p><span class="label">Payment Date:</span> <asp:Label ID="lbl_paymentdate" runat="server" /></p>
                <p><span class="label">Transaction ID:</span> <asp:Label ID="lbl_orderidrozor" runat="server" /></p>
                <p><span class="label">Status:</span> <asp:Label ID="lbl_status" runat="server" /></p>
            </div>

            <asp:Button ID="btn_back" runat="server" Text="Go Back" Visible="false" OnClick="btn_back_Click"  CssClass="btn-back" /> <asp:Button ID="Button1" runat="server" Text="Go Back" Visible="false" OnClientClick="closeWindow(); return false;"  CssClass="btn-back" />

        </div>
    </div>
</form>
</body>
</html>
