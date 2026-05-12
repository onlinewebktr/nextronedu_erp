<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="enach_api.aspx.cs" Inherits="school_web.Admin.enach_api" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <script src="https://www.paynimo.com/paynimocheckout/client/lib/jquery.min.js" type="text/javascript"></script>
       
       <script type="text/javascript" src="https://www.paynimo.com/paynimocheckout/server/lib/checkout.js"></script>
 <script type="text/javascript">
     $(document).ready(function () {
         function handleResponse(res) {
             if (
                 typeof res !== "undefined" &&
                 typeof res.paymentMethod !== "undefined" &&
                 typeof res.paymentMethod.paymentTransaction !== "undefined" &&
                 typeof res.paymentMethod.paymentTransaction.statusCode !== "undefined"
             ) {
                 var statusCode = res.paymentMethod.paymentTransaction.statusCode;
                 if (statusCode === "0300") {
                     // success block
                 } else if (statusCode === "0398") {
                     // initiated block
                 } else {
                     // error block
                 }
             } else {
                 // error block
             }
         }

         document.getElementById('btnSubmit').onclick = function (e) {
             bindSubmitButton(); // Call the function here
             e.preventDefault();
         };

         function bindSubmitButton() {
             var token = $("#<%=hd_token.ClientID%>").val();
            var merchantId = $("#<%=hd_merchantId.ClientID%>").val();
            var student_admission_no = $("#<%=hd_student_admission_no.ClientID%>").val();
            var email = $("#<%=hd_email.ClientID%>").val();
            var mobile = $("#<%=hd_mobile.ClientID%>").val();
            var orderid = $("#<%=hd_order_id.ClientID%>").val();
             var camount = "1"; 
            var logo = $("#<%=hd_logo.ClientID%>").val();
            var startdate = $("#<%=hd_startdate.ClientID%>").val();
            var enddate = $("#<%=hd_enddate.ClientID%>").val();
             var maxamount = $("#<%=hd_camount.ClientID%>").val();
            var reqJson = {
                "features": {
                    "enableAbortResponse": true,
                    "enableExpressPay": true,
                    "enableMerTxnDetails": true,
                    "siDetailsAtMerchantEnd": true,
                    "enableSI": true
                },
                "consumerData": {
                    "deviceId": "WEBSH2",
                    "token": token,
                    "returnUrl": "https://pgproxyuat.in.worldline-solutions.com/linuxsimulator/MerchantResponsePage.jsp",
                    "responseHandler": handleResponse,
                    "paymentMode": "netBanking",
                    "merchantLogoUrl": logo,
                    "merchantId": merchantId,
                    "currency": "INR",
                    "consumerId": student_admission_no,
                    "consumerMobileNo": mobile,
                    "consumerEmailId": email,
                    "txnId": orderid,
                    "items": [{
                        "itemId": "FIRST",
                        "amount": "1",
                        "comAmt": "0"
                    }],
                    "customStyle": {
                        "PRIMARY_COLOR_CODE": "#45beaa",
                        "SECONDARY_COLOR_CODE": "#FFFFFF",
                        "BUTTON_COLOR_CODE_1": "#2d8c8c",
                        "BUTTON_COLOR_CODE_2": "#FFFFFF"
                    },
                    "accountNo": "",
                    "accountHolderName": "",
                    "ifscCode": "ICIC0000001",
                    "accountType": "Saving",
                    "debitStartDate": startdate,
                    "debitEndDate": enddate,
                    "maxAmount": maxamount,
                    "amountType": "M",
                    "frequency": "ADHO"
                }
            };

            $.pnCheckout(reqJson);
            if (reqJson.features.enableNewWindowFlow) {
                pnCheckoutShared.openNewWindow();
            }
        }

        // 🔽 Auto-click the submit button on page load
        $('#btnSubmit').trigger('click');
    });
 </script>

    <style>
        .btnSubmit {
    background-color: #2d8c8c;         /* Button background */
    color: #ffffff;                    /* Text color */
    border: none;
    padding: 12px 24px;
    font-size: 16px;
    font-weight: 600;
    border-radius: 8px;
    cursor: pointer;
    transition: background-color 0.3s ease, box-shadow 0.3s ease;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.btnSubmit:hover {
    background-color: #247676;         /* Darker shade on hover */
    box-shadow: 0 6px 10px rgba(0, 0, 0, 0.15);
}

.btnSubmit:disabled {
    background-color: #cccccc;
    cursor: not-allowed;
}
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <button id="btnSubmit" class="btnSubmit">Register Now</button>

               <asp:HiddenField ID="hd_token" runat="server" />
                <asp:HiddenField ID="hd_merchantId" runat="server" />
               <asp:HiddenField ID="hd_student_admission_no" runat="server" />
                 
                <asp:HiddenField ID="hd_email" runat="server" />
                <asp:HiddenField ID="hd_mobile" runat="server" />
                <asp:HiddenField ID="hd_camount" runat="server" />
                <asp:HiddenField ID="hd_order_id" runat="server" />
                <asp:HiddenField ID="hd_logo" runat="server" />
                <asp:HiddenField ID="hd_startdate" runat="server" />
                <asp:HiddenField ID="hd_enddate" runat="server" />
        </div>
    </form>
</body>
</html>
