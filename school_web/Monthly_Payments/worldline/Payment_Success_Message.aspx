<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment_Success_Message.aspx.cs" Inherits="school_web.Monthly_Payments.worldline.Payment_Success_Message" %>

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
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">

            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">


                            <div class="success-message">
                                <h2 class="success-h"><i class="fa fa-check success-icon" aria-hidden="true"></i>SUCCESS !</h2>
                                <asp:Label ID="Label1" runat="server" class="success-lbl">Your payment has been sucessfully recived</asp:Label>

                                <asp:Label ID="lbl_ampunt" runat="server" class="success-lbl"></asp:Label>
                                <asp:Label ID="lbl_Order_id" runat="server" class="success-lbl"></asp:Label>
                                <asp:Label ID="lbl_paymentdate" runat="server" class="success-lbl"></asp:Label>
                                <asp:Button ID="btn_reset" runat="server" Text="Print Slip" OnClick="btn_reset_Click" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>


                </div>

            </div>
        </div>
    </form>
</body>
</html>
