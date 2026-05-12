<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student_Monthly_Payment.aspx.cs" Inherits="school_web.Student_Profile.webview.Student_Monthly_Payment" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Monthly Payment</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Language" content="en" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no" />

    <script src="../../assets/js/jquery-1.10.2.min.js"></script>



    <%-- ===================================================== --%>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600;700;800&display=swap" rel="stylesheet" />
    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            background: #edf3ff;
            font-family: 'Poppins',sans-serif;
            padding-bottom: 30px;
            overflow-x: hidden;
        }

        /* BACKGROUND */

        .bg {
            position: fixed;
            border-radius: 50%;
            filter: blur(90px);
            z-index: -1;
            animation: float 8s infinite ease-in-out;
        }

        .bg1 {
            width: 220px;
            height: 220px;
            background: #5d85ff;
            top: -60px;
            left: -60px;
        }

        .bg2 {
            width: 220px;
            height: 220px;
            background: #ff7ac6;
            right: -60px;
            bottom: 0;
            animation-delay: 2s;
        }

        @keyframes float {

            0% {
                transform: translateY(0);
            }

            50% {
                transform: translateY(-18px);
            }

            100% {
                transform: translateY(0);
            }
        }

        /* HEADER */

        .header {
            background: linear-gradient(135deg,#122c8c,#0d5be1,#29b6f6);
            padding: 20px 16px 90px;
            border-radius: 0 0 30px 30px;
            color: #fff;
            position: relative;
            overflow: hidden;
        }

            .header::before {
                content: '';
                position: absolute;
                width: 260px;
                height: 260px;
                border-radius: 50%;
                background: rgba(255,255,255,.08);
                top: -120px;
                right: -80px;
            }

        .header-flex {
            display: flex;
            align-items: center;
            gap: 14px;
        }

        .back-btn {
            width: 42px;
            height: 42px;
            border-radius: 14px;
            background: rgba(255,255,255,.15);
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 24px;
            backdrop-filter: blur(10px);
        }

        .header h2 {
            font-size: 24px;
            font-weight: 700;
        }

        .header p {
            font-size: 13px;
            opacity: .85;
        }

        /* MAIN */

        .container {
            padding: 0 14px;
            margin-top: -60px;
        }

        /* CARD */

        .card {
            background: rgba(255,255,255,.88);
            backdrop-filter: blur(18px);
            border-radius: 24px;
            padding: 16px;
            margin-bottom: 18px;
            box-shadow: 0 10px 30px rgba(0,0,0,.08);
            animation: fadeUp .6s ease;
        }

        /* TITLE */

        .section-title {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 14px;
        }

            .section-title h3 {
                font-size: 18px;
                color: #1d315b;
                font-weight: 700;
            }

        .reset-btn {
            border: none;
            background: linear-gradient(135deg,#ff7b54,#ff4fa0);
            color: #fff;
            padding: 10px 16px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: 700;
        }

        /* MONTH GRID */

        .month-list {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 12px;
        }

        .month-item {
            display: flex;
            justify-content: space-between;
            align-items: center;
            background: #f2f7ff;
            padding: 8px 14px;
            border-radius: 16px;
            transition: .3s;
        }

            .month-item:hover {
                transform: translateY(-2px);
            }

        .month-left {
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .month-color {
            width: 8px;
            height: 30px;
            border-radius: 20px;
        }

        .blue {
            background: linear-gradient(to bottom,#0f58d8,#29b6f6);
        }

        .pink {
            background: linear-gradient(to bottom,#ff4fa0,#ff7b54);
        }

        .green {
            background: linear-gradient(to bottom,#00b894,#55efc4);
        }

        .orange {
            background: linear-gradient(to bottom,#ff9800,#ffc107);
        }

        .purple {
            background: linear-gradient(to bottom,#7b61ff,#b388ff);
        }

        .red {
            background: linear-gradient(to bottom,#ff5252,#ff8a80);
        }

        .month-name {
            font-size: 14px;
            font-weight: 600;
            color: #213860;
        }

        .month-item input {
            width: 20px;
            height: 20px;
            accent-color: #0f58d8;
        }

        /* TABLE */

        .table-wrap {
            overflow: auto;
            border-radius: 18px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        thead {
            background: linear-gradient(135deg,#132c8d,#0d5be1);
            color: #fff;
        }

        th {
            padding: 12px 5px;
            text-align: left;
            font-size: 13px;
            white-space: nowrap;
            border: 1px solid #bdbdbd;
        }

        td {
            padding: 12px 5px;
            font-size: 13px;
            font-weight: 500;
            color: #233a61;
            white-space: nowrap;
            border: 1px solid #bdbdbd;
        }

        tbody tr:nth-child(1),
        tbody tr:nth-child(2) {
            background: #e8fff5;
        }

        tbody tr:nth-child(3),
        tbody tr:nth-child(4) {
            background: #fff9e5;
        }

        tbody tr:nth-child(5),
        tbody tr:nth-child(6),
        tbody tr:nth-child(7) {
            background: #fff0ec;
        }

        tfoot {
            background: #1b2f84;
            color: #fff;
            font-weight: 700;
        }

        /* BILLING */

        .billing-grid {
            display: grid;
            gap: 14px;
        }

        .input-box label {
            display: block;
            margin-bottom: 6px;
            font-size: 13px;
            color: #425577;
            font-weight: 600;
        }

        .input-box input {
            width: 100%;
            height: 48px;
            border: none;
            outline: none;
            border-radius: 14px;
            background: #f7faff;
            padding: 0 14px;
            font-size: 15px;
            font-weight: 600;
            color: #1e335a;
        }

        /* TOTAL */

        .total-box {
            background: linear-gradient(135deg,#0f58d8,#29b6f6);
            border-radius: 20px;
            padding: 16px;
            color: #fff;
            margin-top: 16px;
        }

            .total-box small {
                font-size: 12px;
                opacity: .8;
            }

            .total-box h2 {
                margin-top: 4px;
                font-size: 30px;
            }

        /* BUTTON */

        .pay-btn {
            width: 100%;
            height: 54px;
            border: none;
            border-radius: 18px;
            background: linear-gradient(135deg,#00b894,#00d68f);
            color: #fff;
            font-size: 17px;
            font-weight: 700;
            margin-top: 18px;
            cursor: pointer;
            transition: .3s;
            box-shadow: 0 10px 20px rgba(0,184,148,.25);
        }

            .pay-btn:hover {
                transform: translateY(-2px);
            }

        /* ANIMATION */

        @keyframes fadeUp {

            from {
                opacity: 0;
                transform: translateY(25px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        .reminder-card {
            background: white;
            max-width: 500px;
            width: 100%;
            border-radius: 15px;
            overflow: hidden;
            box-shadow: 0 10px 30px rgba(0,0,0,0.1);
            border-top: 8px solid #1a73e8;
        }

        .icon-header {
            background-color: #f8fbff;
            text-align: center;
            padding: 30px 20px 10px 20px;
        }

            .icon-header i {
                font-size: 50px;
                color: #fbbc04; /* Warning Amber */
                background: #fff8e1;
                padding: 20px;
                border-radius: 50%;
            }

        .card-body {
            padding: 30px;
            color: #3c4043;
            line-height: 1.6;
        }

        .message-content {
            margin-bottom: 25px;
            font-size: 16px;
        }

        .highlight-box {
            background-color: #f1f3f4;
            border-radius: 8px;
            padding: 15px;
            display: flex;
            align-items: flex-start;
            gap: 12px;
            border-left: 4px solid #1a73e8;
        }

            .highlight-box i {
                color: #1a73e8;
                margin-top: 4px;
            }

        .footer {
            margin-top: 30px;
            padding-top: 20px;
            border-top: 1px solid #e8eaed;
            color: #70757a;
            font-weight: 600;
        }

        .action-button {
            display: block;
            background-color: #1a73e8;
            color: white;
            text-align: center;
            padding: 14px;
            border-radius: 8px;
            text-decoration: none;
            font-weight: bold;
            margin-top: 20px;
            transition: background 0.3s;
        }

            .action-button:hover {
                background-color: #1557b0;
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



        <!-- BACKGROUND -->

        <div class="bg bg1"></div>
        <div class="bg bg2"></div>

        <!-- HEADER -->

        <div class="header">
            <div class="header-flex">
                <div>
                    <h2>Make Payment</h2>
                    <p>Pay monthly student fees online</p>
                </div>
            </div>
        </div>
        <div>
            <div style="height: 1px; overflow: hidden">
                <asp:Button ID="btnSubmit" runat="server" Text="Pay"
                    OnClick="btnSubmit_Click" OnClientClick="retun ShowProgress();" />
            </div>
            <div class="loading" align="center" id="a1" runat="server">
                Please wait.We have checking your payment status Processing. Please not close and bake app. When till payment process not done.
                <br />
                <br />
                <img src="RazorPay/loader.gif" />
            </div>
        </div>

        <!-- MAIN -->
        <div class="main" runat="server" id="payMentDV">
            <div class="container">
                <!-- MONTH SECTION -->
                <div class="card">
                    <div class="section-title">
                        <h3>Select Months</h3>
                        <asp:Button ID="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_Click" class="reset-btn" />
                    </div>


                    <div class="month-list">
                        <asp:Repeater ID="RPMonth" runat="server" OnItemDataBound="RPMonth_ItemDataBound">
                            <ItemTemplate>
                                <div class="month-item">
                                    <div class="month-left">
                                        <div class="month-color blue" runat="server" id="mnthColor"></div>
                                        <div class="month-name"><%#Eval("Month") %></div>
                                    </div>
                                    <asp:CheckBox ID="chk_month" OnCheckedChanged="chk_month_CheckedChanged" AutoPostBack="true" runat="server" Enabled="false" />
                                    <asp:Label ID="lbl_Month" runat="server" Visible="false" Text='<%#Bind("Month") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                </div>

                <!-- TABLE -->
                <asp:Panel ID="pnl_month_wise_fee_details" runat="server" Visible="false">
                    <div class="card">

                        <div class="section-title">
                            <h3>Month Wise Fee Details</h3>
                        </div>

                        <div class="table-wrap">

                            <table>
                                <thead>
                                    <tr>
                                        <th>Month</th>
                                        <th>Fees Head</th>
                                        <th>Fees Amt.</th>
                                        <th>Dis.</th>
                                        <th>Paid Prev.</th>
                                        <th>Payable</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    <asp:Repeater ID="rp_fee_details" runat="server" OnItemDataBound="rp_fee_details_ItemDataBound">
                                        <ItemTemplate>
                                            <tr id="row" runat="server">
                                                <td>
                                                    <asp:Label ID="lbl_mnth" runat="server" Text='<%#Bind("months") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblcontent" runat="server" Text='<%#Bind("content") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("amount") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_disc_amt" runat="server" Text='<%#Bind("disc_amount") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_pre_paid" runat="server" Text='<%#Bind("previously_paid") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_tot_pble" runat="server" Text='<%#Bind("total_payable") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th colspan="2">Total :</th>
                                        <th>
                                            <asp:Label ID="lbl_fee_amount" runat="server" Text=""></asp:Label></th>
                                        <th>
                                            <asp:Label ID="lbl_discount" runat="server" Text=""></asp:Label></th>
                                        <th>
                                            <asp:Label ID="lbl_paid_prev" runat="server" Text=""></asp:Label></th>
                                        <th>
                                            <asp:Label ID="lbl_total" runat="server" Text=""></asp:Label></th>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                </asp:Panel>

                <!-- BILLING -->
                <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%" id="pay" runat="server" visible="false">
                    <div class="card">

                        <div class="section-title">
                            <h3>Billing Details</h3>
                        </div>

                        <div class="billing-grid">

                            <div class="input-box">
                                <label>Total Amount</label>
                                <asp:TextBox ID="txttotal" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>

                            <div class="input-box">
                                <label>Paid Previously</label>
                                <asp:TextBox ID="txt_paid_prev" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>

                            <div class="input-box">
                                <label>Discount</label>
                                <asp:TextBox ID="txt_discount" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>

                            <div class="input-box">
                                <label>Late Fine</label>
                                <asp:TextBox ID="txtfineamount" runat="server" Text="0" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="total-box">
                            <small>Total Payable Bill</small>
                            <h2 id="paybleBills" runat="server"></h2>
                            <asp:TextBox ID="txttotalbill" Style="display: none" runat="server" class="form-control find-dv-txtbx" ReadOnly="true"></asp:TextBox>
                        </div>
                        <asp:Button ID="btn_save_payment" runat="server" Text="Pay" Visible="false" OnClientClick="return confirm('Are you sure you want to pay ?');" OnClick="btn_save_payment_Click" class="pay-btn" />


                    </div>
                </div>
            </div>
        </div>















        <div class="reminder-card" id="msgsDisplay" runat="server" visible="false">
            <div class="icon-header">
                <i class="fa-solid fa-clock-rotate-left"></i>
            </div>

            <div class="card-body">
                <div class="message-content">
                    <b>Dear Parent/Guardian,</b>
                    <br />
                    <br />
                    This is a gentle reminder that your admission/annual fee is currently pending. Kindly clear the outstanding dues at the earliest.
                </div>

                <div class="highlight-box">
                    <i class="fa-solid fa-circle-info"></i>
                    <span>Once the admission/annual fee has been settled, you will be able to proceed with the payment of the monthly fee.</span>
                </div>

                <div class="footer">
                    Thank you for your cooperation.
                </div>
            </div>
        </div>
    </form>
</body>
</html>
