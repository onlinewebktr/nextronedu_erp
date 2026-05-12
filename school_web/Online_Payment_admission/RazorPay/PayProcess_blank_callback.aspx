<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayProcess_blank_callback.aspx.cs" Inherits="school_web.Online_Payment_admission.RazorPay.PayProcess_blank_callback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Payment Process</title>
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    
      <style>
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            padding: 14px 0px 0px 0px;
            line-height: 49px;
            font-family: Arial;
            font-size: 23pt;
            border: 5px solid #67CFF5;
            width: 89%;
            height: 254px;
            display: block;
            position: fixed;
            background-color: White;
            z-index: 999;
            left: 50px;
            top: 102px;
        }
    </style>
    <script type="text/javascript">
        function ShowProgress() {
            // alert('sdsjgdhsdgfsd');
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        function ShowProgress_hide() {
            // alert('sdsjgdhsdgfsd');

            document.getElementsByClassName('loading').style.visibility = 'hidden';

        }
        $('form').on("submit", function () {
            // alert('sdsjgdhsdgfsd');
            ShowProgress();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="height: 1px; overflow: hidden">
                <asp:Button ID="btnSubmit" runat="server" Text="Pay"
                    OnClick="btnSubmit_Click" OnClientClick="retun ShowProgress();" />
            </div>
            <div class="loading" align="center" id="a1" runat="server">
                Please wait. Processing Payment. Please not close and bake app. When till payment process not done.
        <br />
                <br />
                <img src="loader.gif" />
            </div>
        </div>
    </form>
</body>
</html>
