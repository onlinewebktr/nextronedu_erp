<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment_Error_Message.aspx.cs" Inherits="school_web.Student_Profile.webview.Payment_Error_Message" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Failed</title>
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
            background: #f00;
        }

        .success-h {
            margin: 10px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            float: left;
            text-align: center;
            color: #fffdfd;
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
            color: #f00;
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

        .fail-card {
            background: #fff;
            padding: 30px 40px;
            border-radius: 10px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.1);
            text-align: center;
            width: 420px;
        }

        .fail-icon {
            font-size: 60px;
            color: #dc3545;
            margin-bottom: 10px;
        }

        .fail-title {
            font-size: 26px;
            color: #dc3545;
            font-weight: bold;
            margin-bottom: 10px;
        }

        .fail-text {
            font-size: 15px;
            color: #555;
            margin-bottom: 20px;
            line-height: 1.5;
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
        <div class="fail-card">

            <div class="fail-icon">✖</div>

            <div class="fail-title">Payment Failed</div>

            <div class="fail-text">
                <strong>Payment Unsuccessful</strong> <br /><br />
                We’re sorry, but your recent payment was not successful.  
                If the amount has been deducted from your account, please wait up to <b>48 hours</b> for the transaction to reflect.  
                You may also contact the school with a screenshot of the transaction for assistance.
            </div>

            <div class="details">
                <p><span class="label">Amount:</span> <asp:Label ID="lbl_ampunt" runat="server" /></p>
                <p><span class="label">Order ID:</span> <asp:Label ID="lbl_Order_id" runat="server" /></p>
                <p><span class="label">Payment Date:</span> <asp:Label ID="lbl_paymentdate" runat="server" /></p>
            </div>

             <asp:Button ID="btn_back" CssClass="btn-back" OnClick="btn_back_Click" Visible="false" runat="server" Text="Back" /> <asp:Button ID="Button1" runat="server" Text="Back" Visible="false" OnClientClick="closeWindow(); return false;" CssClass="btn-back"   />

        </div>
    </div>
</form>
</body>
</html>
