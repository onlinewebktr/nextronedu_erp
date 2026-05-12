<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Check_out_bolt.aspx.cs" Inherits="school_web.Student_Profile.webview.RazorPay.Check_out_bolt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../assets/js/jquery-1.10.2.min.js"></script>
    <style>
        /* CSS */
        .button-29 {
            width: 270px;
            font-size: 73px;
            align-items: center;
            appearance: none;
            background-image: radial-gradient(100% 100% at 100% 0, #5adaff 0, #5468ff 100%);
            border: 0;
            border-radius: 6px;
            box-shadow: rgba(45, 35, 66, .4) 0 2px 4px,rgba(45, 35, 66, .3) 0 7px 13px -3px,rgba(58, 65, 111, .5) 0 -3px 0 inset;
            box-sizing: border-box;
            color: #fff;
            cursor: pointer;
            display: inline-flex;
            font-family: "JetBrains Mono",monospace;
            height: 108px;
            justify-content: center;
            line-height: 1;
            list-style: none;
            overflow: hidden;
            padding-left: 16px;
            padding-right: 16px;
            position: relative;
            text-align: left;
            text-decoration: none;
            transition: box-shadow .15s,transform .15s;
            user-select: none;
            -webkit-user-select: none;
            touch-action: manipulation;
            white-space: nowrap;
            will-change: box-shadow,transform;
            font-size: 18px;
        }

            .button-29:focus {
                box-shadow: #3c4fe0 0 0 0 1.5px inset, rgba(45, 35, 66, .4) 0 2px 4px, rgba(45, 35, 66, .3) 0 7px 13px -3px, #3c4fe0 0 -3px 0 inset;
            }

            .button-29:hover {
                box-shadow: rgba(45, 35, 66, .4) 0 4px 8px, rgba(45, 35, 66, .3) 0 7px 13px -3px, #3c4fe0 0 -3px 0 inset;
                transform: translateY(-2px);
            }

            .button-29:active {
                box-shadow: #3c4fe0 0 3px 7px inset;
                transform: translateY(2px);
            }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="hd_key" runat="server" />
            <asp:HiddenField ID="hd_amount" runat="server" />
            <asp:HiddenField ID="hd_fname" runat="server" />
            <asp:HiddenField ID="hd_email" runat="server" />
            <asp:HiddenField ID="hd_mobile" runat="server" />
            <asp:HiddenField ID="hd_razor_odr_id" runat="server" />
            <asp:HiddenField ID="hd_order_id" runat="server" />
            <asp:HiddenField ID="hd_logo" runat="server" />
            <asp:HiddenField ID="hd_regid" runat="server" />
            <asp:HiddenField ID="callback_url" runat="server" />
            <asp:HiddenField ID="hd_student_name" runat="server" />

            <button id="rzp-button1" class="button-29">Pay</button>
            <script src="https://checkout.razorpay.com/v1/checkout.js"></script>
            <script>



                var fname = $("#<%=hd_fname.ClientID%>").val();
                var amount = $("#<%=hd_amount.ClientID%>").val();
                var mobile = $("#<%=hd_mobile.ClientID%>").val();
                var email = $("#<%=hd_email.ClientID%>").val();
                var orderid = $("#<%=hd_order_id.ClientID%>").val();
                var marchantKey = $("#<%=hd_key.ClientID%>").val();
                var razor_odr_id = $("#<%=hd_razor_odr_id.ClientID%>").val();
                var regid = $("#<%=hd_regid.ClientID%>").val();
                var logo = $("#<%=hd_logo.ClientID%>").val();
                var callbackurl = $("#<%=callback_url.ClientID%>").val();
                var student_name = $("#<%=hd_student_name.ClientID%>").val();


                var options = {
                    "key": marchantKey, // Enter the Key ID generated from the Dashboard
                    "amount": amount, // Amount is in currency subunits. Default currency is INR. Hence, 50000 refers to 50000 paise
                    "currency": "INR",
                    "name": fname, //your business name
                    "description": "Month Payment",
                    "image": logo,
                    "order_id": razor_odr_id, //This is a sample Order ID. Pass the `id` obtained in the response of Step 1
                    "callback_url": callbackurl, //"https://eneqd3r9zrjok.x.pipedream.net/",
                    "redirect": true,
                    "webview_intent": true,
                    "prefill": { //We recommend using the prefill parameter to auto-fill customer's contact information especially their phone number
                        "name": student_name, //your customer's name
                        "email": email,
                        "contact": mobile //"9000090000" //Provide the customer's phone number for better conversion rates
                    },
                    "notes": {
                        "address": "Razorpay Corporate Office"
                    },
                    "theme": {
                        "color": "#3399cc"
                    }
                };
                var rzp1 = new Razorpay(options);
                $(document).ready(function () { 
                    rzp1.open();
                    e.preventDefault();
                }); 
            </script>
        </div>
    </form>
</body>
</html>
